
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colorsql.Models;

namespace colorsql.Data
{
    class TranslationRepository : ITranslationRepository
    {
        private readonly ColorsContext _context;

        public TranslationRepository(ColorsContext context)
        {
            _context = context;
        }
        public async Task<Translation> GetTranslationAsync(int id) => await _context.Translations.FindAsync(id);

        public Task<List<Translation>> GetTranslationsAsync()
        {
            return Task.FromResult(_context.Translations.ToList());
        }

        public Task<List<Translation>> GetTranslationsByColor(int colorId)
        {
            return Task.FromResult(_context.Translations.Where(col => col.Color.Id == colorId).ToList());
        }
    }
}