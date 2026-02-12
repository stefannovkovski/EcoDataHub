import pandas as pd
from pathlib import Path
import joblib
import numpy as np
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]
MODEL_DIR = BASE_DIR / "models/Industrial_water_use_poly"

def forecast_industrial_water(df, start_year, end_year, columns, standardized=False):
    try:
        df = df.sort_values("Year")
        trained_until = df["Year"].max()

        result = df[(df["Year"] >= start_year) & (df["Year"] <= min(end_year, trained_until))].copy()

        if end_year > trained_until:
            future_years = pd.DataFrame({"Year": range(trained_until + 1, end_year + 1)})
            forecasts = {}

            for col in columns:
                model_path = MODEL_DIR / f"{col}_poly2.pkl"

                if not model_path.exists():
                    raise FileNotFoundError(f"Model file not found: {model_path}")

                model = joblib.load(model_path)

                predictions = model.predict(future_years[["Year"]])

                predictions = np.maximum(predictions, 0)

                predictions = np.nan_to_num(predictions, nan=0.0, posinf=0.0, neginf=0.0)

                forecasts[col] = predictions

            future_df = future_years.copy()
            for col in columns:
                future_df[col] = forecasts[col]

            result = pd.concat([result, future_df], ignore_index=True)

        if standardized:
            scaler = StandardScaler()
            result[columns] = scaler.fit_transform(result[columns])
            result[columns] = result[columns].fillna(0)

        result = result.replace([np.inf, -np.inf], 0)
        result = result.fillna(0)

        return result

    except Exception as e:
        import traceback
        traceback.print_exc()
        raise