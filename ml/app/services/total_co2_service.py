import pandas as pd
import joblib
from pathlib import Path
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]

def forecast_total_co2(df, start_year, end_year, standardized=False):
    trained_until = df["Year"].max()
    result = df[(df["Year"] >= start_year) & (df["Year"] <= min(end_year, trained_until))][["Year"] + ["Value"]].copy()

    if end_year > trained_until:
        future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
        model_path = BASE_DIR / "models/TotalEmissionCO2s/ridge_total_emission_model.pkl"
        model = joblib.load(model_path)
        pred = model.predict(future_years[["Year"]].values)
        future_df = future_years.copy()
        future_df["Value"] = pred
        result = pd.concat([result, future_df], ignore_index=True)

    if standardized:
        scaler = StandardScaler()
        result[["Value"]] = scaler.fit_transform(result[["Value"]])

    return result
