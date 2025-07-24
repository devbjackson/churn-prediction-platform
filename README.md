# Churn Prediction Platform

This repository contains a multi-service machine learning platform for predicting customer churn. The platform includes a Python-based ML service using FastAPI and scikit-learn, a C# backend API, and Docker Compose orchestration for easy deployment.

---

## Features

- **Data Preprocessing**: Handles missing values, encodes categorical features, and scales numerical features.
- **Model Training**: Logistic Regression model trained on customer churn data.
- **Python ML API**: FastAPI-based REST API for serving ML predictions.
- **C# Backend API**: ASP.NET Core backend that communicates with the Python ML service.
- **Multi-Service Architecture**: Microservices architecture with separate Python ML and C# backend services.
- **Docker Compose Support**: Complete containerization with service orchestration.
- **Production-Ready**: Saves trained model and column structure for consistent predictions on new data.

---

## Installation

### **Clone the Repository**
```bash
git clone https://github.com/your-username/churn-platform.git
cd churn-platform
```

---

## Usage

The recommended way to run the platform is with Docker Compose. This will handle everything from training the model to running the services.

### **1. Train the Model**

Run the following command to train the model using a temporary Docker container:

```bash
docker-compose run --build python-api-train
```

This will generate the `churn_model.joblib` and `model_columns.joblib` files.

### **2. Run the Platform**

Once the model is trained, start the platform:

```bash
docker-compose up --build
```

This will start:
- **Python ML API** on port `8000`
- **C# Backend API** on port `8001`

---

## Alternative: Running Services Manually

If you prefer to run the services without Docker, follow these steps:

### **1. Set Up Virtual Environment**
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

### **2. Install Dependencies**
```bash
pip install -r requirements.txt
```

### **3. Train the Model**
Run the `train.py` script to preprocess the data and generate the model files:
```bash
python train.py
```

### **4. Run the Services**

#### **Run the Python ML API**
```bash
uvicorn main:app --host 0.0.0.0 --port 8000
```

#### **Run the C# Backend API**
```bash
cd ChurnPlatform.Backend
dotnet run
```

**Note:** When running manually, the C# backend may need its configuration updated to locate the Python API at `http://localhost:8000`.

### **Make Predictions**
Send a POST request to the `/predict` endpoint with customer data:
```json
{
    "gender": "Male",
    "SeniorCitizen": 0,
    "Partner": "Yes",
    "Dependents": "No",
    "tenure": 12,
    "PhoneService": "Yes",
    "MultipleLines": "No",
    "InternetService": "DSL",
    "OnlineSecurity": "No",
    "OnlineBackup": "Yes",
    "DeviceProtection": "No",
    "TechSupport": "No",
    "StreamingTV": "Yes",
    "StreamingMovies": "No",
    "Contract": "Month-to-month",
    "PaperlessBilling": "Yes",
    "PaymentMethod": "Electronic check",
    "MonthlyCharges": 29.85,
    "TotalCharges": 29.85
}
```

---

## Docker Deployment

### **Build the Docker Image**
```bash
docker build -t churn-platform .
```

### **Run the Docker Container**
```bash
docker run -p 80:80 churn-platform
```

---

## File Structure

- **Notebook.ipynb**: Jupyter notebook for data preprocessing and model training.
- **main.py**: FastAPI application for serving predictions.
- **requirements.txt**: List of dependencies.
- **Dockerfile**: Docker configuration for containerizing the application.
- **docker-compose.yaml**: Docker Compose configuration for multi-service deployment.
- **ChurnPlatform.Backend/**: C# ASP.NET Core backend service.
  - **Program.cs**: Main application entry point.
  - **Controllers/PredictionController.cs**: API controller for handling prediction requests.
  - **Dockerfile**: Docker configuration for the C# backend.
- **.gitignore**: Files and directories to ignore in version control.