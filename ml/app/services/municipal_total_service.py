import json
import joblib
import pandas as pd
from pathlib import Path
from sqlalchemy import create_engine
from sklearn.preprocessing import StandardScaler

BASE_DIR = Path(__file__).resolve().parents[2]
MODEL_DIR = BASE_DIR / "models" / "collected_generated_total"

engine = create_engine(
    "",
    connect_args={"sslmode": "require"}
)

def forecast_total_waste(start_year, end_year, types, standardized=False):
    with open(MODEL_DIR / "metadata.json") as f:
        metadata = json.load(f)

    trained_until = metadata["trained_until_year"]

    df = pd.read_sql(
        'SELECT * FROM "Collected_and_generated_municipal_wastes";',
        engine
    )
    df = df.drop(columns=["Id"])
    df["Year"] = df["Year"].astype(int)

    results = []

    for waste_type in types:
        if waste_type == "collected":
            model = joblib.load(MODEL_DIR / metadata["models"]["collected"])
            df_type = df[df["Type"] == "Collected municipal waste"]
        else:
            model = joblib.load(MODEL_DIR / metadata["models"]["generated"])
            df_type = df[df["Type"] == "Generated municipal waste"]

        hist_end = min(end_year, trained_until)
        hist = df_type[
            (df_type["Year"] >= start_year) & (df_type["Year"] <= hist_end)
        ][["Year", "Total"]].copy()

        if end_year > trained_until:
            future_years = pd.DataFrame(
                {"Year": range(trained_until + 1, end_year + 1)}
            )
            preds = model.predict(future_years)
            forecast = pd.DataFrame({
                "Year": future_years["Year"],
                "Total": preds
            })
            combined = pd.concat([hist, forecast])
        else:
            combined = hist

        combined["Type"] = waste_type
        results.append(combined)

    final_df = pd.concat(results)

    if standardized:
        scaler = StandardScaler()
        final_df["Total"] = scaler.fit_transform(final_df[["Total"]])

    return final_df.sort_values(["Type", "Year"])
