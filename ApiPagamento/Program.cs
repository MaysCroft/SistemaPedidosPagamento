using ApiPagamento.Data;
using ApiPagamento.Config;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});

// Configuração do Entity Framework Core para usar o SQLite como banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=pagamento.db"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de dependencia para configuração
builder.Services.Configure<ApiConfig>(
    builder.Configuration.GetSection("ApiConfig"));

var app = builder.Build();

// Configure the HTTP request pipeline.
// Url personalizada para acessar a documentação - http://localhost:5000/documentacao
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "documentacao";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API SIMI v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
