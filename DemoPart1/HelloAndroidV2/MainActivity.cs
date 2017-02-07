using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HelloAndroidV2
{
    [Activity(Label = "MainActivity", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private int i;
        Button btnClickMe;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //int idButton = Resources.GetIdentifier("btnClickMe", "id", PackageName);
            //int idLayout = Resources.GetIdentifier("mainlayout", "layout", PackageName);

            this.SetContentView(Resource.Layout.MainLayout);
            btnClickMe = this.FindViewById<Button>(Resource.Id.btnClickMe);

            btnClickMe.Click += BtnClickMe_Click;
        }

        private void BtnClickMe_Click(object sender, EventArgs e)
        {
            string s = string.Format("현재 {0}번 클릭 하셨습니다", ++i);
            btnClickMe.Text = s;
        }
    }
}