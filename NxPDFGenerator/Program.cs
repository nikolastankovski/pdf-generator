using Microsoft.EntityFrameworkCore;
using NxPDFGenerator.DbContexts;
using NxPDFGenerator.Middleware;
using QuestPDF.Infrastructure;
using QuestPDF;
using Serilog;
using NxPDFGenerator.Models;
using NxPDFGenerator.Reports;
using QuestPDF.Companion;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NxPDFGeneratorDbContext>(options => options.UseNpgsql(connectionString));

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Settings.License = LicenseType.Community;

// For documentation and implementation details, please visit:
// https://www.questpdf.com/getting-started.html

var insurers = new List<Insurer>()
{
    new Insurer("1", "John Smith 1", "1111111111111", "11111111", "/", "/", "/", "/", true),
    new Insurer("2", "John Smith 2", "2222222222222", "22222222", "/", "/", "/", "/", false),
    new Insurer("3", "John Smith 3", "3333333333333", "33333333", "/", "/", "/", "/", false)
};

var travelInsurance = new TravelInsurancePolicy(
    PolicyNo: "PZO 080224",
    ReferenceNo: "114-210224",
    TransactionID: "C-372a56d966d322118fde",
    TypeOfPolicy: "Индивидуално",
    TypeOfCoverage: "PLATINUM",
    NoOfPeopleInsured: 3,
    StartDate: "23.07.2022",
    EndDate: "31.07.2022",
    NumberOfDaysValid: "9",
    Deductible: "Без Франшиза / No deductible",
    CountriesApplicable: "Valid for all countries",
    PremiumAmount: "1274 ден"
);

var document = new rpt_Invoice_SavaInsurance(travelInsurance, insurers);

document.ShowInCompanion();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterEndpoints();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();