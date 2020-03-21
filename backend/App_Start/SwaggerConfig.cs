using System.Web.Http;
using WebActivatorEx;
using backend;
using Swashbuckle.Application;
using backend.SwaggerAddition;

namespace backend
{
   public class SwaggerConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "backend");
                c.DocumentFilter<AuthTokenOperation>();
                c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
            })
            .EnableSwaggerUi(c =>
            {
            });
        }
    }
}