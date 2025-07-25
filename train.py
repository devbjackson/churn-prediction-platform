import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
import joblib

# Load the dataset
df = pd.read_csv('WA_Fn-UseC_-Telco-Customer-Churn.csv')

# Preprocessing
df['TotalCharges'] = pd.to_numeric(df['TotalCharges'], errors='coerce')
df.dropna(inplace=True)
df['Churn'] = df['Churn'].apply(lambda x: 1 if x == 'Yes' else 0)

# Define features (X) and target (y)
X = df.drop(['customerID', 'Churn'], axis=1)
y = df['Churn']

# One-hot encode the features
X_encoded = pd.get_dummies(X, drop_first=True)

# Save the columns of the encoded data
model_columns = X_encoded.columns
joblib.dump(model_columns, 'model_columns.joblib')

# Train the model on the entire dataset
model = LogisticRegression(max_iter=5000)
model.fit(X_encoded, y)

# Save the final model
joblib.dump(model, 'churn_model.joblib')

print("Model and columns saved successfully.")
