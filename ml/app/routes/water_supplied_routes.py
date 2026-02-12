from flask import Blueprint, request, jsonify
import pandas as pd
import numpy as np
from sqlalchemy import create_engine
from app.services.water_supplied_service import forecast_water_supplied
import json

bp = Blueprint(
    "water_supplied",
    __name__,
    url_prefix="/api/water-supplied"
)

engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

@bp.route("/forecast", methods=["POST", "OPTIONS"])
def forecast():
    # Handle preflight CORS request
    if request.method == "OPTIONS":
        return "", 200

    try:
        data = request.get_json()
        print(f"Received request: {data}")

        start_year = data.get("start_year")
        end_year = data.get("end_year")
        columns = data.get("columns", [])
        standardized = data.get("standardized", False)

        if not columns:
            return jsonify({"error": "No columns specified"}), 400

        df = pd.read_sql('SELECT * FROM "Water_supplied_by_business_entitiess";', engine).drop(columns=["Id"])

        result_df = forecast_water_supplied(df, start_year, end_year, columns, standardized)

        # Clean up NaN, inf values before converting to dict
        result_df = result_df.replace([np.inf, -np.inf], 0)
        result_df = result_df.fillna(0)

        # Convert to dict
        result = result_df.to_dict(orient="records")

        # Validate JSON (will raise error if NaN/Infinity exists)
        try:
            json.dumps(result, allow_nan=False)
        except ValueError as e:
            print(f"JSON validation failed: {e}")
            # If validation fails, do more aggressive cleaning
            for row in result:
                for key, value in row.items():
                    if isinstance(value, float) and (np.isnan(value) or np.isinf(value)):
                        row[key] = 0.0

        print(f"Returning {len(result)} records")
        return jsonify(result), 200

    except KeyError as e:
        error_msg = f"Missing required field: {str(e)}"
        print(error_msg)
        return jsonify({"error": error_msg}), 400

    except Exception as e:
        error_msg = f"Error in forecast: {str(e)}"
        print(error_msg)
        import traceback
        traceback.print_exc()
        return jsonify({"error": error_msg}), 500