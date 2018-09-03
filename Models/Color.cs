using System.Collections.Generic;

namespace colorsql.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Rgb { get; set; }
        public List<Translation> translations { get; set; }
    }

}