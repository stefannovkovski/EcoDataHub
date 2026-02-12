from flask import Blueprint, request, jsonify
from app.services.amount_collected_service import forecast_amount_collected

amount_collected_bp = Blueprint(
    "amount_collected",
    __name__,
    url_prefix="/api/amount-collected"
)

@amount_collected_bp.route("/forecast", methods=["POST"])
def forecast():
    payload = request.get_json()

    start_year = payload["start_year"]
    end_year = payload["end_year"]
    columns = payload["columns"]
    standardized = payload.get("standardized", False)

    df = forecast_amount_collected(
        start_year=start_year,
        end_year=end_year,
        columns=columns,
        standardized=standardized
    )

    return jsonify(df.to_dict(orient="records"))
