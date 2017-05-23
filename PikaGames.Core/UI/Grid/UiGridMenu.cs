using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PikaGames.Games.Core.Input;
using PikaGames.Games.Core.UI.Controls;
using PikaGames.Games.Core.UI.Menu;

namespace PikaGames.Games.Core.UI.Grid
{
    public class UiGridMenu : UiGridContainer
    {
        private int _selectedX = 0;
        private int _selectedY = 0;

        public UiGridItem ActiveItem => Grid[_selectedX,_selectedY];

        public UiGridMenu(UiContainer parent, int rows, int cols) : base(parent, rows, cols)
        {

        }

        private bool CheckItem(int x, int y)
        {
            var item = Grid[x, y];
            if (item == null) return false;

            if (item.Row == _selectedY && item.Column == _selectedX) return false;

            var child = item?.Child as ISelectable;
            if (child != null)
            {
                _selectedX = item.Column;
                _selectedY = item.Row;

                return true;
            }
            return false;
        }

        private void NavigateUp()
        {
            var x = _selectedX;
            var y = _selectedY;
            for (int i = 0; i < Rows; i++)
            {
                if (y == 0) y = Rows - 1;

                if (CheckItem(x, y))
                    return;

                y--;
            }
        }

        private void NavigateDown()
        {
            var x = _selectedX;
            var y = _selectedY;
            for (int i = 0; i < Rows; i++)
            {
                if (y == Rows-1) y = 0;

                if (CheckItem(x, y))
                    return;

                y++;
            }
        }

        private void NavigateLeft()
        {
            var x = _selectedX;
            var y = _selectedY;
            for (int i = 0; i < Columns; i++)
            {
                if (x <= 0) x = Columns - 1;

                if (CheckItem(x, y))
                    return;

                x--;
            }
        }

        private void NavigateRight()
        {
            var x = _selectedX;
            var y = _selectedY;
            for (int i = 0; i < Columns; i++)
            {
                if (x >= Columns - 1) x = 0;

                if (CheckItem(x, y))
                    return;

                x++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsFocused)
            {

                var c = (ActiveItem.Child as UiControl);
                if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Up)))
                {
                    if (c != null) c.IsSelected = false;

                    NavigateUp();

                    GameBase.Instance.SoundManager.Play(Resources.Sfx.UI_Zap);
                }
                else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Down)))
                {
                    if (c != null) c.IsSelected = false;

                    NavigateDown();

                    GameBase.Instance.SoundManager.Play(Resources.Sfx.UI_Zap);
                }
                else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Left)))
                {
                    if (c != null) c.IsSelected = false;

                    NavigateLeft();
                    GameBase.Instance.SoundManager.Play(Resources.Sfx.UI_Zap);
                }
                else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.Right)))
                {
                    if (c != null) c.IsSelected = false;

                    NavigateRight();
                    GameBase.Instance.SoundManager.Play(Resources.Sfx.UI_Zap);
                }
                else if (GameBase.Instance.Players.Any(p => p.Input.IsPressed(InputCommand.A)))
                {
                    if (c != null) c.Focus();
                }

                c = (ActiveItem.Child as UiControl);
                if (c != null) c.IsSelected = true;
            }
            base.Update(gameTime);
        }
    }
}
