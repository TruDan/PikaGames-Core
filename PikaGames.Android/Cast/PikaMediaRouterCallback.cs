using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Media;
using Android.Views;
using Android.Widget;

namespace PikaGames.Android.Cast
{
    internal class PikaMediaRouterCallback : MediaRouter.Callback
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