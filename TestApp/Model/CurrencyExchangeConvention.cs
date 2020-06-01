namespace TestApp.Model
{
	public class CurrencyExchangeConvention
	{
		protected bool Equals(CurrencyExchangeConvention other)
		{
			return Code == other.Code;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CurrencyExchangeConvention)obj);
		}

		public override int GetHashCode()
		{
			return (Code != null ? Code.GetHashCode() : 0);
		}

		public string Code { get; set; }
		public decimal Rate { get; set; }
	}
}
