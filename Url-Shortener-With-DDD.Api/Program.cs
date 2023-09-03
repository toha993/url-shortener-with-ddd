using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortenerWithDDD.Application;
using UrlShortenerWithDDD.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        corsPolicyBuilder => corsPolicyBuilder
            .WithOrigins("http://localhost:4200") // Replace with the origin of your Angular app
            .AllowAnyHeader()
            .AllowAnyMethod());
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddLazyCache();
builder.Services.AddMemoryCache();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
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

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
