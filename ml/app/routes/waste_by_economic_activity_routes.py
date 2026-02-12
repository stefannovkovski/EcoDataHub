from flask import Blueprint, request, jsonify
from sqlalchemy import create_engine
import pandas as pd
from app.services.waste_by_economic_activity_service import forecast_waste_by_economic_activity

bp = Blueprint(
    "waste_by_economic_activity",
    __name__,
    url_prefix="/api/waste-by-economic-activity"
)

engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

@bp.route("/forecast", methods=["POST"])
def forecast():
    data = request.get_json()
    waste_type = data["type"]
    start_year = data["start_year"]
    end_year = data["end_year"]
    columns = data["columns"]
    standardized = data.get("standardized", False)

    df = pd.read_sql('SELECT * FROM "WasteBySectionOfEconomicActivitys";', engine).drop(columns=["Id", "Households", "Extraterritorial"])
    df[columns] = df[columns].fillna(df[columns].mean())

    result_df = forecast_waste_by_economic_activity(df, waste_type, start_year, end_year, columns, standardized)

    return jsonify(result_df.to_dict(orient="records"))
