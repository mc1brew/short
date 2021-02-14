using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Net.Http;



namespace Kvin.Short.Controllers
{    
    [ApiController]
    [Route("api/")]
    public class Short : ControllerBase
    {
        private readonly ILogger<Short> _logger;

        public Short(ILogger<Short> logger)
        {
            _logger = logger;
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            if(key == Constants.Favicon)
                return new OkResult();

            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync(Constants.FunctionUrls.GetLink(key)).Result;

            //The name not being found is a common case.
            //Handle it gracefully by sending them to add the key for now.
            //Later I'll add feedback to the front end.
            if(responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogInformation($"Name: {key} was not found");
                return Redirect(Constants.Configuration.CreateLinkPageUrl);
            }

            responseMessage.EnsureSuccessStatusCode();

            string getLinkResponse = responseMessage.Content.ReadAsStringAsync().Result;
            Link retrievedLink = Newtonsoft.Json.JsonConvert.DeserializeObject<Link>(getLinkResponse);

            if(string.IsNullOrEmpty(retrievedLink.TargetUrl))
            {
                //If this occurs in production that means I have missed a case for evaluating the input
                //Or alternatively someone has found a way to add data to the data store.
                //In either case this would be very bad.
                _logger.LogError($"Record for Key: {key} was found but had no corresponding url.");
                Redirect(Constants.Configuration.CreateLinkPageUrl);
            }
                
            
            //Todo: URL scrubbing and touching up will need to be more in depth later.
            if(!retrievedLink.TargetUrl.StartsWith("http"))
                retrievedLink.TargetUrl = $"https://{retrievedLink.TargetUrl}";

            return new OkObjectResult(retrievedLink);
        }

        [HttpPost]
        public IActionResult CreateLink([FromBody]Link linkToCreate)
        {
            if(linkToCreate == null)
            {
                string msg = $"{nameof(Link)} was null";
                _logger.LogWarning(msg);
                return new BadRequestObjectResult(new {error = msg});
            }
            
            if(string.IsNullOrEmpty(linkToCreate.Key))
            {
                string msg = $"{nameof(Link.Key)} was null";
                _logger.LogWarning(msg);
                return new BadRequestObjectResult(new {error = msg});
            }

            if(string.IsNullOrEmpty(linkToCreate.TargetUrl))
            {
                string msg = $"{nameof(Link.TargetUrl)} was null";
                _logger.LogWarning(msg);
                return new BadRequestObjectResult(new {error = msg});
            }

            _logger.LogInformation($"Creating {nameof(Link)} with {nameof(Link.Key)}: {linkToCreate.Key} and {nameof(Link.TargetUrl)}: {linkToCreate.TargetUrl}.");

            try{
                HttpClient client = new HttpClient();
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(linkToCreate));
                var responseMessage = client.PostAsync(Constants.FunctionUrls.CreateLink(), content).Result;
                responseMessage.EnsureSuccessStatusCode();

                //**************************************
                //          Start here Kevin!
                //**************************************

                var url = responseMessage.Content.ReadAsStringAsync().Result;

                string linkUrl = $"{Constants.Configuration.ForwardLinkUrl}/{linkToCreate.Key}";
                return new OkObjectResult(new Link{ShortenedUrl = linkUrl});
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw ex;
            }            
        }
    }
}
