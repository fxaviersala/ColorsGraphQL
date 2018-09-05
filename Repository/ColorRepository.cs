using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colorsql.Models;

namespace colorsql.Data
{
    public class ColorRepository : IColorRepository
    {

        private readonly ColorsContext _context;

        public ColorRepository(ColorsContext context)
        {
            _context = context;
        }

        public async Task Add(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();
        }

        public Task<List<Color>> ColorsAsync()
        {
            return Task.FromResult(_context.Colors.ToList());
        }

        public async Task<Color> GetColorAsync(int id) => await _context.Colors.FindAsync(id);

    }

}