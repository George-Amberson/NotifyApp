global using Grpc.Net.Client;
using NotificationBackService;
using Plugin.LocalNotification;
namespace Notify;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
#if ANDROID
builder.Services.AddTransient<BaseService,notificationService>();
#endif
        builder.Services.AddTransient<App>();
		builder
            .UseMauiApp<App>()
			.UseLocalNotification()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddScoped(services =>
		{
			var channel = GrpcChannel.ForAddress("http://7.tcp.eu.ngrok.io:19481");
			return new NotifyService.NotifyServiceClient(channel);
		});
		return builder.Build();
	}
}
