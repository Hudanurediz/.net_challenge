using Enoca_Challenge.Persistance;
using Enoca_Challenge.Application;
using Hangfire;
using Enoca_Challenge.WebApi.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationService();

var hangfire_connection_string = builder.Configuration.GetConnectionString("mssql_hangfire");

builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(hangfire_connection_string);
});

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire_dashboard");

RecurringJob.AddOrUpdate<ReportGenerationJob>(
    "hourly-report-job",
    j => j.ReportGenerationHourlyJob(),
    "0 * * * *");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
