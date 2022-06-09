using SnakeGame.f_ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class SnakeField 
    {
        #region Fields
        private Cell[,] field;
        #endregion

        #region Constructors
        public SnakeField(GameDifficulties difficulty)
        {
            //setting difficulty
            Difficulty = difficulty;
            //emptying field
            field = new Cell[FieldSize, FieldSize];
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    field[i, j] = new Cell(CellTypes.EmptyCell, i, j);
                }
            }
            //initializing snake
            MySnake = new Snake(this);
            //initializing food
            MyFood = new Food(this);
        }
        #endregion

        #region Properties
        public int FieldSize { get; } = 20;
        public Snake MySnake { get; set; }
        public Food MyFood { get; set; }
        public GameDifficulties Difficulty { get; set; }
        #endregion

        #region Methods
        public Cell this[int rowInd, int colInd]
        {
            get { return field[rowInd, colInd]; }
        }

        public void SnakeFieldUpdate()
        {
            MySnake.SnakeUpdate();
            MyFood.UpdateFood();
        }

        //public void ResetField()
        //{
        //    for (int i = 0; i < FieldSize; i++)
        //    {
        //        for (int j = 0; j < FieldSize; j++)
        //        {
        //            Field[i, j].CellType = Cell.CellTypes.EmptyCell;
        //        }
        //    }
        //    //initializing snake
        //    MySnake = new Snake(this);
        //    //initializing food
        //    MyFood = new Food(this);
        //}
        #endregion

        #region Test
        public void TestFieldDebuggerDisplay()
        {
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    Debug.Write((int)field[i, j].CellType + " ");
                }
                Debug.WriteLine("\n");
            }
            Debug.WriteLine("----------------------------------------------------------------------");
            Debug.WriteLine("");
            Debug.WriteLine("");
        }
        public void TestCheckBorderCrossing()
        {
            for (int i = 0; i < 15; i++)
            {
                TestFieldDebuggerDisplay();
                SnakeFieldUpdate();
            }
        }
        public void TestCheckSnakeDeath()
        {
            TestFieldDebuggerDisplay();

            MySnake.SnakeDirection = MovementDirections.Up;
            SnakeFieldUpdate();
            Debug.WriteLine(MySnake.IsDead);
            TestFieldDebuggerDisplay();

            MySnake.SnakeDirection = MovementDirections.Right;
            SnakeFieldUpdate();
            Debug.WriteLine(MySnake.IsDead);
            TestFieldDebuggerDisplay();

            MySnake.SnakeDirection = MovementDirections.Down;
            SnakeFieldUpdate();
            Debug.WriteLine(MySnake.IsDead);
            TestFieldDebuggerDisplay();
        }
        #endregion
    }
    enum GameDifficulties
    {
        Easy,
        Medium,
        Hard,
    }

}
