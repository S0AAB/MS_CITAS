using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class PersonaServiceAPI
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:44345/api/Personas"; 

    public PersonaServiceAPI()
    {
        _httpClient = new HttpClient();
    }

    public async Task<dynamic> ObtenerPersona(int personaId)
    {
        var url = $"{_baseUrl}/{personaId}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return null; 

            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<dynamic>(jsonResponse);
        }
        catch (Exception)
        {
            return null; 
        }
    }
}