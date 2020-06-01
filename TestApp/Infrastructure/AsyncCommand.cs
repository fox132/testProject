using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace TestApp.Infrastructure
{
	public class AsyncCommand : AsyncCommandBase
	{
		private readonly Func<Task> _command;
		public AsyncCommand(Func<Task> command)
		{
			_command = command;
		}
		public override bool CanExecute(object parameter)
		{
			return true;
		}
		public override Task ExecuteAsync(object parameter)
		{
			return _command();
		}
	}
	public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged
	{
		private readonly Func<Task<TResult>> _command;
		private NotifyTaskCompletion<TResult> _execution;

		public event PropertyChangedEventHandler PropertyChanged;

		public AsyncCommand(Func<Task<TResult>> command)
		{
			_command = command;
		}

		public override bool CanExecute(object parameter)
		{
			return true;
		}
		public override Task ExecuteAsync(object parameter)
		{
			Execution = new NotifyTaskCompletion<TResult>(_command());
			return Execution.Task;
		}
		
		public NotifyTaskCompletion<TResult> Execution { get; private set; }
	}
}
