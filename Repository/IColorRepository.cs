using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using colorsql.Models;

namespace colorsql.Data
{
    public interface IColorRepository
    {
        Task<List<Color>> ColorsAsync();
        Task<Color> GetColorAsync(int id);
        Task Add(Color color);
    }
}
