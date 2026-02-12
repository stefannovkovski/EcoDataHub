from flask import Blueprint, request, jsonify
import pandas as pd
from sqlalchemy import create_engine
from app.services.industrial_water_service import forecast_industrial_water

bp = Blueprint(
    "industrial_water",
    __name__,
    url_prefix="/api/industrial-water"
)

engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

@bp.route("/forecast", methods=["POST"])
def forecast():
    data = request.get_json()
    start_year = data["start_year"]
    end_year = data["end_year"]
    columns = data["columns"]
    standardized = data.get("standardized", False)

    df = pd.read_sql('SELECT * FROM "Water_For_Productions";', engine).drop(columns=["Id"])

    result_df = forecast_industrial_water(df, start_year, end_year, columns, standardized)
    return jsonify(result_df.to_dict(orient="records"))
