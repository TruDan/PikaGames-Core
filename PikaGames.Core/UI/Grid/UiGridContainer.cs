using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PikaGames.Games.Core.UI.Grid
{
    public class UiGridContainer : UiContainer
    {
        public int Rows { get; }
        public int Columns { get; }

        public int CellWidth { get; set; } = 50;
        public int CellHeight { get; set; } = 50;
        public int CellSpacing { get; set; } = 10;

        protected UiGridItem[,] Grid { get; }

        public UiGridContainer(UiContainer parent, int rows, int cols) : base(parent, 0, 0)
        {
            Rows = rows;
            Columns = cols;

            Grid = new UiGridItem[cols, rows];
        }

        public void AddItem(UiItem item, int col, int row, int rowSpan = 1, int colSpan = 1)
        {
            var gridItem = new UiGridItem(this, item, row, col, rowSpan, colSpan);
            item.SetParent(gridItem);
            
            for (int i = 0; i < gridItem.ColumnSpan; i++)
            {
                for (int j = 0; j < gridItem.RowSpan; j++)
                {
                    Grid[gridItem.Column + i, gridItem.Row + j] = gridItem;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

        }
    }
}
