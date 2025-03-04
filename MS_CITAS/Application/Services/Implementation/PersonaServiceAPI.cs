using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

public class PersonaServiceAPI
{
    private readonly RestClient _client;
    private readonly string _baseUrl = "https://localhost:44345/api/Personas";

    public PersonaServiceAPI()
    {
        _client = new RestClient(_baseUrl);
    }

    public async Task<dynamic> ObtenerPersona(int personaId)
    {
        var request = new RestRequest($"/{personaId}", Method.Get);

        try
        {
            RestResponse response = await _client.ExecuteAsync(request);

            if (!response.IsSuccessful)
                return null;

            return JsonConvert.DeserializeObject<PersonaDto>(response.Content);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
