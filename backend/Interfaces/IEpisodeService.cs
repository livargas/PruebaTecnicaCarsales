using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Models;

namespace PruebaTecnicaCarsales.Interfaces
{
    public interface IEpisodeService
    {
        Task<EpisodeResponse<EpisodeDto>> GetEpisodesAsync(int pageNumber);
    }
}
