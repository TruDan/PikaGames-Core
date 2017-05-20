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

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private PikaMediaRouterCallback _mediaRouterCallback;

        private CastDevice _castDevice;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Game = new PaperCastGame();

            // Create your application here
            SetContentView(Resource.Layout.PikaRemoteDisplay);


            _mediaRouter = MediaRouter.GetInstance(Application.Context);
            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();

            _mediaRouterCallback = new PikaMediaRouterCallback()
            {
                OnRouteSelectedHandler = (router, route) =>
                {

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