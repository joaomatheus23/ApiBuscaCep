using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Endereco
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Complemento { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
    public string Ibge { get; set; }
    public string Gia { get; set; }
    public string Ddd { get; set; }
    public string Siafi { get; set; }
}

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Endereco> GetEnderecoAsync(string cep)
    {
        var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            var endereco = JsonConvert.DeserializeObject<Endereco>(responseBody);
            return endereco;
        }
        else
        {
            throw new Exception($"Erro de conexão - status code {response.StatusCode}");
        }
    }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var apiClient = new ApiClient();
            Console.Write("Digite o CEP: ");
            string cep = Console.ReadLine().ToString();

            var endereco = await apiClient.GetEnderecoAsync(cep);
            Console.WriteLine($"Logradouro: {endereco.Logradouro}"); // Nome da rua
            Console.WriteLine($"Bairro: {endereco.Bairro}");         // Nome do bairro
            Console.WriteLine($"Localidade: {endereco.Localidade}"); // Nome da cidade
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
