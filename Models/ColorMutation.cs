using colorsql.Data;
using GraphQL.Types;

namespace colorsql.Models
{

    /// {
    ///   mutation ($color:ColorType!){ create(color: $color) { id rgb } }",
    ///   "variables": {
    ///     "color": {
    ///       "rgb": "#FFFFFF"
    ///     }
    ///   }
    /// }
    public class ColorMutation : ObjectGraphType
    {
        public ColorMutation(IColorRepository colorRepository, ITranslationRepository translationRepository)
        {
            Name = "Mutation";

            Field<ColorType>(
                "create",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<ColorType>> { Name = "color" }
                ),
                resolve: context =>
                {
                    var color = context.GetArgument<Color>("color");
                    return colorRepository.Add(color);
                });
        }
    }
}