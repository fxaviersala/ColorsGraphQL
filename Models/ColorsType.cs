using GraphQL.Types;

namespace colorsql.Models
{
    public class ColorsType : ObjectGraphType<ColorsList>
    {
        public ColorsType()
        {
            Field<ListGraphType<ColorType>>("colors");
        }
    }
}