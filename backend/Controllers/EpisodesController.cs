using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.DTOs;
using PruebaTecnicaCarsales.Interfaces;
using PruebaTecnicaCarsales.Models;

namespace PruebaTecnicaCarsales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        public EpisodesController(IEpisodeService episodeService)
        {
            _episodeService = episodeService ?? throw new ArgumentNullException(nameof(episodeService));
        }

        [HttpGet]
        public async Task<ActionResult<EpisodeResponse<EpisodeDto>>> GetEpisodes([FromQuery] int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }

            var episodesResponse = await _episodeService.GetEpisodesAsync(page);
            return Ok(episodesResponse);
        }
    }
}
