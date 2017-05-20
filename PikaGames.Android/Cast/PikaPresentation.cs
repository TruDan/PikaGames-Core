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
using PikaGames.Games.PaperCast;

namespace PikaGames.Android.Cast
{
    internal class PikaPresentation : CastPresentation
    {
        private Game _game;

        public PikaPresentation(Context context, Display display, Game game) : base(context, display)
        {
            _game = game;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            //_game = new MyGame();

            SetContentView((View) _game.Services.GetService<View>());
            
            _game.Run(GameRunBehavior.Asynchronous);
        }
    }
}