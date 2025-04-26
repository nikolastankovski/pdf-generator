namespace NxPDFGenerator.Models;

public record Insurer(
    string OrderNumber,
    string Name,
    string EMBG,
    string PassportNo,
    string SportRisk,
    string Covid19,
    string ProfessionalDriver,
    string StudentAbroad,
    bool IsContractor
);