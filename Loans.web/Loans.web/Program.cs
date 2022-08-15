using Loans.BL;
using Loans.Data;
using Loans.Data.Initializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    await InitializeDatabase(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task InitializeDatabase(WebApplication app)
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetService<IDBInitializer>();
        await dbInitializer!.Initialize();
        await dbInitializer!.SeedConfiguration();
        await dbInitializer!.SeedClient();
        await dbInitializer!.SeedBusiness();
        await dbInitializer!.SeedLoan();
    }
}