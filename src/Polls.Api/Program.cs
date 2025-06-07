using Polls.Api.Setup;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;

    builder.Services
        .AddDomainSetup(configuration)
        .AddApplicationSetup(configuration)
        .AddInfrastructure(configuration)
        .AddApiSetup(configuration);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
