using System;
using System.Collections.Generic;

namespace Providers.Currency
{
	public class CurrenciesInfo
	{
		public DateTime Date { get; set; }
		public DateTime PreviousDate { get; set; }
		public Dictionary<string, CurrencyInfo> Valute { get; set; }
	}
}
