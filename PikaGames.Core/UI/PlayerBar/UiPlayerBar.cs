using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PikaGames.Games.Core.UI.ButtonBar;

namespace PikaGames.Games.Core.UI.PlayerBar
{
    public class UiPlayerBar : UiContainer
    {
        public const int Spacing = 25;
        public int MaxPlayers { get; set; } = 4;

        public override int Width => ((int)RootGame.Instance.VirtualSize.X - (2*Spacing));

        public override Vector2 Position => base.Position + new Vector2(0, -Height);

        public UiPlayerBar(UiContainer container) : base(container, Spacing, RootGame.Instance.ViewportAdapter.ViewportHeight - Spacing)
        {

        }

        public UiPlayerBar() : this(null)
        {
            AddDefaults();
        }

        public void AddDefaults()
        {
            AddPlayer(PlayerIndex.One);
            AddPlayer(PlayerIndex.Two);
            AddPlayer(PlayerIndex.Three);
            AddPlayer(PlayerIndex.Four);
        }

        public void AddPlayer(PlayerIndex playerIndex)
        {
            var buttonBarItem = new UiPlayerBarItem(this, ((Width/ MaxPlayers) * (int)playerIndex) + Spacing, 0, playerIndex);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DistributeChildren(Width);
        }
    }
}
