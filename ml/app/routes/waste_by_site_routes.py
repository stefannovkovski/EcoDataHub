from flask import Blueprint, request, jsonify
from sqlalchemy import create_engine
import pandas as pd
from app.services.waste_by_site_service import forecast_waste_by_site

bp = Blueprint(
    "waste_by_site",
    __name__,
    url_prefix="/api/waste-by-site"
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

    df = pd.read_sql('SELECT * FROM "Waste_by_site_of_generations";', engine).drop(columns=["Id"])
    result_df = forecast_waste_by_site(df, start_year, end_year, columns, standardized)

    return jsonify(result_df.to_dict(orient="records"))
