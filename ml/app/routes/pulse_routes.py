from flask import Blueprint, request, jsonify
from app.services.pulse_service import predict_pulse

bp = Blueprint(
    "pulse",
    __name__,
    url_prefix="/api/pulse"
)

@bp.route("/forecast", methods=["POST"])
def forecast():
    data = request.get_json()

    city = data["city"]
    indicators = data["indicators"]
    start_year = data.get("start_year", 2026)
    end_year = data.get("end_year", 2027)

    try:
        results = predict_pulse(city, indicators, start_year, end_year)

        return jsonify({
            indicator: df.to_dict(orient="records")
            for indicator, df in results.items()
        })

    except FileNotFoundError as e:
        return jsonify({"error": str(e)}), 404

    except Exception as e:
        return jsonify({"error": str(e)}), 500
