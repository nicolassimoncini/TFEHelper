using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Filters;

namespace TFEHelper.Backend.API.Examples.Publications
{
    public class PatchPublicationRequestExample : IMultipleExamplesProvider<List<Operation>>
    {
        public IEnumerable<SwaggerExample<List<Operation>>> GetExamples()
        {
            yield return SwaggerExample.Create(
                "add",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "add",
                        path= "/key",
                        value = "new-key"
                    }
                });

            yield return SwaggerExample.Create(
                "remove",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "remove",
                        path= "/key"
                    }
                });

            yield return SwaggerExample.Create(
                "replace",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "replace",
                        path= "/key",
                        value = "replaced-key"
                    }
                });

            yield return SwaggerExample.Create(
                "move",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "move",
                        from= "/Publications/1/key",
                        path = "/key"
                    }
                });

            yield return SwaggerExample.Create(
                "copy",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "copy",
                        from= "/Publications/1/key",
                        path = "/key"
                    }
                });

            yield return SwaggerExample.Create(
                "test",
                new List<Operation>
                {
                    new Operation()
                    {
                        op = "test",
                        path= "/key",
                        value = "test-key"
                    }
                });
        }
    }
}
