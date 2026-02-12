import pandas as pd
import joblib
from pathlib import Path
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]

def forecast_waste_by_economic_activity(df, waste_type, start_year, end_year, columns, standardized=False):
    df_type = df[df["Type"] == waste_type].reset_index(drop=True)
    trained_until = df_type["Year"].max()

    result = df_type[(df_type["Year"] >= start_year) & (df_type["Year"] <= min(end_year, trained_until))][["Year"] + list(columns)].copy()

    if end_year > trained_until:
        future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
        forecasts = {}
        for col in columns:
            model_path = BASE_DIR / f"models/WasteBySectionOfEconomicActivitys/{waste_type}_{col}_lr_model.pkl"
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
