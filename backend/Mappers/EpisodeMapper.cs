using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Interfaces;
using PruebaTecnicaCarsales.Models;

namespace PruebaTecnicaCarsales.Mappers
{
    public class EpisodeMapper : IEpisodeMapper
    {
        public EpisodeDto MapToDto(Episode episode)
        {
            return new EpisodeDto
            {
                Id = episode.Id,
                Name = episode.Name,
                AirDate = episode.AirDate,
                EpisodeCode = episode.EpisodeCode,
                Characters = episode.Characters,
                Url = episode.Url,
                fullCharacters = new List<CharacterDto>()
            };
        }

        public List<EpisodeDto> MapToDtoList(IEnumerable<Episode> episodes)
        {
            return episodes.Select(MapToDto).ToList();
        }
    }
}
