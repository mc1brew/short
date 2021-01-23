using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MongoDB.Driver;
using MongoDB.Bson;

namespace kvinShort.Controllers
{
    public class Link
    {
        public string Name;
        public string Url;
    }

    [ApiController]
    [Route("/")]
    public class Short : ControllerBase
    {
        // private readonly ILogger<WeatherForecastController> _logger;
        private readonly string _connectionString = "mongodb://localhost:27017";
        private readonly string _databaseName = "short";
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;

        // public WeatherForecastController(ILogger<WeatherForecastController> logger)
        // {
        //     _logger = logger;
        // }

        public Short()
        {
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

            var filter = Builders<BsonDocument>.Filter.Eq("name", name);
            var document = collection.Find(filter).First();

            if(document["url"] != null)
                return Redirect(document["url"].ToString());
            return Redirect("http://www.k.vin");
        }

        [HttpPost]
        public void CreateLink(Link link)
        {
            var collection = _mongoDatabase.GetCollection<BsonDocument>("bar");
            
            //TODO: Figure out what's wrong with declaring bson's.
            // BsonDocument doc = new BsonDocument("$set", Link.To");
            // doc.Add(new BsonElement("Name", link.Name));
            // doc.Add(new BsonElement("Url", link.Url));
            // {
            //     { "Name", link.Name},
            //     { "Url", link.Url}
            // };

            // collection.InsertOne(doc);
        }
    }
}
