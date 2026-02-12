import pandas as pd
import joblib
from pathlib import Path
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]

def forecast_so2(df, start_year, end_year, columns, standardized=False):
    trained_until = df["Year"].max()
    result = df[(df["Year"] >= start_year) & (df["Year"] <= min(end_year, trained_until))][["Year"] + list(columns)].copy()

    if end_year > trained_until:
        future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
        forecasts = {}
        for col in columns:
            model_path = BASE_DIR / f"models/TotalEmissionSO2s/{col}_ridge_model.pkl"
            model = joblib.load(model_path)
            pred = model.predict(future_years[["Year"]])
            forecasts[col] = pred
        future_df = future_years.copy()
        for col in columns:
            future_df[col] = forecasts[col]
        result = pd.concat([result, future_df], ignore_index=True)

    if standardized:
        scaler = StandardScaler()
        result[columns] = scaler.fit_transform(result[columns])

    return result
