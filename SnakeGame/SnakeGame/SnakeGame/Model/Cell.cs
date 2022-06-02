using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class Cell
    {
        #region Fields
        public enum CellTypes
        {
            EmptyCell,
            FoodCell,
            SnakeCell,
        }
        public CellTypes CellType  { get ; set ; }

        public int ColCoord { get; set; }
        public int RowCoord { get; set; }
        #endregion

        #region Constructors
        public Cell(CellTypes cellType, int RowCoord, int ColCoord)
        {
            CellType = cellType;
            this.RowCoord = RowCoord;
            this.ColCoord = ColCoord;
        }
        #endregion

        #region Methods
        #endregion
    }
}
