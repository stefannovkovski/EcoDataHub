import pandas as pd
from pathlib import Path
import joblib
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]


def forecast_water_abstracted(df, start_year, end_year, columns, source_type, standardized=False):
    df_source = df[df["WaterSourceType"] == source_type][["Year"] + columns].dropna()
    trained_until = df_source["Year"].max()

    result = df_source[(df_source["Year"] >= start_year) & (df_source["Year"] <= min(end_year, trained_until))].copy()

    if end_year > trained_until:
        future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
        forecasts = {}
        for col in columns:
            model_path = BASE_DIR / f"models/Water_abstracted_by_business_entitiess/{source_type}_{col}_poly2.pkl"
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
