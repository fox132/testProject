using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TestApp.Infrastructure
{
	public static class ComboBoxSelectionChangedEventProvider
    {
		public static ICommand GetCommand(ComboBox comboBox)
		{
			return (ICommand)comboBox.GetValue(CommandProperty);
		}

		public static void SetCommand(ComboBox comboBox, ICommand command)
		{
			comboBox.SetValue(CommandProperty, command);
		}

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.RegisterAttached(
				"Command",
				typeof(ICommand),
				typeof(ComboBoxSelectionChangedEventProvider),
				new PropertyMetadata(OnSetCustomCommandCallback));

		private static void OnSetCustomCommandCallback(
			DependencyObject dep,
			DependencyPropertyChangedEventArgs e)
		{
			var comboBox = dep as ComboBox;
			if (comboBox != null)
			{
				ComboBoxTextChangedCommandBehavior behavior = GetOrCreateComboBoxBehavior(comboBox);
				behavior.Command = e.NewValue as ICommand;
			}
		}


		public static object GetCommandParameter(ComboBox comboBox)
		{
			return (object)comboBox.GetValue(CommandParameterProperty);
		}

		public static void SetCommandParameter(ComboBox comboBox, object value)
		{
			comboBox.SetValue(CommandParameterProperty, value);
		}

		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.RegisterAttached(
				"CommandParameter",
				typeof(object),
				typeof(ComboBoxSelectionChangedEventProvider),
				new PropertyMetadata(OnSetCustomCommandParameterCallback));

		private static void OnSetCustomCommandParameterCallback(DependencyObject dep,
			DependencyPropertyChangedEventArgs e)
		{
			var comboBox = dep as ComboBox;
			if (comboBox != null)
			{
				ComboBoxTextChangedCommandBehavior behavior = GetOrCreateComboBoxBehavior(comboBox);
				behavior.CommandParameter = e.NewValue;
			}
		}

		public static readonly DependencyProperty SelectionChangedCommandBehaviorProperty =
			DependencyProperty.RegisterAttached("ComboBoxTextChangedCommandBehavior",
				typeof(ComboBoxTextChangedCommandBehavior),
				typeof(ComboBoxSelectionChangedEventProvider));

		private static ComboBoxTextChangedCommandBehavior GetOrCreateComboBoxBehavior(ComboBox comboBox)
		{
			var behavior = comboBox.GetValue(SelectionChangedCommandBehaviorProperty) as ComboBoxTextChangedCommandBehavior;
			if (behavior == null)
			{
				behavior = new ComboBoxTextChangedCommandBehavior(comboBox);
				comboBox.SetValue(SelectionChangedCommandBehaviorProperty, behavior);
			}
			return behavior;
		}
	}
}
