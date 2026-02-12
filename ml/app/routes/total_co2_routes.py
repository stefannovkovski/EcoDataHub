from flask import Blueprint, request, jsonify
from sqlalchemy import create_engine
import pandas as pd
from app.services.total_co2_service import forecast_total_co2

bp = Blueprint(
    "total_co2",
    __name__,
    url_prefix="/api/total-co2"
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
    standardized = data.get("standardized", False)

    df = pd.read_sql('SELECT * FROM "TotalEmissionCO2s";', engine).drop(columns=["Id"])
    result_df = forecast_total_co2(df, start_year, end_year, standardized)

    return jsonify(result_df.to_dict(orient="records"))
