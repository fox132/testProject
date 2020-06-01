using System.Threading.Tasks;

namespace Providers.Currency
{
	public interface ICurrencyProvider
	{
		Task<CurrenciesInfo> GetAsync();
	}
}
