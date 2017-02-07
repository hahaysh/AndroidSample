using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net.Http;

namespace DemoPart2
{
    [Activity(Label = "DemoPart2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        Button btnCallHome;
        Button btnNotify;
        Button btnDownload;
        TextView txtView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            btnCallHome = FindViewById<Button>(Resource.Id.btnCallHome);
            btnNotify = FindViewById<Button>(Resource.Id.btnNotify);
            btnDownload = FindViewById<Button>(Resource.Id.btnDownloadWeb);
            txtView = FindViewById<TextView>(Resource.Id.txtView);

            btnCallHome.Click += Button_Click;
            btnNotify.Click += BtnNotify_Click;
            btnDownload.Click += BtnDownload_Click;
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            txtView.Text =await client.GetStringAsync("http://www.naver.com");
        }

        private void BtnNotify_Click(object sender, EventArgs e)
        {
            //Intent intent = new Intent(this, typeof(MainActivity));
            //const int pendingIntentId = 0;
            //PendingIntent pendingIntent =
            //    PendingIntent.GetActivity(this, pendingIntentId, intent, PendingIntentFlags.OneShot);

            Notification.Builder builder = new Notification.Builder(this);
            //builder.SetContentIntent(pendingIntent);
            builder.SetPriority((int)NotificationPriority.High);
            builder.SetContentTitle("알려드립니다!!");
            builder.SetContentText("Hello World! This is my first notification!");
            builder.SetDefaults(NotificationDefaults.Sound | NotificationDefaults.Vibrate | NotificationDefaults.Lights);
            builder.SetSmallIcon(Resource.Drawable.Icon);
            Notification noti = builder.Build();

            // Build the notification:
            Notification notification = builder.Build();
            NotificationManager manager = GetSystemService(Context.NotificationService) as NotificationManager;
            manager.Notify(101, noti);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Intent callIntent = new Intent(Intent.ActionCall);
            callIntent.SetData(Android.Net.Uri.Parse("tel:031-413-7958"));
            StartActivity(callIntent);
        }
    }
}

