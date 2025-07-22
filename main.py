from fastapi import FastAPI
import joblib
import pandas as pd

# 1. Create the FastAPI application
app = FastAPI()

# 2. Load the saved model and columns
model = joblib.load('churn_model.joblib')
model_columns = joblib.load('model_columns.joblib')

# 3. Define the prediction endpoint
@app.post('/predict')
def predict(data: dict):
    """
    Accepts a dictionary of customer data and returns a churn prediction.
    The input `data` should be a dictionary with keys matching the
    original features, e.g., {'gender': 'Male', 'tenure': 10, ...}.
    """
    # Convert the incoming dictionary to a pandas DataFrame
    # The `index=[0]` is used to create a single-row DataFrame
    input_data = pd.DataFrame([data])

    # One-hot encode the categorical features
    # This will add new columns for each category
    input_encoded = pd.get_dummies(input_data)

    # The key step: Reindex the encoded input to match the training columns
    # This ensures we have the exact same columns in the same order as the model was trained on.
    # `fill_value=0` handles cases where a category present in training is missing in the input.
    final_input = input_encoded.reindex(columns=model_columns, fill_value=0)

    # Make the prediction
    prediction = model.predict(final_input)
    probability = model.predict_proba(final_input)

    # Return the result
    return {
        'prediction': 'Churn' if prediction[0] == 1 else 'No Churn',
        'churn_probability': probability[0][1] # Probability of the 'Churn' class
    }