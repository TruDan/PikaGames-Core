using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Cast;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Media;
using Android.Views;
using PikaGames.Android.Cast;
using Java.Lang;
using MediaRouteButton = Android.Support.V7.App.MediaRouteButton;

namespace PikaGames.Android
{
    [Activity(Label = "@string/app_name",
        ConfigurationChanges = ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(actions: new[] { "android.intent.action.MAIN" }, Categories = new[] { "android.intent.category.LAUNCHER" })]
    public class MainActivity : ActionBarActivity
    {

        private MediaRouteButton _mediaRouteButton;

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private PikaMediaRouterCallback _myMediaRouterCallback;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            //var g = new Game1();
            //SetContentView((View)g.Services.GetService(typeof(View)));
            //g.Run();
            InitMediaRouter();
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

        private void InitMediaRouter()
        {

            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();
            _mediaRouter = MediaRouter.GetInstance(Application.Context);

            _mediaRouteButton = FindViewById<MediaRouteButton>(Resource.Id.mediaRouteButton1);
            _mediaRouteButton.RouteSelector = _mediaRouteSelector;

            _myMediaRouterCallback = new PikaMediaRouterCallback()
            {
                OnRouteSelectedHandler = (router, route) =>
                {

                    Console.WriteLine("Route Selected: " + route.Name);

                    var castDevice = CastDevice.GetFromBundle(route.Extras);
                    
                    if (castDevice != null)
                    {
                        Intent intent = new Intent(this, Class.FromType(typeof(PikaRemoteDisplayActivity)));
                        intent.PutExtra(PikaRemoteDisplayActivity.INTENT_EXTRA_CAST_DEVICE, castDevice);
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
    
    }
}

