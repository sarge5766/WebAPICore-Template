using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var title = "Our versioned API";
    var descriptions = "This is a Web API that demonstrates versions";
    var terms = new Uri("https://localhost:");
    var license = new OpenApiLicense() {
        Name = "This is my license info"
    };
    var contact = new OpenApiContact() {
        Name = "Helpdesk",
        Email = "info@scottlampron.com",
        Url = new Uri("https://www.scottlampron.com")
    };

    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = $"{title} v1",
        Description = descriptions,
        TermsOfService = terms,
        License = license,
        Contact = contact
    });

});

builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options => {
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
