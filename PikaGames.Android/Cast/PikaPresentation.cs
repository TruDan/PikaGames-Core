using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using PikaGames.Android.Cast.MonoGame;
using PikaGames.Games.Core;
using Debug = System.Diagnostics.Debug;

namespace PikaGames.Android.Cast
{
    internal class PikaPresentation : CastPresentation
    {
        private readonly AndroidGameWrapper _game;
        
        public PikaPresentation(Context context, Display display, AndroidGameWrapper game) : base(context, display)
        {
            _game = game;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //_game = new MyGame();
            Debug.WriteLine("PikaPresentation OnCreate");
            SetContentView((View) _game.Services.GetService<View>());

            _game.Run();
        }
    }
}