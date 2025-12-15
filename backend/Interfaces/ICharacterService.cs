using PruebaTecnicaCarsales.DTOs;

namespace PruebaTecnicaCarsales.Interfaces
{
    public interface ICharacterService
    {
        Task<List<CharacterDto>> GetCharactersAsync(List<string> characterUrls);
    }
}
