using System.Collections.Generic;
using colorsql.Data;
using GraphQL.Types;

namespace colorsql.Models
{
    public class ColorQuery : ObjectGraphType
    {
        public ColorQuery(IColorRepository colorRepository, ITranslationRepository translationRepository)
        {
            Field<TranslationType>(
                "translation",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Category id" }
                ),
                resolve: context => translationRepository.GetTranslationAsync(context.GetArgument<int>("id")).Result
            );

            Field<ColorType>(
                "color",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "id", Description = "Product id" }
                ),
                resolve: context => colorRepository.GetColorAsync(context.GetArgument<int>("id")).Result
            );
            Field<ListGraphType<ColorType>>(
                "colors",
                resolve: context => colorRepository.ColorsAsync()
            );
        }
    }
}