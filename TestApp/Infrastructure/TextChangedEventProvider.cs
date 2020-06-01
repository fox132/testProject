using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestApp.Infrastructure
{
	public static class TextChangedEventProvider
	{
		public static ICommand GetCommand(TextBox textBox)
		{
			return (ICommand)textBox.GetValue(CommandProperty);
		}

		public static void SetCommand(TextBox textBox, ICommand command)
		{
			textBox.SetValue(CommandProperty, command);
		}

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command",
				typeof(ICommand),
				typeof(TextChangedEventProvider),
				new PropertyMetadata(OnSetCustomCommandCallback));

		private static void OnSetCustomCommandCallback(
			DependencyObject dep,
			DependencyPropertyChangedEventArgs e)
		{
			var textBox = dep as TextBox;
			if (textBox != null)
			{
				TextBoxTextChangedCommandBehavior behavior = GetOrCreateTextBoxBehavior(textBox);
				behavior.Command = e.NewValue as ICommand;
			}
		}


		public static object GetCommandParameter(TextBox textBox)
		{
			return (object)textBox.GetValue(CommandParameterProperty);
		}

		public static void SetCommandParameter(TextBox textBox, object value)
		{
			textBox.SetValue(CommandParameterProperty, value);
		}

		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached(
				"CommandParameter",
				typeof(object),
				typeof(TextChangedEventProvider),
				new PropertyMetadata(OnSetCustomCommandParameterCallback));

		private static void OnSetCustomCommandParameterCallback(DependencyObject dep,
			DependencyPropertyChangedEventArgs e)
		{
			var textBox = dep as TextBox;
			if (textBox != null)
			{
				TextBoxTextChangedCommandBehavior behavior = GetOrCreateTextBoxBehavior(textBox);
				behavior.CommandParameter = e.NewValue;
			}
		}

		public static readonly DependencyProperty TextChangedCommandBehaviorProperty =
			DependencyProperty.RegisterAttached("TextChangedCommandBehavior",
				typeof(TextBoxTextChangedCommandBehavior),
				typeof(TextChangedEventProvider));

		private static TextBoxTextChangedCommandBehavior GetOrCreateTextBoxBehavior(TextBox textBox)
		{
			var behavior = textBox.GetValue(TextChangedCommandBehaviorProperty) as TextBoxTextChangedCommandBehavior;
			if (behavior == null)
			{
				behavior = new TextBoxTextChangedCommandBehavior(textBox);
				textBox.SetValue(TextChangedCommandBehaviorProperty, behavior);
			}

			return behavior;
		}
	}

}
