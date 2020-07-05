using System;
using System.Collections.Generic;
using System.IdentityModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Identity.Client;

namespace AzureADAuth.Controllers
{
    // CORS - Enable HTTP calls from any source URL
    //      - To allow specific caller DNS domains only use this syntax:
    //        (origins: "http://domain1, http://domain1",
    [EnableCors(origins: "*",
        headers: "*",
        methods: "*",
        SupportsCredentials = true)]
    //[Authorize]
    public class AzureADAuthController : ApiController
    {
        // GET api/values
        public async Task<string> Get()
        {
            //Parse parameters
            string username = "cpfisystest3@centurypacific.com.ph";
            string password = "$yst3m32";
            string clientid = "664a86e5-c67c-4d03-98d9-7f747a878786";
            string tenant = "1aacd6e9-0cf9-43d9-b02a-1624a8b85ecd";
            
            //open connection
            string authority = "https://login.microsoftonline.com/" + tenant;
            string[] scopes = new string[] { "user.read" };
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(clientid)
                .WithAuthority(authority)
                .Build();
            var securePassword = new SecureString();
            foreach (char c in password.ToCharArray())
                securePassword.AppendChar(c);
            var result = await app.AcquireTokenByUsernamePassword(scopes, username, securePassword).ExecuteAsync();


            // Return
            return result.IdToken; 
            //return "token acquiring...";
        }

        // GET api/values/5
        public string Get(int id)
        {
            DateTime now = DateTime.Now;
            return id.ToString() + " " + now.ToLongDateString() + " " + now.ToLongTimeString();
        }

        // POST api/values
        public async Task<string> Post([FromBody]AuthParams authparam)
        {
            //Parse parameters
            string username = authparam.username;
            string password = authparam.password;
            string clientid = authparam.clientid;
            string tenant = authparam.tenant;

            Console.WriteLine(authparam.username);

            //open connection
            string authority = "https://login.microsoftonline.com/" + tenant;
            string[] scopes = new string[] { "user.read" };
            IPublicClientApplication app;
            app = PublicClientApplicationBuilder.Create(clientid)
                .WithAuthority(authority)
                .Build();
            var securePassword = new SecureString();
            foreach (char c in password.ToCharArray())
                securePassword.AppendChar(c);
            var result = await app.AcquireTokenByUsernamePassword(scopes, username, securePassword).ExecuteAsync();

            return result.IdToken;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class AuthParams
    {
        public string tenant;
        public string clientid;
        public string username;
        public string password;
    }
}
