using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsracardTest.Models;
using IsracardTest.Utility;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace IsracardTest.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private const string BOOKMARK_SESSION_KEY = "bookmark_session_key";
        private IDataProvider _dataProvider;

        public SampleDataController()
        {
            _dataProvider = new DataProvider();
        }

        [HttpGet("[action]/{val}")]
        public IEnumerable<CustomCard> CardsForcasts(string val)
        {
            if (_dataProvider == null)
                return Enumerable.Empty<CustomCard>();

            var result = _dataProvider.GetBySearchString(val);

            return result;
        }

        [HttpGet("[action]")]
        public IEnumerable<CustomCard> CardFromsession()
        {
            var json = HttpContext.Session.GetString(BOOKMARK_SESSION_KEY);
            var collection = String.IsNullOrEmpty(json) ? new List<CustomCard>() : JsonConvert.DeserializeObject<List<CustomCard>>(json);

            return collection;
        }

        [HttpPost("[action]")]
        public void CardToSession([FromBody] CustomCard card)
        {
            var json = HttpContext.Session.GetString(BOOKMARK_SESSION_KEY);
            var collection = String.IsNullOrEmpty(json) ? new List<CustomCard>() : JsonConvert.DeserializeObject<List<CustomCard>>(json);

            collection.Add(card);
            json = JsonConvert.SerializeObject(collection, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Session.SetString(BOOKMARK_SESSION_KEY, json);
        }
    }
}
