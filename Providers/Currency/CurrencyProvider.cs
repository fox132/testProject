using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Providers.Currency
{
	public class CurrencyProvider : ICurrencyProvider
	{
		public async Task<CurrenciesInfo> GetAsync()
		{
			var handler = new HttpClientHandler();
			handler.DefaultProxyCredentials = CredentialCache.DefaultCredentials;

			using (var client = new HttpClient(handler))
			{
				HttpResponseMessage response = await client.GetAsync("https://www.cbr-xml-daily.ru/daily_json.js");
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<CurrenciesInfo>(responseBody);
			}

		}
	}

}
