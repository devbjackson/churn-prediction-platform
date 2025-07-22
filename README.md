# Churn Prediction Platform

This repository contains a machine learning platform for predicting customer churn using FastAPI and scikit-learn. The project includes data preprocessing, model training, and deployment of a REST API for real-time predictions.

---

## Features

- **Data Preprocessing**: Handles missing values, encodes categorical features, and scales numerical features.
- **Model Training**: Logistic Regression model trained on customer churn data.
- **API Deployment**: FastAPI-based REST API for serving predictions.
- **Docker Support**: Containerized application for easy deployment.
- **Production-Ready**: Saves trained model and column structure for consistent predictions on new data.

---

## Installation

### **Clone the Repository**
```bash
git clone https://github.com/your-username/churn-platform.git
cd churn-platform
```

### **Set Up Virtual Environment**
```bash
python -m venv venv
source venv/bin/activate  # On Windows: venv\Scripts\activate
```

### **Install Dependencies**
```bash
pip install -r requirements.txt
```

---

## Usage

### **Train the Model**
Run the Jupyter notebook (`Notebook.ipynb`) to preprocess the data, train the model, and save the trained model and column structure:
```bash
jupyter notebook Notebook.ipynb
```

### **Run the API**
Start the FastAPI application:
```bash
uvicorn main:app --host 0.0.0.0 --port 80
```

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
- **.gitignore**: Files and directories to ignore in version control.

---

## Contributing

Feel free to submit issues or pull requests to improve the project.

---

## License

This project is licensed under the MIT License.