using PruebaTecnicaCarsales.DTOs;

namespace PruebaTecnicaCarsales.Interfaces
{
    public interface ICharacterEnricher
    {
        Task EnrichEpisodesWithCharactersAsync(List<EpisodeDto> episodes);
    }
}
