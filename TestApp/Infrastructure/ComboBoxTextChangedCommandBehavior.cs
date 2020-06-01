using System.Windows.Controls;

namespace TestApp.Infrastructure
{
	public class ComboBoxTextChangedCommandBehavior : CommandBehaviorBase<ComboBox>
	{
		public ComboBoxTextChangedCommandBehavior(ComboBox comboBox) : base(comboBox)
		{
			comboBox.SelectionChanged += this.OnSelectionChanged;
		}

		private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var comboBox = sender as ComboBox;
			if (comboBox != null)
				this.ExecuteCommand();
		}
	}
}
