using System.Threading.Tasks;
using System.Windows.Input;

namespace TestApp.Infrastructure
{
	public interface IAsyncCommand : ICommand
	{
		Task ExecuteAsync(object parameter);
	}
}
