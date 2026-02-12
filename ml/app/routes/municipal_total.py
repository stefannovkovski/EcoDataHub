from flask import Blueprint, request, jsonify
from app.services.municipal_total_service import forecast_total_waste

bp = Blueprint("municipal_total", __name__, url_prefix="/api/municipal-waste/total")

@bp.route("/forecast", methods=["POST"])
def forecast():
    data = request.get_json()
    print("RAW DATA:", data)
    print("TYPES:", data.get("types"), type(data.get("types")))
    df = forecast_total_waste(
        start_year=data["start_year"],
        end_year=data["end_year"],
        types=data["types"],
        standardized=data.get("standardized", False)
    )

    return jsonify(df.to_dict(orient="records"))
