from flask import Blueprint, request, jsonify
from sqlalchemy import create_engine
import pandas as pd
from app.services.water_abstracted_service import forecast_water_abstracted

bp = Blueprint(
    "water_abstracted",
    __name__,
    url_prefix="/api/water-abstracted"
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
    source_type = data["source_type"]
    standardized = data.get("standardized", False)

    df = pd.read_sql('SELECT * FROM "Water_abstracted_by_business_entitiess";', engine).drop(columns=["Id"])
    df = df.drop(columns=[
        "AgricultureForestryFishing",
        "WaterSupplyWasteManagement",
        "MiningAndQuarrying"
    ])

    result_df = forecast_water_abstracted(df, start_year, end_year, columns, source_type, standardized)
    return jsonify(result_df.to_dict(orient="records"))
