using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Interfaces;

namespace PruebaTecnicaCarsales.Services
{
    public class CharacterEnricher : ICharacterEnricher
    {
        private readonly ICharacterService _characterService;
        private readonly ILogger<CharacterEnricher> _logger;

        public CharacterEnricher(ICharacterService characterService, ILogger<CharacterEnricher> logger)
        {
            _characterService = characterService;
            _logger = logger;
        }

        public async Task EnrichEpisodesWithCharactersAsync(List<EpisodeDto> episodes)
        {
            var enrichmentTasks = episodes.Select(async episode =>
            {
                if (episode.Characters != null && episode.Characters.Any())
                {
                    try 
                    {
                        episode.fullCharacters = await _characterService.GetCharactersAsync(episode.Characters);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "No se han podido obtener los personajes del episodio {EpisodeCode}", episode.EpisodeCode);
                        episode.fullCharacters = new List<CharacterDto>();
                    }
                }
            });

            await Task.WhenAll(enrichmentTasks);
        }
    }
}
