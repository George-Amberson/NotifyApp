using NotificationBackService;

namespace Notify;

public partial class App : Application
{
	public App(NotifyService.NotifyServiceClient notifyClient, BaseService _BackService)
	{
		InitializeComponent();

		MainPage = new AppShell(notifyClient,  _BackService);
	}
}
