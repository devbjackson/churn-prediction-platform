using Swashbuckle.AspNetCore.Filters;
using ChurnPlatform.Backend.DTOs;

namespace ChurnPlatform.Backend.SwaggerExamples;

public class CustomerDataRequestExample : IExamplesProvider<CustomerDataDto>
{
    public CustomerDataDto GetExamples()
    {
        return new CustomerDataDto
        {
            Gender = "Female",
            SeniorCitizen = 0,
            Partner = "Yes",
            Dependents = "No",
            Tenure = 1,
            PhoneService = "No",
            MultipleLines = "No phone service",
            InternetService = "DSL",
            OnlineSecurity = "No",
            OnlineBackup = "Yes",
            DeviceProtection = "No",
            TechSupport = "No",
            StreamingTV = "No",
            StreamingMovies = "No",
            Contract = "Month-to-month",
            PaperlessBilling = "Yes",
            PaymentMethod = "Electronic check",
            MonthlyCharges = 29.85m,
            TotalCharges = 29.85m
        };
    }
}