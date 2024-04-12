using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using OpenAI_API;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;


namespace IntegrationWithChatGpt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private IConfiguration _configuration { get; }
        private readonly string _apiKey = "";
        public ChatGptController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost("Pergunta")]
        public async Task<IActionResult> GetPergunta(string pergunta)
        {
            try
            {
                var result = "";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
                    var response = await client.PostAsync("https://api.openai.com/v1/completions",
                    new StringContent("{\"model\": \"gpt-3.5-turbo\", \"prompt\": \"" + pergunta + "\", \"temperature\": 1, \"max_tokens\": 1024}", Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Resposta resposta = JsonSerializer.Deserialize<Resposta>(responseString);

                        Array.ForEach(resposta.data.ToArray(), (item) => result += item.url.Replace("\n", ""));

                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest("Ocorreu um erro ao enviar a pergunta.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }



        [HttpPost("Image")]
        public async Task<IActionResult> GetImagem(string pergunta)
        {


            try
            {

                var result = "";
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _apiKey);
                    var response = await client.PostAsync("https://api.openai.com/v1/images/generations",
                    new StringContent("{\"prompt\": \"" + pergunta + "\", \"n\": 1, \"size\": \"1024x1024\"}", Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();
                        Resposta resposta = JsonSerializer.Deserialize<Resposta>(responseString);


                        Array.ForEach(resposta.data.ToArray(), (item) => result += item.url.Replace("\n", ""));

                        return Ok(result);

                    }
                    else
                    {
                        return BadRequest("Ocorreu um erro ao enviar a pergunta.");
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }



        }

    }
}
