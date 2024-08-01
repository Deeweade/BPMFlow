using System.Text.Json.Serialization;
using BPFlow.API.Middlewares;
using BPMFlow.API.Middlewares;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Services;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Infrastructure.Data;
using BPMFlow.Infrastructure.Data.Contexts;
using BPMFlow.Infrastructure.Data.Repositories;
using BPMFlow.Infrastructure.Models.Mappings;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;

#region EnvironmentConfiguring

var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Settings");

var config = new ConfigurationBuilder()
    .SetBasePath(settingsPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string machineName = Environment.MachineName.ToLower();

var machineNames = config.GetSection("EnvironmentMachines").Get<Dictionary<string, string>>();

string environment = machineNames.FirstOrDefault(x => x.Value.ToLower() == machineName).Key ?? "Development";

#endregion

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    EnvironmentName = environment,
    ContentRootPath = settingsPath
});

builder.Configuration
    .SetBasePath(settingsPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

#region AuthenticationConfiguring

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
});

#endregion

#region ControllersConfiguring

builder.Services.AddControllers(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

#endregion

#region ContextsConfiguring

string bpmFlowContextConnectionString = builder.Configuration.GetConnectionString("BPMFlow");

builder.Services.AddDbContext<BPMFlowDbContext>(options =>
    options.UseSqlServer(bpmFlowContextConnectionString, b => b.MigrationsAssembly("BPMFlow.API")));

var perfManagement1ConnectionString = builder.Configuration.GetConnectionString("PerfManagement1");

builder.Services.AddDbContext<PerfManagement1DbContext>(options =>
    options.UseSqlServer(perfManagement1ConnectionString));

#endregion

#region DependenciesInjection

//services
builder.Services.AddScoped<IObjectRequestService, ObjectRequestService>();
builder.Services.AddScoped<IRequestStatusTransitionService, RequestStatusTransitionService>();
builder.Services.AddScoped<IEmployeeRoleService, EmployeeRoleService>();
builder.Services.AddScoped<IRequestStatusService, RequestStatusService>();

//data
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(InfrastructureMappingProfile), typeof(ApplicationMappingProfile));

#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

#region ApplicationSettingUp

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<DevAuthMiddleware>();
}

if (!app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHttpsRedirection();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();

#endregion

#region RunMigrations

//Запуск миграций при старте приложения
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<BPMFlowDbContext>();
    dbContext.Database.Migrate();
}

#endregion

app.Run();