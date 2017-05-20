using System;
using System.ComponentModel.Design;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Content.PM;
using Android.Gms.Cast;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Support.V7.App;
using Android.Support.V7.Media;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MediaRouteButton = Android.Support.V7.App.MediaRouteButton;
using Object = Java.Lang.Object;
using String = System.String;

namespace PikaGames
{
    // the ConfigurationChanges flags set here keep the EGL context
    // from being destroyed whenever the device is rotated or the
    // keyboard is shown (highly recommended for all GL apps)
    [Activity(Label = "@string/app_name",
                    ConfigurationChanges = ConfigChanges.Orientation,
                    ScreenOrientation = ScreenOrientation.Portrait,
                    LaunchMode = LaunchMode.SingleTop,
                    Icon = "@mipmap/icon")]
    [IntentFilter(actions: new []{"android.intent.action.MAIN"}, Categories = new []{"android.intent.category.LAUNCHER"})]
    public class MainActivity : ActionBarActivity
    {
        public const String INTENT_EXTRA_CAST_DEVICE = "CastDevice";
        
        private MediaRouteButton _mediaRouteButton;

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private MyMediaRouterCallback _myMediaRouterCallback;
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            SetContentView(Resource.Layout.Main);
            
            InitMediaRouter();
        }

        private void InitMediaRouter()
        {

            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();
            _mediaRouter = MediaRouter.GetInstance(Application.Context);

            _mediaRouteButton = FindViewById<MediaRouteButton>(Resource.Id.mediaRouteButton1);
            _mediaRouteButton.RouteSelector = _mediaRouteSelector;

            _myMediaRouterCallback = new MyMediaRouterCallback()
            {
                OnRouteSelectedHandler = (router, route) =>
                {

                    Console.WriteLine("Route Selected: " + route.Name);

                    var castDevice = CastDevice.GetFromBundle(route.Extras);

                    if (castDevice != null)
                    {
                        Intent intent = new Intent(this, Class.FromType(typeof(CastRemoteDisplayActivity)));
                        intent.PutExtra(INTENT_EXTRA_CAST_DEVICE, castDevice);
                        StartActivity(intent);
                    }
                },
                OnRouteUnselectedHandler = (router, route) =>
                {
                    Console.WriteLine("Route Unselected: " + route.Name);
                },
                RouteCountChangedHandler = newCount =>
                {
                    _mediaRouteButton.Visibility = newCount > 0 ? ViewStates.Visible : ViewStates.Gone;
                }
            };

        }
        
        protected override void OnStart()
        {
            base.OnStart();
            _mediaRouter.AddCallback(_mediaRouteSelector, _myMediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);
        }

        protected override void OnStop()
        {
            base.OnStop();
            _mediaRouter.RemoveCallback(_myMediaRouterCallback);
        }
    }

    class MyMediaRouterCallback : MediaRouter.Callback
    {
        public Action<int> RouteCountChangedHandler { get; set; }

        int routeCount = 0;

        public override void OnRouteAdded(MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine("Route Added: " + route.Name);

            routeCount++;

            RouteCountChangedHandler?.Invoke(routeCount);
        }
        public override void OnRouteRemoved(MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine("Route Removed: " + route.Name);

            routeCount--;

            RouteCountChangedHandler?.Invoke(routeCount);
        }

        public override void OnRouteChanged(MediaRouter router, MediaRouter.RouteInfo route)
        {
            Console.WriteLine("Route Changed: " + route.Name);
        }

        public Action<MediaRouter, MediaRouter.RouteInfo> OnRouteSelectedHandler { get; set; }
        public override void OnRouteSelected(MediaRouter router, MediaRouter.RouteInfo route)
        {
            OnRouteSelectedHandler?.Invoke(router, route);
        }

        public Action<MediaRouter, MediaRouter.RouteInfo> OnRouteUnselectedHandler { get; set; }
        public override void OnRouteUnselected(MediaRouter router, MediaRouter.RouteInfo route)
        {
            OnRouteUnselectedHandler?.Invoke(router, route);
        }

    }
}