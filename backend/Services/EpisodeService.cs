using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging; 
using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Interfaces;
using PruebaTecnicaCarsales.Models;
using System.Net.Http.Json;

namespace PruebaTecnicaCarsales.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly HttpClient _httpClient;

        private readonly IEpisodeMapper _episodeMapper;
        private readonly ICharacterEnricher _episodeEnricher;
        private readonly ILogger<EpisodeService> _logger;
        private readonly string _baseUrl;

        public EpisodeService(
            HttpClient httpClient,
            IConfiguration configuration,
            IEpisodeMapper episodeMapper,
            ICharacterEnricher episodeEnricher,
            ILogger<EpisodeService> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _episodeMapper = episodeMapper ?? throw new ArgumentNullException(nameof(episodeMapper));
            _episodeEnricher = episodeEnricher ?? throw new ArgumentNullException(nameof(episodeEnricher));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _baseUrl = configuration["RickAndMortyApi:BaseUrl"]
                       ?? throw new InvalidOperationException("La configuración 'RickAndMortyApi:BaseUrl' no se encontró.");
        }

        public async Task<EpisodeResponse<EpisodeDto>> GetEpisodesAsync(int pageNumber)
        {
            try
            {
                var requestUri = $"{_baseUrl}/episode?page={pageNumber}";

                _logger.LogInformation("Solicitando episodios de la API: {Uri}", requestUri);

                var data = await _httpClient.GetFromJsonAsync<ApiResponse<Episode>>(requestUri);

                if (data?.Results == null)
                {
                    _logger.LogWarning("La respuesta de la API no contiene resultados para la página {Page}.", pageNumber);
                    return new EpisodeResponse<EpisodeDto>
                    {
                        CurrentPage = pageNumber,
                        TotalPages = 0,
                        TotalItems = 0,
                        Episodes = new List<EpisodeDto>()
                    };
                }

                var episodes = _episodeMapper.MapToDtoList(data.Results);

                await _episodeEnricher.EnrichEpisodesWithCharactersAsync(episodes);

                return new EpisodeResponse<EpisodeDto>
                {
                    CurrentPage = pageNumber,
                    TotalPages = data.Info?.Pages ?? 0,
                    TotalItems = data.Info?.Count ?? 0,
                    Episodes = episodes
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error HTTP al obtener los episodios de la API. URI: {Uri}", $"{_baseUrl}/episode?page={pageNumber}");
                throw new Exception($"¡Wubba Lubba Dub Dub!, Error en la comunicación con el servicio externo (RickAndMorty API)", ex);
            }
        }
    }
}
