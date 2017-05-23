using System;

namespace PikaGames.Games.Core.UI.Grid
{
    public class UiGridItem : UiContainer
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public int RowSpan { get; set; } = 1;
        public int ColumnSpan { get; set; } = 1;

        public override int X => Column * (_grid.CellWidth + _grid.CellSpacing);
        public override int Y => Row * (_grid.CellHeight + _grid.CellSpacing);

        public override int Width => _grid.CellWidth * ColumnSpan + ((ColumnSpan - 1) * _grid.CellSpacing);
        public override int Height => _grid.CellHeight * RowSpan + ((RowSpan - 1) * _grid.CellSpacing);

        public UiItem Child { get; }

        private UiGridContainer _grid { get; }

        public UiGridItem(UiGridContainer gridContainer, UiItem item, int row, int col, int rowSpan = 1,
            int colSpan = 1) : base(gridContainer, 0, 0)
        {
            _grid = gridContainer;
            Child = item;

            Row = row;
            Column = col;

            RowSpan = Math.Max(1, rowSpan);
            ColumnSpan = Math.Max(1, colSpan);
        }
    }
}
