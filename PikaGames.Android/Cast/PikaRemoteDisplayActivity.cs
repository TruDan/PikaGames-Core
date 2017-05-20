using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Media;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using PikaGames.Android.Views;
using PikaGames.Games;
using PikaGames.Games.PaperCast;

namespace PikaGames.Android.Cast
{
    [Activity(Label = "PikaRemoteDisplayActivity")]
    public class PikaRemoteDisplayActivity : AndroidGameActivity
    {
        public const string INTENT_EXTRA_CAST_DEVICE = "CastDevice";
        public const string INTENT_EXTRA_GAME = "Game";

        internal static Game Game;
        internal static Player LocalPlayer;

        private PikaToolbar _toolbar;

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private PikaMediaRouterCallback _mediaRouterCallback;

        private CastDevice _castDevice;

        private Button _upButton, _downButton, _leftButton, _rightButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Game = new PaperCastGame();
            LocalPlayer = ((PaperCastGame) Game).AddPlayer("TruDan");

            // Create your application here
            SetContentView(Resource.Layout.PikaRemoteDisplay);


            _mediaRouter = MediaRouter.GetInstance(Application.Context);
            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();

            _mediaRouterCallback = new PikaMediaRouterCallback()
            {
                OnRouteSelectedHandler = (router, route) =>
                {
                    if (IsRemoteDisplaying())
                    {
                        CastDevice castDevice = CastDevice.GetFromBundle(_mediaRouter.SelectedRoute.Extras);
                        _castDevice = castDevice;
                    }
                    else
                    {
                        Bundle extras = Intent.Extras;
                        if (extras != null)
                        {
                            _castDevice = (CastDevice)extras.GetParcelable(INTENT_EXTRA_CAST_DEVICE);
                        }
                    }

                    StartCastService(_castDevice);
                },
                OnRouteUnselectedHandler = (router, route) =>
                {
                    if (IsRemoteDisplaying())
                    {
                        CastRemoteDisplayLocalService.StopService();
                    }
                    _castDevice = null;
                    Finish();

                }
            };

            if (IsRemoteDisplaying())
            {
                CastDevice castDevice = CastDevice.GetFromBundle(_mediaRouter.SelectedRoute.Extras);
                _castDevice = castDevice;
            }
            else
            {
                Bundle extras = Intent.Extras;
                if (extras != null)
                {
                    _castDevice = (CastDevice)extras.GetParcelable(INTENT_EXTRA_CAST_DEVICE);
                }
            }

            _mediaRouter.AddCallback(_mediaRouteSelector, _mediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);

            _upButton = FindViewById<Button>(Resource.Id.button1);
            _downButton = FindViewById<Button>(Resource.Id.button4);
            _leftButton = FindViewById<Button>(Resource.Id.button2);
            _rightButton = FindViewById<Button>(Resource.Id.button3);

            _upButton.Touch += (sender, args) => LocalPlayer.Move(Direction.North);
            _downButton.Touch += (sender, args) => LocalPlayer.Move(Direction.South);
            _leftButton.Touch += (sender, args) => LocalPlayer.Move(Direction.West);
            _rightButton.Touch += (sender, args) => LocalPlayer.Move(Direction.East);
        }

        protected override void OnResume()
        {
            base.OnResume();
            if (!IsRemoteDisplaying())
            {
                if (_castDevice != null)
                {
                    StartCastService(_castDevice);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _mediaRouter.RemoveCallback(_mediaRouterCallback);
        }

        private bool IsRemoteDisplaying()
        {
            return CastRemoteDisplayLocalService.Instance != null;
        }

        private void StartCastService(CastDevice castDevice)
        {
            Intent intent = new Intent(this, Java.Lang.Class.FromType(typeof(PikaRemoteDisplayActivity)));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            PendingIntent notificationPendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            CastRemoteDisplayLocalService.NotificationSettings settings = new CastRemoteDisplayLocalService.NotificationSettings.Builder().SetNotificationPendingIntent(notificationPendingIntent).Build();
            CastRemoteDisplayLocalService.StartService(this, Java.Lang.Class.FromType(typeof(PikaPresentationService)), GetString(Resource.String.app_id), castDevice, settings, new PikaRemoteDisplayCallback()
            {
                OnRemoteDisplaySessionErrorHandler = (errorReason) =>
                {
                    int code = errorReason.StatusCode;
                    Console.WriteLine("OnServiceError: " + errorReason.StatusCode);

                    _castDevice = null;
                    Finish();
                }
            });
        }
    }
}