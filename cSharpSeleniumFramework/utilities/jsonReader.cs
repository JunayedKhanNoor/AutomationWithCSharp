using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cSharpSeleniumFramework.utilities
{
    public class jsonReader
    {
        public jsonReader()
        {

        }
        public string extractData(string tokenName)
        {
            string myJsonString = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }
        public string[] extractDataArray(string tokenName)
        {
            string myJsonString = File.ReadAllText("utilities/testData.json");
            var jsonObject = JToken.Parse(myJsonString);
            List<String> productsList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productsList.ToArray();
        }
    }
}
