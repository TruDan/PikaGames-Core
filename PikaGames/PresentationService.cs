using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.Opengl;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;

namespace PikaGames
{
    [Service(Exported = false)]
    public class PresentationService : CastRemoteDisplayLocalService
    {
        private CastPresentation _presentation;


        public override void OnCreate()
        {
            base.OnCreate();

        }

        public override void OnCreatePresentation(Display display)
        {
            CreatePresentation(display);
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

        private void CreatePresentation(Display display)
        {
            DismissPresentation();
            _presentation = new FirstScreenPresentation(this, display);

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


    class FirstScreenPresentation : CastPresentation
    {
        private GLView1 _glView;
        //private MyGame _game;

        public FirstScreenPresentation(Context context, Display display) : base(context, display)
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create our OpenGL view, and display it
            _glView = new GLView1(Context);
            SetContentView(_glView);

            //_game = new MyGame();

            //SetContentView(_game.Services.GetService<View>());
            //_game.Run();
        }
    }
}