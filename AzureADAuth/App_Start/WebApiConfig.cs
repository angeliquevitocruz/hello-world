using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;

namespace AzureADAuth
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Enable CORS
            config.EnableCors();

            //XML default format - enables desktop Excel "Data Connection"
            //by returning XML with no HTTP "Accept" header is given
            /*
            config.Formatters.Clear();
			config.Formatters.Add(new System.Net.Http.Formatting.XmlMediaTypeFormatter());
			config.Formatters.Add(new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            */



            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("application/octet-stream"));

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/plain"));

            config.Formatters.JsonFormatter.MediaTypeMappings
                .Add(new System.Net.Http.Formatting.RequestHeaderMapping("Accept", "text/html",
                System.StringComparison.InvariantCultureIgnoreCase, true, "application/json"));

            //config.EnableCors(new EnableCorsAttribute("", "", ""));
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

            //Microsoft defaults
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


        }
    }
}


