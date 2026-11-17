using MySqlConnector;
using System.Data;
using UrbanStockApi.Query.Implements;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Implements;
using UrbanStockApi.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión a MySQL
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("MySqlConnection")));

// Registro de Queries
builder.Services.AddScoped<ICategoriaQueries, CategoriaQueries>();
builder.Services.AddScoped<IProveedorQueries, ProveedorQueries>();
builder.Services.AddScoped<IClienteQueries, ClienteQueries>();
builder.Services.AddScoped<IUsuarioQueries, UsuarioQueries>();
builder.Services.AddScoped<IProductoQueries, ProductoQueries>();
builder.Services.AddScoped<IProductoProveedorQueries, ProductoProveedorQueries>();
builder.Services.AddScoped<IVentaQueries, VentaQueries>();
builder.Services.AddScoped<IDetalleVentaQueries, DetalleVentaQueries>();

// Registro de Repositories
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoProveedorRepository, ProductoProveedorRepository>();
builder.Services.AddScoped<IVentaRepository, VentaRepository>();
builder.Services.AddScoped<IDetalleVentaRepository, DetalleVentaRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFront", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirFront");
app.UseAuthorization();
app.MapControllers();
app.Run();