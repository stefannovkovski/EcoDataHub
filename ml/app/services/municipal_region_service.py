import json
import joblib
import pandas as pd
from pathlib import Path
from sqlalchemy import create_engine
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]

# Database connection
engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

def forecast_regional_waste(waste_type, start_year, end_year, regions, standardized=False):
    # Determine model directory
    model_dir = BASE_DIR / "models" / (
        "collected_regions_random_forest" if waste_type == "collected"
        else "generated_regions_random_forest"
    )

    # Load metadata
    with open(model_dir / "metadata.json") as f:
        metadata = json.load(f)
    trained_until = metadata["trained_until_year"]

    # Load data
    df = pd.read_sql('SELECT * FROM "Collected_and_generated_municipal_wastes";', engine)
    df = df.drop(columns=["Id"])
    df["Year"] = df["Year"].astype(int)

    # Filter by waste type
    df_type = df[df["Type"] == ("Collected municipal waste" if waste_type == "collected" else "Generated municipal waste")]

    # Filter historical data up to min(end_year, trained_until)
    historical_years = df_type[(df_type["Year"] >= start_year) & (df_type["Year"] <= min(end_year, trained_until))]
    result = historical_years[["Year"] + regions].copy()

    # Forecast future years if needed
    if end_year > trained_until:
        future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
        for region in regions:
            model = joblib.load(model_dir / f"{region}.pkl")
            future_years[region] = model.predict(future_years[["Year"]])
        result = pd.concat([result, future_years], ignore_index=True)

    # Optional standardization
    if standardized:
        scaler = StandardScaler()
        result[regions] = scaler.fit_transform(result[regions])

    return result
