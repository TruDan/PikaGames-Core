
using Android.App;
using Android.Gms.Cast;
using Android.Hardware.Display;
using Android.Views;
using GameLauncher;
using Java.Lang;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.Android.Cast.MonoGame;
using PikaGames.Games.Core;
using Debug = System.Diagnostics.Debug;

namespace PikaGames.Android.Cast
{
    [Service(Exported = false 
        //,Process = "PikaGames.Android.Cast.PikaCastProcess"
        )]
    public class PikaPresentationService : CastRemoteDisplayLocalService
    {
        private CastPresentation _presentation;

        public PikaPresentationService()
        {
            Debug.WriteLine("---------------------------");
            Debug.WriteLine("> PikaPresentationService <");
            Debug.WriteLine("---------------------------");
            SetDebugEnabled();
            //SetForeground(true);
        }

        public override void OnCreate()
        {
            Debug.WriteLine("---------------------------");
            Debug.WriteLine("> OnCreate                <");
            Debug.WriteLine("---------------------------");
            base.OnCreate();
        }

        public override void OnCreatePresentation(Display display)
        {
            Debug.WriteLine("---------------------------");
            Debug.WriteLine("> OnCreatePresentation    <");
            Debug.WriteLine("---------------------------");
            var g = new AndroidGameWrapper(rg => new GameLauncherGame(rg));
            //var rootgame = RootGame.Instance;
            
            //RootGame.Instance.LoadGame(g);
            
            CreatePresentation(display, g);
        }

        public override void OnDismissPresentation()
        {
            DebugGraphics();
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

        private void CreatePresentation(Display display, AndroidGameWrapper game)
        {
            DismissPresentation();
            

            //var pp = game.GraphicsDevice.PresentationParameters;
            //pp.DeviceWindowHandle = display.Handle;
            
            _presentation = new PikaPresentation(this, display, game);

            try
            {
                _presentation.Show();
                DebugGraphics();
                Debug.WriteLine("Presentation Started");

            }
            catch (WindowManagerInvalidDisplayException ex)
            {
                Debug.WriteLine("Unable to show presentation, display was removed", ex);
                DismissPresentation();
            }
        }

        private void DebugGraphics()
        {
            DisplayManager d = (DisplayManager)ApplicationContext.GetSystemService(Class.FromType(typeof(DisplayManager)));

            foreach (var display in d.GetDisplays())
            {
                Debug.WriteLine("DISPLAY: " + display.DisplayId + " - " + display.Name + " - " + display.State + " - " + display.Flags);
            }

            RootGame.DebugGraphics();

            foreach (var adapter in GraphicsAdapter.Adapters)
            {
                Debug.WriteLine("Graphics Adapter: " + adapter.Description + " " + adapter.IsWideScreen + " - " + adapter.CurrentDisplayMode.ToString());

                foreach (var e in adapter.SupportedDisplayModes)
                {
                    Debug.WriteLine(" - " + e.ToString());
                }
            }
        }
    }
}