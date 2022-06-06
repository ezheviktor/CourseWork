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
        #region FieldsAndProperties
        public GameDifficulties Difficulty { get; set; }
        public int FieldSize { get; } = 20;

        private Cell[,] field;
        private Cell[,] Field
        {
            get => field;
            set
            {
                field = value;
            }
        }
        public Snake MySnake { get; set; }
        public Food MyFood { get; set; }
   
        #endregion

        #region Constructors
        public SnakeField(GameDifficulties difficulty)
        {
            //setting difficulty
            Difficulty = difficulty;
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
            MyFood = new Food(this);
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
                    Debug.Write((int)Field[i, j].CellType + " ");
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

            MySnake.SnakeDirection = Snake.MovementDirections.Up;
            SnakeFieldUpdate();
            Debug.WriteLine(MySnake.IsDead);
            TestFieldDebuggerDisplay();

            MySnake.SnakeDirection = Snake.MovementDirections.Right;
            SnakeFieldUpdate();
            Debug.WriteLine(MySnake.IsDead);
            TestFieldDebuggerDisplay();

            MySnake.SnakeDirection = Snake.MovementDirections.Down;
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
