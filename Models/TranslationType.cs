using colorsql.Data;
using GraphQL.Types;

namespace colorsql.Models
{
    public class TranslationType : ObjectGraphType<Translation>
    {

        public TranslationType(IColorRepository repo)
        {
            Field(x => x.Id).Description("Translation id.");
            Field(x => x.Language).Description("Language");
            Field(x => x.Name, nullable: true).Description("Translation text.");

            Field<ColorType>(
                "color",
                resolve: context => repo.GetColorAsync(context.Source.ColorId).Result
            );
        }
    }
}