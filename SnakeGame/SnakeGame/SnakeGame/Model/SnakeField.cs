using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class SnakeField
    {
        #region FieldsAndProperties
        public int FieldSize { get; } = 20;
        public Cell[,] Field { get; set; }

        private Snake MySnake { get; set; }
        #endregion

        #region Constructors
        public SnakeField()
        {
            //emptying field
            Field = new Cell[FieldSize, FieldSize];
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Field[i, j] = new Cell(Cell.CellTypes.EmptyCell, j, i);
                }
            }
            //initializing snake
            MySnake = new Snake(this);

            TestFieldInit();
        }
        #endregion

        #region Methods

        #endregion

        #region Test
        public void TestFieldInit()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Debug.Write((int)Field[i, j].CellType+" ");
                }
                Debug.WriteLine("\n");
            }
        }
        #endregion
    }
}
