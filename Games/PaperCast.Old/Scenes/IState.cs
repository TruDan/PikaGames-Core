using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PikaGames.Games.PaperCast.States
{
    public interface IState
    {

        void Update(GameTime deltaTime);

        void Draw(GameTime deltaTime);

    }
}
