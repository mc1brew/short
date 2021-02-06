using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

using System.Net.Http;



namespace Short.Controllers
{
    public class Link
    {
        public string Name {get;set;}
        public string Url {get;set;}
    }

    [ApiController]
    [Route("/")]
    public class Short : ControllerBase
    {
        private readonly ILogger<Short> _logger;

        public Short(ILogger<Short> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Redirect(Constants.Configuration.AddUrl);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            HttpClient client = new HttpClient();
            var responseMessage = client.GetAsync(Constants.FunctionUrls.GetUrl(name)).Result;
            responseMessage.EnsureSuccessStatusCode();
            var url = responseMessage.Content.ReadAsStringAsync().Result;

            if(string.IsNullOrEmpty(url))
                Redirect(Constants.Configuration.AddUrl);
            
            //Todo: URL scrubbing and touching up will need to be more in depth later.
            if(!url.StartsWith("http"))
                url = $"https://{url}";
            return Redirect(url);
        }

        [HttpPost]
        public void CreateLink([FromBody]Link link)
        {
            if(link == null)
            {
                _logger.LogError("link was null.");
                throw new ArgumentNullException("Link was null.");
            }
            
            if(string.IsNullOrEmpty(link.Name))
            {
                _logger.LogError("link.Name was null.");
                throw new ArgumentNullException("link.Name was null.");
            }

            if(string.IsNullOrEmpty(link.Url))
            {
                _logger.LogError("link.Url was null.");
                throw new ArgumentNullException("link.Url was null.");
            }

            _logger.LogInformation($"Creating link with Name: {link.Name} and Url: {link.Url}.");

            try{
                HttpClient client = new HttpClient();
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(link));
                var responseMessage = client.PostAsync(Constants.FunctionUrls.AddUrl(), content).Result;
                responseMessage.EnsureSuccessStatusCode();
                var url = responseMessage.Content.ReadAsStringAsync().Result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw ex;
            }            
        }
    }
}
