using System;
using System.Diagnostics;
using System.Linq;
using ClientLauncher;
using GameLauncher;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.Core;
using PikaGames.PaperCast;
using Resources = PikaGames.Games.Core.Resources;

namespace PikaGames.Windows
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            RootGame.InitDesktop();

            GameBase game;
//            game = new GameLauncherGame();
            game = new ClientLauncherGame(new RootGame());

            using (game)
            {
                //var screen =
                //    System.Windows.Forms.Screen.AllScreens.First(
                 //       s => s.Primary);

                //var width = 1280;
                //var height = 720;


                //game.Window.Position = new Point((screen.WorkingArea.TileWidth - width) / 2, (screen.WorkingArea.TileHeight - height) / 2);

               // game._graphics.IsFullScreen = false;
                //game._graphics.PreferredBackBufferWidth = width;
                //game._graphics.PreferredBackBufferHeight = height;
                //game._graphics.ApplyChanges();

                //var player = game.AddPlayer("TruDan");
                Mouse.SetCursor(MouseCursor.FromTexture2D(Resources.Images.Cursor, 0, 0));
                RootGame.Instance.Run();

            }
        }
    }
#endif
}
