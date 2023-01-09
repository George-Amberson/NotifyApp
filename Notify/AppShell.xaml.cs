namespace Notify;
using NotificationBackService;
using Notify.ViewModel;
public partial class AppShell : Shell
{
	NotesViewModel sharedContext;
    public AppShell(NotifyService.NotifyServiceClient notifyClient, BaseService _backgroundService)
    {
        InitializeComponent();
        sharedContext = new NotesViewModel(notifyClient, _backgroundService);
        this.TabBar.BindingContext = sharedContext;
    }
  
}
