using AspNetCoreRateLimit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
// 
// builder.Services.AddSingleton<IRateLimitConfiguration, CustomRateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

//CASO BUSQUE AS CONFIGURACOES DO APPSETTINGS
//builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false; // Empilhamento de Requests (Incluindo as rejeitadas)
    options.HttpStatusCode = 429; //HttpCode para Too Many Requests
    options.RealIpHeader = "X-Real-IP"; //Esta bloqueando por IP
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*:/WeatherForecast",//POR 'CONTROLLER'
                //Endpoint = "*", TODOS OS ENDPOINTS
                Period = "10s",
                Limit = 2
            }
        };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseIpRateLimiting();

app.MapControllers();

app.Run();
