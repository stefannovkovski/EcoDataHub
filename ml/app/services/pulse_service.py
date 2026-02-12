import pandas as pd
import numpy as np
import joblib
from pathlib import Path

BASE_DIR = Path(__file__).resolve().parents[2]
MODEL_DIR = BASE_DIR / "models" / "pulse_city_indicator_models"
DATA_PATH = BASE_DIR / "pulse_data" / "pulseeco_avg_day_2018-2025_final.csv"

LAGS = [1, 2, 3, 7, 14, 30]
WINDOWS = [7, 14, 30]

df = pd.read_csv(DATA_PATH)
df['timestamp'] = pd.to_datetime(df['timestamp'], utc=True, errors='coerce')
df['dayofyear'] = df['timestamp'].dt.dayofyear
df['dayofweek'] = df['timestamp'].dt.dayofweek
df['month'] = df['timestamp'].dt.month
df['year'] = df['timestamp'].dt.year
df['sin_doy'] = np.sin(2 * np.pi * df['dayofyear'] / 365)
df['cos_doy'] = np.cos(2 * np.pi * df['dayofyear'] / 365)

for lag in LAGS:
    df[f'lag_{lag}'] = df.groupby(['city', 'type'])['value'].shift(lag)
for w in WINDOWS:
    df[f'roll_mean_{w}'] = (
        df.groupby(['city', 'type'])['value']
          .shift(1)
          .rolling(w)
          .mean()
    )

df = df.dropna().reset_index(drop=True)

cat_features = ['city', 'type']
num_features = ['dayofweek', 'month', 'year', 'sin_doy', 'cos_doy'] + \
               [f'lag_{l}' for l in LAGS] + [f'roll_mean_{w}' for w in WINDOWS]


def predict_pulse(city: str, indicators: list[str], start_year: int, end_year: int):
    city = city.lower()
    indicators = [i.lower() for i in indicators]

    results = {}

    for indicator in indicators:
        model_path = MODEL_DIR / f"{city}_{indicator}.pkl"

        if not model_path.exists():
            raise FileNotFoundError(
                f"No saved model found for {city}-{indicator} at {model_path}"
            )

        model = joblib.load(model_path)

        future_dates = pd.date_range(
            start=f'{start_year}-01-01',
            end=f'{end_year}-12-31',
            freq='D'
        )

        future_df = pd.DataFrame({
            'timestamp': future_dates,
            'city': city,
            'type': indicator
        })

        future_df['dayofyear'] = future_df['timestamp'].dt.dayofyear
        future_df['dayofweek'] = future_df['timestamp'].dt.dayofweek
        future_df['month'] = future_df['timestamp'].dt.month
        future_df['year'] = future_df['timestamp'].dt.year
        future_df['sin_doy'] = np.sin(2 * np.pi * future_df['dayofyear'] / 365)
        future_df['cos_doy'] = np.cos(2 * np.pi * future_df['dayofyear'] / 365)

        last_values = (
            df[(df['city'] == city) & (df['type'] == indicator)]
            .sort_values('timestamp')['value']
            .values
        )

        for lag in LAGS:
            future_df[f'lag_{lag}'] = np.nan
            future_df.loc[0, f'lag_{lag}'] = last_values[-lag]

        for w in WINDOWS:
            future_df[f'roll_mean_{w}'] = np.nan
            future_df.loc[0, f'roll_mean_{w}'] = np.mean(last_values[-w:])

        future_df.fillna(method='ffill', inplace=True)

        X_future = future_df[cat_features + num_features]
        future_df['predicted'] = model.predict(X_future)

        results[indicator] = future_df[['timestamp', 'predicted']]

    return results
