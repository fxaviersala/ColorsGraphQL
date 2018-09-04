using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using colorsql.Models;
using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation.Complexity;
using Microsoft.AspNetCore.Mvc;

namespace colorsql.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : Controller
    {

        private readonly IDocumentExecuter _documentExecuter;
        private readonly IDocumentWriter _writer;
        private readonly ISchema _schema;

        public ColorController(IDocumentExecuter documentExecuter,
                               IDocumentWriter writer,
                               ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _writer = writer;
            _schema = schema;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            var inputs = query.Variables.ToInputs();
            var queryToExecute = query.Query;

            try
            {
                var result = await _documentExecuter.ExecuteAsync(_ =>
                {
                    _.Schema = _schema;
                    _.Query = queryToExecute;
                    _.OperationName = query.OperationName;
                    _.Inputs = inputs;

                    _.ComplexityConfiguration = new ComplexityConfiguration { MaxDepth = 15 };
                    _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();

                }).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                {
                    return BadRequest(result);
                }

                var json = _writer.Write(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

    public class GraphQLQuery
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public Newtonsoft.Json.Linq.JObject Variables { get; set; }
    }
}
