using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestServiceCats.Models
{
    public class Repository
    {
        /* ##Metodo usado quando estava utilizando consulta de serviço Rest
        public async Task<List<Cat>> GetCats()
        {
            List<Cat> Cats;
            var URLWebAPI = "http://demos.ticapacitacion.com/cats";
            using (var Client = new HttpClient())
            {
                var JSON = await Client.GetStringAsync(URLWebAPI);
                Cats = JsonConvert.DeserializeObject<List<Cat>>(JSON);
            }
            return Cats;
        } ## */        // Metodo usado para consulta no Azure (backend app)        public async Task<List<Cat>> GetCats()
        {
            var Service = new Services.AzureService<Cat>();
            var Items = await Service.GetTable();
            return Items.ToList();
        }
    }
}
