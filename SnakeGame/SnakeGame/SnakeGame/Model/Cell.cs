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
            FruitCell,
            SnakeCell,
            SnakeHeadCell
        }
        public CellTypes CellType  { get ; set ; }

        public int CoordX { get; set; }
        public int CoordY { get; set; }
        #endregion

        #region Constructors
        public Cell(CellTypes cellType, int coordX, int coordY)
        {
            CellType = cellType;
            CoordX = coordX;
            CoordY = coordY;
        }
        #endregion

        #region Methods
        #endregion
    }
}
