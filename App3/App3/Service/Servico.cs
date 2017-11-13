using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using App3.Model;

namespace App3.Service
{
   public class Servico
   {
        private HttpClient _client = new HttpClient();
        private List<UsuarioModel> _usuarios;

        public async Task<List<UsuarioModel>> PegarUsuariosAsync()
        {
            try
            {
                string url = string.Format("https://jsonplaceholder.typicode.com/users/");
                var resposta = await _client.GetAsync(url);
                if (resposta.StatusCode == HttpStatusCode.NotFound)
                {
                    _usuarios = new List<UsuarioModel>();
                }
                else
                {
                    var content = await resposta.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<List<UsuarioModel>>(content);
                    _usuarios = new List<UsuarioModel>(json);
                }
                return _usuarios;
                
            }
            catch (Exception)
            {

                throw;
            }
        }
   }
}
