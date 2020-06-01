using System.Windows.Controls;

namespace TestApp.Infrastructure
{
	public class TextBoxTextChangedCommandBehavior : CommandBehaviorBase<TextBox>
	{
		public TextBoxTextChangedCommandBehavior(TextBox textBox) : base(textBox)
		{
			textBox.TextChanged += this.OnTextChanged;
		}

		private void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			var textBox = sender as TextBox;
			if (textBox != null && textBox.IsKeyboardFocused)
			{
				this.ExecuteCommand();
			}
		}
	}
}
