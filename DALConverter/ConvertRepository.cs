using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using DALConverter.Models;
using Microsoft.Extensions.Configuration;



namespace DALConverter
{

    public class ConvertRepository : IConvertRepository
    {
        public List<string>? resourcesAPI { get; set; }
        public ConvertRepository(IConfiguration configuration)
        {
            resourcesAPI = configuration.GetSection("ResourceAPIList").Get<List<string>>();
        }

        public async Task<IEnumerable<string>> Converter(string route, string format)
        {

            var client = new System.Net.Http.HttpClient();
            List<Task<string>> resourcesAPITask = new List<Task<string>>();
            try
            {
                foreach (var get in resourcesAPI)
                {
                    async Task<string> CallAPI()
                    {
                        var response = await client.GetAsync(get);
                        return await response.Content.ReadAsStringAsync();
                    }

                    resourcesAPITask.Add(CallAPI());
                }
                return await Task.WhenAll(resourcesAPITask);

            }
            catch (Exception Ex)
            {
                Debug.Write(" Error in DAL Converter function" + Ex.Message);
                return null;

            }

        }
    }
}
