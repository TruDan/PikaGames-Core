using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.Gms.Common.Apis;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PikaGames.Android.Cast
{
    public class PikaRemoteDisplayCallback : Java.Lang.Object, CastRemoteDisplayLocalService.ICallbacks
    {
        public PikaRemoteDisplayCallback() : base()
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