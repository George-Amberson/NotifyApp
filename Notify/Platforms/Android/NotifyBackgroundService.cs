using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Notify;
using NotificationBackService;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Microsoft.Maui.ApplicationModel.Platform;
using System.Diagnostics;
using Intent = Android.Content.Intent;
using Resource = Notify.Resource;
using Debug = System.Diagnostics.Debug;
using Notify.Models;
using Plugin.LocalNotification;
namespace NotificationBackService
{
    [Service(ForegroundServiceType = ForegroundService.TypeDataSync)]
    public class notificationService : Service, BaseService
    {
        Notification notification;
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }
        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (intent != null)
            {
                if (intent.Action == "START_SERVICE")
                {
                    StartForegroundService(intent.GetStringExtra("textInfo"), intent.GetStringExtra("dateAndTime"));
                }
                else if (intent.Action == "STOP_SERVICE")
                {
                    StopForeground(true);
                    StopSelfResult(startId);
                }
            }
            return base.OnStartCommand(intent, flags, startId);
        }

        public void Start(localNote Note)
        {

            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(notificationService));
            startService.SetAction("START_SERVICE");
            startService.PutExtra("textInfo", Note.textInfo);
            startService.PutExtra("dateAndTime", Note.dateAndTime);
            MainActivity.ActivityCurrent.StartService(startService);
        }

        public void Stop()
        {
            Intent stopIntent = new Intent(MainActivity.ActivityCurrent, this.Class);
            stopIntent.SetAction("STOP_SERVICE");
            MainActivity.ActivityCurrent.StartService(stopIntent);
        }

        private void StartForegroundService(string _textInfo, string _dateAndTime)
        {
            var request = new NotificationRequest
            {
                NotificationId = 1337,
                Title = _textInfo,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Parse(_dateAndTime)
                },
            };
            LocalNotificationCenter.Current.Show(request);

        }
    }
}