using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PikaGames.PaperCast;

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
            using (var game = new PaperCastGame())
            {
                //var screen =
                //    System.Windows.Forms.Screen.AllScreens.First(
                 //       s => s.Primary);

                //var width = 1280;
                //var height = 720;


                //game.Window.Position = new Point((screen.WorkingArea.Width - width) / 2, (screen.WorkingArea.Height - height) / 2);

               // game._graphics.IsFullScreen = false;
                //game._graphics.PreferredBackBufferWidth = width;
                //game._graphics.PreferredBackBufferHeight = height;
                //game._graphics.ApplyChanges();

                //var player = game.AddPlayer("TruDan");


                game.Run();


            }
        }
    }
#endif
}
