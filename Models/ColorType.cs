using colorsql.Data;
using GraphQL.Types;

namespace colorsql.Models
{
    public class ColorType : ObjectGraphType<Color>
    {
        public ColorType(ITranslationRepository repo)
        {
            Field(x => x.Id).Description("Color id.");
            Field(x => x.Rgb, nullable: true).Description("RGB code.");

            Field<ListGraphType<TranslationType>>(
                "translations",
                resolve: context => repo.GetTranslationsByColor(context.Source.Id).Result
            );
        }
    }
}