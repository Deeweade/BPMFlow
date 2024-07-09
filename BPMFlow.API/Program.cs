using System.Text.Json.Serialization;
using BPFlow.API.Middlewares;
using BPMFlow.API.Middlewares;
using Microsoft.AspNetCore.Authentication.Negotiate;

#region EnvironmentConfiguring

var settingsPath = Path.Combine(Directory.GetCurrentDirectory(), "Settings");

var config = new ConfigurationBuilder()
    .SetBasePath(settingsPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
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

app.Run();