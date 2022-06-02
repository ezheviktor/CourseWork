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

        private Cell[,] Field { get; set; }
        public Snake MySnake { get; set; }
        public Food MyFood { get; set; }
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
                    Field[i, j] = new Cell(Cell.CellTypes.EmptyCell, i, j);
                }
            }
            //initializing snake
            MySnake = new Snake(this);
            //initializing food
            MyFood=new Food(this);
        }
        #endregion

        #region Methods
        public Cell this[int rowInd, int colInd]
        {
            get { return Field[rowInd, colInd]; }
        }
        public void SnakeFieldUpdate()
        {
            MySnake.SnakeUpdate();
            MyFood.UpdateFood();
        }
        #endregion

        #region Test
        public void TestFieldDebuggerDisplay()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Debug.Write((int)Field[i, j].CellType+" ");
                }
                Debug.WriteLine("\n");
            }
            Debug.WriteLine("----------------------------------------------------------------------");
            Debug.WriteLine("");
            Debug.WriteLine("");
        }
        public void CheckBorderCrossing()
        {
            for (int i = 0; i < 15; i++)
            {
                TestFieldDebuggerDisplay();
                SnakeFieldUpdate();
            }
        }
        public void CheckSnakeDeath()
        {
            
        }
        #endregion
    }
}
