using PruebaTecnicaCarsales.Interfaces;
using PruebaTecnicaCarsales.Services;
using PruebaTecnicaCarsales.Mappers;
using PruebaTecnicaCarsales.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IEpisodeService, EpisodeService>();
builder.Services.AddHttpClient<ICharacterService, CharacterService>();
builder.Services.AddScoped<IEpisodeMapper, EpisodeMapper>();
builder.Services.AddScoped<ICharacterEnricher, CharacterEnricher>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

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
