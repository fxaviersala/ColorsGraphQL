
using System.Collections.Generic;
using System.Threading.Tasks;
using colorsql.Models;

namespace colorsql.Data
{
    public interface ITranslationRepository
    {
        Task<List<Translation>> GetTranslationsAsync();
        Task<Translation> GetTranslationAsync(int id);
        Task<List<Translation>> GetTranslationsByColor(int colorId);
    }
}