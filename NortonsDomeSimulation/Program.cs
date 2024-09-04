using NortonsDomeSimulation.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<INortonDomeService, NortonsDomeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", builder =>
    {
        builder.WithOrigins("http://localhost:7076")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowVueApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
