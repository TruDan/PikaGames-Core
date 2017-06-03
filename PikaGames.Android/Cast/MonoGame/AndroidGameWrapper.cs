using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Cast;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core;

namespace PikaGames.Android.Cast.MonoGame
{
    public class AndroidGameWrapper : RootGame
    {
        private readonly GameBase AndroidGame;

        public AndroidGameWrapper(Func<RootGame, GameBase> gameFactory) : base()
        {
            AndroidGame = gameFactory.Invoke(this);
        }

    }
}