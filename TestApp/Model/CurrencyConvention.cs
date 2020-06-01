using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace TestApp.Model
{
	public class CurrencyConvention : ObservableObject
	{
		private sealed class ExchangeConventionEqualityComparer : IEqualityComparer<CurrencyConvention>
		{
			public bool Equals(CurrencyConvention x, CurrencyConvention y)
			{
				if (ReferenceEquals(x, y)) return true;
				if (ReferenceEquals(x, null)) return false;
				if (ReferenceEquals(y, null)) return false;
				if (x.GetType() != y.GetType()) return false;
				return Equals(x._exchangeConvention, y._exchangeConvention);
			}

			public int GetHashCode(CurrencyConvention obj)
			{
				return (obj._exchangeConvention != null ? obj._exchangeConvention.GetHashCode() : 0);
			}
		}

		public static IEqualityComparer<CurrencyConvention> DefaultComparer { get; } = new ExchangeConventionEqualityComparer();

		private CurrencyExchangeConvention _exchangeConvention;
		private decimal _value;
		public CurrencyExchangeConvention ExchangeConvention
		{
			get => this._exchangeConvention;
			set => this.Set(() => this.ExchangeConvention, ref this._exchangeConvention, value);
		}

		public decimal Value
		{
			get => this._value;
			set => this.Set(() => this.Value, ref this._value, value);
		}

	}
}
