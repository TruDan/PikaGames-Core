using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace PikaGames.Android.Cast
{
    [Service(Exported = false)]
    public class PikaPresentationService : CastRemoteDisplayLocalService
    {
        private CastPresentation _presentation;

        public override void OnCreate()
        {
            base.OnCreate();

        }

        public override void OnCreatePresentation(Display display)
        {
            CreatePresentation(display, PikaRemoteDisplayActivity.Game);
        }

        public override void OnDismissPresentation()
        {
            DismissPresentation();
        }

        private void DismissPresentation()
        {
            if (_presentation != null)
            {
                _presentation.Dismiss();
                _presentation = null;
            }
        }

        private void CreatePresentation(Display display, Game game)
        {
            DismissPresentation();
            _presentation = new PikaPresentation(this, display, game);

            try
            {
                _presentation.Show();
            }
            catch (WindowManagerInvalidDisplayException ex)
            {
                Console.WriteLine("Unable to show presentation, display was removed", ex);
                DismissPresentation();
            }
        }
    }
}