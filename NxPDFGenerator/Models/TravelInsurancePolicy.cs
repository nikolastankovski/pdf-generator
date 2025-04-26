namespace NxPDFGenerator.Models;

public record TravelInsurancePolicy(
    string PolicyNo, 
    string ReferenceNo, 
    string TransactionID, 
    string TypeOfPolicy, 
    string TypeOfCoverage, 
    int NoOfPeopleInsured,
    string StartDate, 
    string EndDate,
    string NumberOfDaysValid,
    string Deductible,
    string CountriesApplicable,
    string PremiumAmount
);