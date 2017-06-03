using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Media;
using Android.Views;
using Android.Widget;
using MonoGame.Extended.NuclexGui;
using MonoGame.Extended.NuclexGui.Controls.Desktop;
using Debug = System.Diagnostics.Debug;
using MediaRouter = Android.Support.V7.Media.MediaRouter;

namespace PikaGames.Android.Cast.Gui
{
    public class PikaCastRouteSelectDialog : GuiWindowControl
    {

        private MediaRouter _mediaRouter;

        private List<GuiButtonControl> _buttons;

        private MediaRouter.Callback _mediaRouterCallback;

        public PikaCastRouteSelectDialog(MediaRouter mediaRouter, MediaRouteSelector mediaRouteSelector)
        {
            _mediaRouter = mediaRouter;

            Bounds = new UniRectangle(new UniScalar(0.5f, -200), new UniScalar(0f, 50), new UniScalar(0f, 400), new UniScalar(0f, 400));
            Title = "Cast";

            var closeButton = new GuiCloseWindowButtonControl()
            {
                Bounds = new UniRectangle(new UniScalar(1f, -30), new UniScalar(0f, 6), new UniScalar(0f, 25),
                    new UniScalar(0f, 25)),

            };
            closeButton.Pressed += (sender, args) =>
            {
                Close();
            };
            Children.Add(closeButton);

            _mediaRouterCallback = new PikaMediaRouterCallback()
            {
                RouteCountChangedHandler = i =>
                {
                    UpdateRoutes();
                }
            };

            _mediaRouter.AddCallback(mediaRouteSelector, _mediaRouterCallback, MediaRouter.CallbackFlagRequestDiscovery);

            _buttons = new List<GuiButtonControl>();

            UpdateRoutes();

        }

        private void SelectRoute(MediaRouter.RouteInfo route)
        {
            _mediaRouter.SelectRoute(route);
        }
        
        private void UpdateRoutes()
        {
            var routes = _mediaRouter.Routes.ToArray();

            foreach (var btn in _buttons.ToArray())
            {
                Children.Remove(btn);
                _buttons.Remove(btn);
            }

            int x = 0, y = 45, i = 0;
            int h = 40, s = 10;

            foreach (MediaRouter.RouteInfo route in routes)
            {
                if (route.Description != null && route.Description.Equals("chromecast", StringComparison.InvariantCultureIgnoreCase))
                {
                    var btn = new GuiButtonControl()
                    {
                        Bounds = new UniRectangle(new UniScalar(0f, x + s), new UniScalar(0f, y + i*(h+s)), new UniScalar(1f, -(2*s)), new UniScalar(0f, h)),
                        Text = route.Name
                    };

                    btn.Pressed += (sender, args) =>
                    {
                        SelectRoute(route);
                    };

                    Children.Add(btn);
                    _buttons.Add(btn);

                    i++;
                }
            }
        }

    }
}