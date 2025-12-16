using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Models;

namespace PruebaTecnicaCarsales.Interfaces
{
    public interface IEpisodeMapper
    {
        EpisodeDto MapToDto(Episode episode);
        List<EpisodeDto> MapToDtoList(IEnumerable<Episode> episodes);
    }
}
