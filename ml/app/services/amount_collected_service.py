import pandas as pd
import joblib
import json
from pathlib import Path
from sqlalchemy import create_engine
from sklearn.preprocessing import StandardScaler

from pathlib import Path

BASE_DIR = Path(__file__).resolve().parents[2]
MODEL_DIR = BASE_DIR / "models" / "Amount_of_collected_municipal_wastes"

engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

def forecast_amount_collected(start_year, end_year, columns, standardized):
    with open(MODEL_DIR / "metadata.json") as f:
        metadata = json.load(f)

    trained_until = metadata["trained_until_year"]
    all_targets = metadata["targets"]
    lag_features = metadata["features"]

    df = pd.read_sql(
        'SELECT * FROM "Amount_of_collected_municipal_wastes";',
        engine
    )
    df = df.drop(columns=["Id"])
    df["Year"] = df["Year"].astype(int)
    df = df.sort_values("Year").reset_index(drop=True)

    hist_end = min(end_year, trained_until)

    historical = df[
        (df["Year"] >= start_year) & (df["Year"] <= hist_end)
    ][["Year"] + columns]

    if end_year <= trained_until:
        return _standardize(historical, standardized)

    models = {
        col: joblib.load(MODEL_DIR / f"{col}.pkl")
        for col in all_targets
    }

    last_row = (
        df[df["Year"] == trained_until][all_targets]
        .values
        .flatten()
    )

    future_rows = []

    for year in range(trained_until + 1, end_year + 1):
        X_input = pd.DataFrame([last_row], columns=lag_features)

        next_values = []
        for target in all_targets:
            pred = models[target].predict(X_input)[0]
            next_values.append(pred)

        future_rows.append(
            {"Year": year, **dict(zip(all_targets, next_values))}
        )
        last_row = next_values

    forecast_df = pd.DataFrame(future_rows)[["Year"] + columns]

    result = pd.concat([historical, forecast_df], ignore_index=True)
    return _standardize(result, standardized)


def _standardize(df: pd.DataFrame, standardized: bool) -> pd.DataFrame:
    if not standardized:
        return df

    scaler = StandardScaler()
    values = scaler.fit_transform(df.drop(columns=["Year"]))

    scaled = pd.DataFrame(values, columns=df.columns.drop("Year"))
    scaled.insert(0, "Year", df["Year"].values)
    return scaled
