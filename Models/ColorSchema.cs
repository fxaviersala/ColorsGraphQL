using System;
using GraphQL;
using GraphQL.Types;

namespace colorsql.Models
{
    public class ColorSchema : Schema
    {
        public ColorSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<ColorQuery>();
        }
    }
}