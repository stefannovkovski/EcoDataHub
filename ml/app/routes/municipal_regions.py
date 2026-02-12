from flask import Blueprint, request, jsonify
from app.services.municipal_region_service import forecast_regional_waste

bp = Blueprint(
    "municipal_regions",
    __name__,
    url_prefix="/api/collected-generated"
)

@bp.route("/regions", methods=["POST"])
def forecast_regions():
    data = request.get_json()

    df = forecast_regional_waste(
        waste_type=data["type"],
        start_year=data["start_year"],
        end_year=data["end_year"],
        regions=data["regions"],
        standardized=data.get("standardized", False)
    )

    return jsonify(df.to_dict(orient="records"))
