using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MongoDB.Driver;
using MongoDB.Bson;

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
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _databaseName = "short";
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        public Short(ILogger<Short> logger)
        {
            _logger = logger;
            _mongoClient = new MongoClient(_connectionString);
            _mongoDatabase = _mongoClient.GetDatabase(_databaseName);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Redirect("http://www.k.vin");
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var collection = _mongoDatabase.GetCollection<BsonDocument>("bar");

            var filter = Builders<BsonDocument>.Filter.Eq("Name", name);
            var document = collection.Find(filter).First();

            if(document["Url"] != null)
                return Redirect(document["Url"].ToString());
            return Redirect("http://www.k.vin");
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
                var collection = _mongoDatabase.GetCollection<BsonDocument>("bar");

                var document = new BsonDocument
                {
                    { "Name", link.Name},
                    { "Url", link.Url}
                };

                collection.InsertOne(document);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.InnerException.ToString());
                throw ex;
            }            
        }
    }
}
