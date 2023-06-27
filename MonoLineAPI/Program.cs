using Microsoft.EntityFrameworkCore;
using MonoLineAPI.Mapper;
using MonolineInfraestructure;
using MonolineInfraestructure.context;
using MonolineInfraestructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//dependecy inyection
builder.Services.AddScoped<IUserInfreaestructure, UserMySQLInfraestructure>();
builder.Services.AddScoped<IClientInfraestructure, ClientMySQLInfraestructure>();
builder.Services.AddScoped<ICreditInfraestructure, CreditMySQLInfraestructure>();
builder.Services.AddScoped<IPropertyInfraestructure, PropertyMySQLInfraestructure>();
builder.Services.AddScoped<IFlowsInfraestructure, FlowsMySQLInfraestructure>();


//conection to MySQL
var connectionString = builder.Configuration.GetConnectionString("monolineConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

builder.Services.AddDbContext<MonolineDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString),
            options => options.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: System.TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null)
        );
    });

builder.Services.AddAutoMapper(
    typeof(DatasToCredit)
);


var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<MonolineDBContext>())
{
    context.Database.EnsureCreated();
}
//Cors
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();