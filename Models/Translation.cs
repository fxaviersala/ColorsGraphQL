
namespace colorsql.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }

        public Color Color { get; set; }
    }
}