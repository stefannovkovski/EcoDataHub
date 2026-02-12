from flask import Flask

from app.routes import sector_co2_routes, total_so2_routes
from flask_cors import CORS

def create_app():
    app = Flask(__name__)
    CORS(app, origins="*")

    from app.routes.amount_collected import amount_collected_bp
    from app.routes import municipal_regions, municipal_total
    from app.routes import water_supply_routes
    from app.routes import total_co2_routes
    from app.routes import waste_by_site_routes
    from app.routes import waste_waters_routes
    from app.routes import waste_by_economic_activity_routes
    from app.routes import water_abstracted_routes
    from app.routes import industrial_water_routes
    from app.routes import water_supplied_routes
    from app.routes import pulse_routes


    app.register_blueprint(amount_collected_bp)
    app.register_blueprint(municipal_regions.bp)
    app.register_blueprint(municipal_total.bp)
    app.register_blueprint(water_supply_routes.bp)
    app.register_blueprint(sector_co2_routes.bp)
    app.register_blueprint(total_co2_routes.bp)
    app.register_blueprint(total_so2_routes.bp)
    app.register_blueprint(waste_by_site_routes.bp)
    app.register_blueprint(waste_waters_routes.bp)
    app.register_blueprint(waste_by_economic_activity_routes.bp)
    app.register_blueprint(water_abstracted_routes.bp)
    app.register_blueprint(industrial_water_routes.bp)
    app.register_blueprint(water_supplied_routes.bp)
    app.register_blueprint(pulse_routes.bp)


    return app