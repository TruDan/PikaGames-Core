using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Cast;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Media;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace PikaGames
{
    [Activity(Label = "@string/app_name", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class CastRemoteDisplayActivity : ActionBarActivity
    {

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private MyMediaRouterCallback _myMediaRouterCallback;

        private CastDevice _castDevice;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RemoteDisplay);


            _mediaRouter = MediaRouter.GetInstance(Application.Context);
            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();

            _myMediaRouterCallback = new MyMediaRouterCallback()
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
                    _castDevice = (CastDevice) extras.GetParcelable(MainActivity.INTENT_EXTRA_CAST_DEVICE);
                }
            }

            _mediaRouter.AddCallback(_mediaRouteSelector, _myMediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);
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
            _mediaRouter.RemoveCallback(_myMediaRouterCallback);
        }

        private bool IsRemoteDisplaying()
        {
            return CastRemoteDisplayLocalService.Instance != null;
        }

        private void StartCastService(CastDevice castDevice)
        {
            Intent intent = new Intent(this, Java.Lang.Class.FromType(typeof(CastRemoteDisplayActivity)));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            PendingIntent notificationPendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            CastRemoteDisplayLocalService.NotificationSettings settings = new CastRemoteDisplayLocalService.NotificationSettings.Builder().SetNotificationPendingIntent(notificationPendingIntent).Build();
            CastRemoteDisplayLocalService.StartService(this, Java.Lang.Class.FromType(typeof(PresentationService)), GetString(Resource.String.app_id), castDevice, settings, new CastRemoteDisplayCallbacks()
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

    public class CastRemoteDisplayCallbacks : Java.Lang.Object, CastRemoteDisplayLocalService.ICallbacks
    {
        public CastRemoteDisplayCallbacks() : base()
        {
        }

        public Action<Statuses> OnRemoteDisplaySessionErrorHandler { get; set; }
        public void OnRemoteDisplaySessionError(Statuses status)
        {
            Console.WriteLine("OnServiceError: " + status.StatusCode);

            OnRemoteDisplaySessionErrorHandler?.Invoke(status);
        }

        public Action<CastRemoteDisplayLocalService> OnRemoteDisplaySessionStartedHandler { get; set; }
        public void OnRemoteDisplaySessionStarted(CastRemoteDisplayLocalService service)
        {
            Console.WriteLine("OnServiceStarted");

            OnRemoteDisplaySessionStartedHandler?.Invoke(service);
        }

        public Action<CastRemoteDisplayLocalService> OnServiceCreatedHandler { get; set; }
        public void OnServiceCreated(CastRemoteDisplayLocalService service)
        {
            Console.WriteLine("OnServiceCreated");

            OnServiceCreatedHandler?.Invoke(service);
        }
    }
}