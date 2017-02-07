using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Speech.Tts;
using static Android.Speech.Tts.TextToSpeech;
using Android.Speech;
using Android.Provider;
using System.Threading;

namespace DemoPart3
{
    [Activity(Label = "DemoPart3", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnInitListener
    {
        const int VOICE = 100;
        const int CAMERA = 101;

        Button btnViewMap;
        Button btnSpeak;
        Button btnListen;
        Button btnCamera;
        TextToSpeech ttsClient;
        TextView txtResult;
        ImageView imgResult;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            btnViewMap = FindViewById<Button>(Resource.Id.btnViewMap);
            btnSpeak = FindViewById<Button>(Resource.Id.btnSpeak);
            btnListen = FindViewById<Button>(Resource.Id.btnListen);
            btnCamera = FindViewById<Button>(Resource.Id.btnCamera);
            txtResult = FindViewById<TextView>(Resource.Id.txtResult);
            imgResult = FindViewById<ImageView>(Resource.Id.imgResult);

            btnViewMap.Click += BtnViewMap_Click;
            btnSpeak.Click += BtnSpeak_Click;
            btnListen.Click += BtnListen_Click;
            btnCamera.Click += BtnCamera_Click;

            ttsClient = new TextToSpeech(this, this);
        }

        private void BtnViewMap_Click(object sender, EventArgs e)
        {
            //map
            //var mapUri = Android.Net.Uri.Parse("geo:37.2485813,127.4828382");

            //street view
            var mapUri = Android.Net.Uri.Parse("google.streetview:cbll=37.575118,126.977069&cbp=1,90,,0,1.0&mz=20");

            Intent mapIntent = new Intent(Intent.ActionView, mapUri);
            StartActivity(mapIntent);

        }

        private void BtnSpeak_Click(object sender, EventArgs e)
        {
            
            //ttsClient.Speak("김프로님 준비하십시요",QueueMode.Add,null);
            //ttsClient.Speak("전방에 과속 방지턱이 있습니다", QueueMode.Add, null);
            //ttsClient.Speak("영원히 당신만을 사랑합니다", QueueMode.Add, null);
            //ttsClient.Speak("How are you", QueueMode.Add, null);
            //ttsClient.Speak("I love you forever", QueueMode.Add, null);
            ttsClient.Speak("아이 러브 유 포에버", QueueMode.Add, null);









            //Thread.Sleep(8000);

            ttsClient.SetLanguage(Java.Util.Locale.English);
            ttsClient.Speak("How are you", QueueMode.Add, null);
            ttsClient.Speak("I love you forever", QueueMode.Add, null);
            //Thread.Sleep(5000);

            ttsClient.SetLanguage(Java.Util.Locale.China);
            ttsClient.Speak("我永远只爱你一个人.", QueueMode.Add, null);
            //Thread.Sleep(3000);

            ttsClient.SetLanguage(Java.Util.Locale.Germany);
            ttsClient.Speak("Ich liebe dich nur für immer.", QueueMode.Add, null);

        }

        private void BtnListen_Click(object sender, EventArgs e)
        {
            var voiceIntent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            voiceIntent.PutExtra(RecognizerIntent.ExtraPrompt, "말씀하세요");
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputCompleteSilenceLengthMillis, 1500);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputPossiblyCompleteSilenceLengthMillis, 1500);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraSpeechInputMinimumLengthMillis, 15000);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraMaxResults, 1);
            //voiceIntent.PutExtra(RecognizerIntent.ExtraLanguage, Java.Util.Locale.Default);
            StartActivityForResult(voiceIntent, VOICE);
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            Intent cameraIntent = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(cameraIntent, CAMERA);
        }

        public void OnInit([GeneratedEnum] OperationResult status)
        {
            
        }

        protected override void OnActivityResult(int requestCode, Result resultVal, Intent data)
        {
            switch (requestCode)
            {
                case VOICE:
                    #region voice
                    if (resultVal == Result.Ok)
                    {
                        var matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                        if (matches.Count != 0)
                        {
                            foreach (var item in matches)
                            {
                                txtResult.Text += item + "\n\r";
                            }

                        }
                        else
                            txtResult.Text = "..해석 불가..";

                    }
                    #endregion
                    break;
                case CAMERA:
                    #region camera
                    if (resultVal == Result.Ok)
                    {
                        imgResult.SetImageURI(data.Data);
                    }
                    #endregion
                    break;
                default:
                    break;
            }

        }
    }
}

