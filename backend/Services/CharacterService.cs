using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Interfaces;
using System.Text.Json; // Se sigue usando si es necesario para el manejo dinámico, pero GetFromJsonAsync es mejor

namespace PruebaTecnicaCarsales.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CharacterService> _logger;
        private readonly string _CharacterApiUrl;

        public CharacterService(HttpClient httpClient,
            IConfiguration configuration, ILogger<CharacterService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            _CharacterApiUrl = $"{configuration["RickAndMortyApi:BaseUrl"]}/character/"
                       ?? throw new InvalidOperationException("La configuración 'RickAndMortyApi:BaseUrl' no se encontró.");
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };


        public async Task<List<CharacterDto>> GetCharactersAsync(List<string> characterUrls)
        {
            if (characterUrls == null || !characterUrls.Any())
            {
                return new List<CharacterDto>();
            }

            try
            {
                var characterIds = characterUrls
                    .Select(url => url.Split('/').Last())
                    .ToList();

                var ids = string.Join(",", characterIds);
                var requestUri = $"{_CharacterApiUrl}{ids}";

                _logger.LogInformation("Solicitando detalles de personajes para IDs: {IDs}", ids);

                var response = await _httpClient.GetAsync(requestUri);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                if (characterIds.Count == 1)
                {
                    var character = JsonSerializer.Deserialize<CharacterDto>(content, _jsonOptions);
                    return new List<CharacterDto> { character! };
                }
                else
                {
                    var characters = JsonSerializer.Deserialize<List<CharacterDto>>(content, _jsonOptions);
                    return characters ?? new List<CharacterDto>();
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error HTTP al obtener detalles de personajes para IDs: {IDs}", string.Join(",", characterUrls.Select(url => url.Split('/').Last())));
                throw new Exception($"¡Wubba Lubba Dub Dub!, Error en la comunicación con el servicio externo (RickAndMorty API): {ex.Message}", ex);
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error de deserialización JSON al procesar detalles de personajes.");
                throw new ApplicationException("¡Wubba Lubba Dub Dub!, Error al procesar el formato de respuesta de la API externa.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error inesperado al obtener los detalles de los personajes.");
                throw new ApplicationException("¡Wubba Lubba Dub Dub!, Ocurrió un error inesperado al obtener los detalles de los personajes.", ex);
            }
        }
    }
}