from flask import Blueprint, request, jsonify
from sqlalchemy import create_engine
import pandas as pd
from app.services.total_so2_service import forecast_so2

bp = Blueprint(
    "so2",
    __name__,
    url_prefix="/api/so2"
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

    df = pd.read_sql('SELECT * FROM "TotalEmissionSO2s";', engine).drop(columns=["Id"])
    result_df = forecast_so2(df, start_year, end_year, columns, standardized)

    return jsonify(result_df.to_dict(orient="records"))
