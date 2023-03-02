using DALConverter;
using DALConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using System.Data;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Diagnostics;

namespace BLConverter.Services
{
    public class ConvertService : IConvertService
    {
        IConvertRepository convertRepository;
        public ConvertService(IConvertRepository convertRepository)
        {
            this.convertRepository = convertRepository;
        }

        List<DisplayDetails> responseAPICallList = new List<DisplayDetails>();

        public async Task<List<DisplayDetails>> Convert(string route, string format)
        {
            try
            {
                var response = this.convertRepository.Converter(route, format).Result.ToList();
                foreach (var item in response)
                {
                    var postResponse = item;
                    MapResponse(postResponse);
                }
                WriteToTheFile(route, format);
                return responseAPICallList;

            }
            catch (Exception Ex)
            {
                Debug.Write(" Error in BL Convert function" + Ex.Message);
                return null;
            }
        }
        public void MapResponse(string postResponse)
        {
            try
            {
                DisplayDetails displayDetails;
                JObject jsonObject = JObject.Parse(postResponse);
                if (jsonObject.TryGetValue("users", out JToken nameToken))
                {
                    foreach (var item in nameToken)
                    {
                        displayDetails = new DisplayDetails();
                        displayDetails.firstName = item["firstName"].ToString();
                        displayDetails.LastName = item["lastName"].ToString();
                        displayDetails.email = item["email"].ToString();
                        displayDetails.sourceId = item["id"].ToString();
                        responseAPICallList.Add(displayDetails);
                    }
                }
                if (jsonObject.TryGetValue("results", out JToken name_Token))
                {
                    foreach (var item in name_Token)
                    {
                        displayDetails = new DisplayDetails();
                        displayDetails.firstName = item["name"]["first"].ToString();
                        displayDetails.LastName = item["name"]["last"].ToString();
                        displayDetails.email = item["email"].ToString();
                        displayDetails.sourceId = item["id"]["value"].ToString();
                        responseAPICallList.Add(displayDetails);
                    }
                }
            }
            catch (Exception Ex)
            {
                Debug.Write("Error in BL MapResponse function " +  Ex.Message);
            }

        }
        public void WriteToTheFile(string route, string format)
        {
            try
            {
                string fileName = "Converter" + "." + format;
                route =route + fileName;
                var json = JsonConvert.SerializeObject(responseAPICallList);
                using (var createStream = File.Create(route))
                {
                    createStream.Close();
                    File.WriteAllText(route, json);
                }
            }

            catch (Exception Ex)
            {
                Debug.Write("Error in BL WriteToTheFile function " + Ex.Message);
            }
        }
    }
}

