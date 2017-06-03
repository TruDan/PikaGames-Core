using System;
using System.Runtime.InteropServices;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Cast;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Support.V7.Media;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using ClientLauncher;
using PikaGames.Android.Cast;
using Java.Lang;
using Microsoft.Xna.Framework;
using PikaGames.Android.Cast.Gui;
using PikaGames.Android.Cast.MonoGame;
using PikaGames.Android.Views;
using PikaGames.Games.Core;
using PikaGames.Games.Core.Gui;
using Debug = System.Diagnostics.Debug;
using MediaRouteButton = Android.Support.V7.App.MediaRouteButton;

namespace PikaGames.Android
{
    [Activity(Label = "@string/app_name",
        ConfigurationChanges = ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Landscape,
        LaunchMode = LaunchMode.SingleInstance)]
    [IntentFilter(actions: new[] { "android.intent.action.MAIN" }, Categories = new[] { "android.intent.category.LAUNCHER" })]
    public class MainActivity : AndroidGameActivity
    {
        private PikaToolbar _toolbar;

        private MediaRouter _mediaRouter;
        private MediaRouteSelector _mediaRouteSelector;
        private PikaMediaRouterCallback _myMediaRouterCallback;

        private CastDevice _castDevice;

        private RootGame _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);



            RootGame.InitMobile();

            var wrapper = new AndroidGameWrapper(rg =>
            {
                var g = new ClientLauncherGame(rg);

                rg.Run();

                g.MainMenuScene.PlayButton.Pressed += (sender, args) =>
                {
                    Console.WriteLine("Showing Cast Dialog");
                    //b.ShowDialog();
                    var dialog = new PikaCastRouteSelectDialog(_mediaRouter, _mediaRouteSelector);
                    dialog.ShowDialog();
                };

                return g;
            });
            var gameView = (View)wrapper.Services.GetService(typeof(View));

            SetContentView(gameView);

            InitMediaRouter();

            //SetContentView(Resource);


            //InitMediaRouter();

            //_toolbar = FindViewById<PikaToolbar>(Resource.Id.pikaToolbar1);
            //_toolbar.MediaRouteButton.RouteSelector = _mediaRouteSelector;
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

            CastRemoteDisplayLocalService.StartService(this, Class.FromType(typeof(PikaPresentationService)), GetString(Resource.String.app_id), castDevice, settings, new PikaRemoteDisplayCallback()
            {
                OnRemoteDisplaySessionErrorHandler = (errorReason) =>
                {
                    int code = errorReason.StatusCode;
                    Debug.WriteLine("OnServiceError: " + errorReason.StatusCode);

                    _castDevice = null;
                    Finish();
                }
            });
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (IsRemoteDisplaying())
            {
                CastRemoteDisplayLocalService.StopService();
            }

            _mediaRouter.RemoveCallback(_myMediaRouterCallback);
        }

        private void InitMediaRouter()
        {

            _mediaRouteSelector = new MediaRouteSelector.Builder().AddControlCategory(CastMediaControlIntent.CategoryForCast(GetString(Resource.String.app_id))).Build();
            _mediaRouter = MediaRouter.GetInstance(Application.Context);

            _myMediaRouterCallback = new PikaMediaRouterCallback()
            {
                OnRouteSelectedHandler = (router, route) =>
                {

                    Console.WriteLine("Route Selected: " + route.Name);
                    
                    if (IsRemoteDisplaying())
                    {
                        CastDevice castDevice = CastDevice.GetFromBundle(_mediaRouter.SelectedRoute.Extras);
                        _castDevice = castDevice;
                    }
                    else
                    {
                        var castDevice = CastDevice.GetFromBundle(route.Extras);
                        _castDevice = castDevice;
                    }

                    StartCastService(_castDevice);

                    //FindViewById<LinearLayout>(Resource.Id.linearLayout1).RemoveView((View)RootGame.Instance.Services.GetService(typeof(View)));
                    //RootGame.Instance.Exit();
                    //Intent intent = new Intent(this, Class.FromType(typeof(PikaRemoteDisplayActivity)));
                    //intent.PutExtra(PikaRemoteDisplayActivity.INTENT_EXTRA_CAST_DEVICE, castDevice);
                    //StartActivity(intent);
                    
                },
                OnRouteUnselectedHandler = (router, route) =>
                {
                    Console.WriteLine("Route Unselected: " + route.Name);
                    if (IsRemoteDisplaying())
                    {
                        CastRemoteDisplayLocalService.StopService();
                    }
                    _castDevice = null;
                    Finish();
                },
                RouteCountChangedHandler = newCount =>
                {
                    //_toolbar.Visibility = newCount > 0 ? ViewStates.Visible : ViewStates.Gone;
                }
            };

            if (IsRemoteDisplaying())
            {
                CastDevice castDevice = CastDevice.GetFromBundle(_mediaRouter.SelectedRoute.Extras);
                _castDevice = castDevice;
            }

            _mediaRouter.AddCallback(_mediaRouteSelector, _myMediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);
        }

    }
}

