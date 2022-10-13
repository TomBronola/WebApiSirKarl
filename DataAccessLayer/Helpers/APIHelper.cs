using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{

    public class APIHelper
    {

        private HttpClient _apiClient;
        private string _api;


        public APIHelper()
        {
            _api = "https://192.168.2.32:30030/b1s/v1";
            InitializeClient();
        }

        private void InitializeClient()
        {
            _apiClient = new HttpClient();
            _apiClient.BaseAddress = new System.Uri(_api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }


}
}
