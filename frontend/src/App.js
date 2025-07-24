import React, { useState, useEffect } from 'react';
import './App.css';

// This is our static, sample customer for the first button
const sampleCustomer = {
  gender: "Female",
  SeniorCitizen: 0,
  Partner: "Yes",
  Dependents: "No",
  tenure: 1,
  PhoneService: "No",
  MultipleLines: "No phone service",
  InternetService: "DSL",
  OnlineSecurity: "No",
  OnlineBackup: "Yes",
  DeviceProtection: "No",
  TechSupport: "No",
  StreamingTV: "No",
  StreamingMovies: "No",
  Contract: "Month-to-month",
  PaperlessBilling: "Yes",
  PaymentMethod: "Electronic check",
  MonthlyCharges: 29.85,
  TotalCharges: 29.85
};

// Helper function to create a random customer
const generateRandomData = () => {
  const randomTenure = Math.floor(Math.random() * 72) + 1;
  const monthlyCharges = (Math.random() * 100 + 20).toFixed(2);
  const totalCharges = (monthlyCharges * randomTenure * (Math.random() * 0.5 + 0.75)).toFixed(2);

  return {
    ...sampleCustomer, // Start with the base sample
    gender: Math.random() > 0.5 ? "Male" : "Female",
    SeniorCitizen: Math.round(Math.random()),
    tenure: randomTenure, // Override with random values
    MonthlyCharges: parseFloat(monthlyCharges),
    TotalCharges: parseFloat(totalCharges),
    Partner: Math.random() > 0.5 ? "Yes" : "No",
    InternetService: ["DSL", "Fiber optic", "No"][Math.floor(Math.random() * 3)],
  };
};

function App() {
  const [customerData, setCustomerData] = useState(sampleCustomer);
  const [prediction, setPrediction] = useState(null);
  const [error, setError] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleInputChange = (e) => {
    const { name, value, type } = e.target;
    // Handle numeric inputs
    const processedValue = type === 'number' ? parseFloat(value) : value;
    setCustomerData({
      ...customerData,
      [name]: processedValue,
    });
  };

  const loadRandomData = () => {
    setCustomerData(generateRandomData());
  };
  
  const loadSampleData = () => {
    setCustomerData(sampleCustomer);
  };

  const getPrediction = async () => {
    setError('');
    setPrediction(null);
    setIsLoading(true);

    // Convert numeric fields from strings back to numbers if needed
    const dataToSend = {
      ...customerData,
      SeniorCitizen: Number(customerData.SeniorCitizen),
      tenure: Number(customerData.tenure),
      MonthlyCharges: Number(customerData.MonthlyCharges),
      TotalCharges: Number(customerData.TotalCharges),
    };

    try {
      const response = await fetch('http://localhost:8001/Prediction', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(dataToSend),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP error! status: ${response.status}, message: ${errorText}`);
      }

      const data = await response.json();
      setPrediction(data);
    } catch (e) {
      console.error(e);
      setError(`Failed to fetch prediction. ${e.message}`);
    } finally {
      setIsLoading(false);
    }
  };

  const renderFormInputs = () => {
    return Object.keys(customerData).map((key) => {
      const value = customerData[key];
      const type = (typeof value === 'number') ? 'number' : 'text';
      
      return (
        <div className="form-group" key={key}>
          <label htmlFor={key}>{key.replace(/([A-Z])/g, ' $1')}</label>
          <input
            type={type}
            id={key}
            name={key}
            value={value}
            onChange={handleInputChange}
            step={type === 'number' ? 'any' : undefined}
          />
        </div>
      );
    });
  };


  return (
    <div className="App">
      <header className="App-header">
        <h1>Customer Churn Prediction</h1>
      </header>
      <main className="main-content">
        <div className="card customer-data-form">
          <h2>Customer Data</h2>
          <div className="form-controls">
            <button onClick={loadSampleData} disabled={isLoading}>Load Sample Customer</button>
            <button onClick={loadRandomData} disabled={isLoading}>Load Random Customer</button>
          </div>
          <div className="form-grid">
            {renderFormInputs()}
          </div>
        </div>

        <div className="card">
          <h2>How It Works</h2>
          <p>
            This prediction is powered by a machine learning model trained on historical customer data. The model analyzes various customer attributes—such as tenure, contract type, and services used—to identify patterns that are highly correlated with churn. When you provide a customer's data, the model calculates the probability of that customer churning based on these learned patterns.
          </p>
        </div>

        <div className="prediction-section">
          <div className="card">
            <h2>Prediction Controls</h2>
            <button onClick={getPrediction} disabled={isLoading} className="predict-button">
              {isLoading ? 'Getting Prediction...' : 'Get Prediction'}
            </button>
            {error && <p className="error-message">{error}</p>}
          </div>

          {prediction && (
            <div className="card prediction-result" style={{ marginTop: '20px' }}>
              <h3>Prediction Result</h3>
              <div className="result-content">
                 <p>Churn Prediction: <span>{prediction.prediction}</span></p>
                 <p>Confidence Score: <span>{(prediction.churn_probability * 100).toFixed(2)}%</span></p>
              </div>
            </div>
          )}
        </div>
      </main>
    </div>
  );
}

export default App;