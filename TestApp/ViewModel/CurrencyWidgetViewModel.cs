using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Providers.Currency;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TestApp.Infrastructure;
using TestApp.Model;

namespace TestApp.ViewModel
{
	public class CurrencyWidgetViewModel : ViewModelBase
	{
		#region variables
		private readonly ICurrencyProvider provider;
		private IEnumerable<Valute> valutes;
		private IEnumerable<Valute> initialValutes;
		private Valute currentValute;
		private string valuteToUsd;
		private string valuteToRub;
		private string lastSearchCriterion;		
		#endregion

		public IEnumerable<CurrencyExchangeConvention> AvailableCurrencies { get; set; }
		public ObservableCollection<CurrencyConvention> Conventions { get; private set; }
		public ICommand RecalculateCommand { get; set; }
		public ICommand SearchCurrencyCommand { get; set; }

		public string ValuteToUsd
		{
			get
			{
				return this.valuteToUsd;
			}
			set
			{
				this.valuteToUsd = value;
				this.RaisePropertyChanged(nameof(ValuteToUsd));
			}
		}
		public string ValuteToRub
		{
			get
			{
				return this.valuteToRub;
			}
			set
			{
				this.valuteToRub = value;
				this.RaisePropertyChanged(nameof(ValuteToRub));
			}
		}
		public DateTime SelectedDateTime { get; } = DateTime.Now;
		public Valute CurrentValute
		{
			get
			{
				return this.currentValute;
			}
			set
			{
				this.currentValute = value;
				OnCurrentValuteChanged();
				this.RaisePropertyChanged(nameof(CurrentValute));
			}
		}
		public IEnumerable<Valute> Valutes
		{
			get
			{
				return this.valutes;
			}
			set
			{
				this.valutes = value;
				this.RaisePropertyChanged(nameof(Valutes));
			}
		}
		
		private void OnCurrentValuteChanged()
		{
			if (CurrentValute == null)
			{
				ValuteToUsd = String.Empty;
				ValuteToRub = String.Empty;
				return;
			}
			var usdValute = this.Valutes.FirstOrDefault(f => string.Equals(f.CharCode, "usd", StringComparison.OrdinalIgnoreCase));
			decimal valuteToUsd = Math.Round(((CurrentValute.CurrentValue) / (usdValute.CurrentValue)), 4);
			ValuteToUsd = string.Format(CurrentValute.Nominal + CurrentValute.CharCode + '=' + valuteToUsd + usdValute.CharCode);
			ValuteToRub = string.Format(CurrentValute.Nominal + CurrentValute.CharCode + '=' + CurrentValute.CurrentValue + "RUB");
		}

		public CurrencyWidgetViewModel(ICurrencyProvider provider)
		{
			this.provider = provider;
			this.RefreshData = new AsyncCommand(this.OnRefreshData);
			this.RecalculateCommand = new RelayCommand<CurrencyConvention>(this.OnRecalculateCommand);
			this.SearchCurrencyCommand = new RelayCommand<string>(this.OnSearchCurrencyCommand);
		}

		private void OnSearchCurrencyCommand(string searchCriterion)
		{
			this.lastSearchCriterion = searchCriterion;
			if (string.IsNullOrEmpty(searchCriterion)) {
				this.Valutes = this.initialValutes;
				this.RaisePropertyChanged(() => this.Valutes);
				return;
			}
			this.Valutes = this.initialValutes?.Where(valute => valute.CharCode.StartsWith(searchCriterion) || valute.Name.StartsWith(searchCriterion)).ToList();
			this.RaisePropertyChanged(() => this.Valutes);
		}

		private void OnRecalculateCommand(CurrencyConvention convention)
		{
			this.RecalculateUsing(convention);
		}

		private void RecalculateUsing(CurrencyConvention conventionToUse)
		{
			foreach (var convention in this.Conventions)
			{
				if (CurrencyConvention.DefaultComparer.Equals(convention, conventionToUse))
				{
					continue;
				}
				convention.Value = Math.Round((conventionToUse.ExchangeConvention.Rate / convention.ExchangeConvention.Rate * conventionToUse.Value), 4);
			}
		}

		private async Task OnRefreshData()
		{
			var newData = await this.provider.GetAsync();
			this.initialValutes = newData.Valute.Select(v => new Valute
			{
				Name = v.Value.Name,
				CharCode = v.Key,
				Nominal = v.Value.Nominal,
				CurrentValue = v.Value.Value
			}).ToList();
			this.OnSearchCurrencyCommand(this.lastSearchCriterion);
			this.AvailableCurrencies = this.initialValutes.Select(v => new CurrencyExchangeConvention
			{
				Code = v.CharCode,
				Rate = v.CurrentValue / v.Nominal
			}).ToList();
			this.Conventions = new ObservableCollection<CurrencyConvention>
			{
				new CurrencyConvention
				{
					ExchangeConvention = this.AvailableCurrencies.First(f=> string.Equals(f.Code, "usd", StringComparison.OrdinalIgnoreCase)),
					Value = 10m
				},
				new CurrencyConvention
				{
					ExchangeConvention = this.AvailableCurrencies.First(f=> string.Equals(f.Code, "byn", StringComparison.OrdinalIgnoreCase)),
					Value = 0m
				}
			};
			this.RaisePropertyChanged(nameof(this.Conventions));
			this.RecalculateUsing(this.Conventions.First());
		}
		public ICommand RefreshData { get; set; }
	}
}



