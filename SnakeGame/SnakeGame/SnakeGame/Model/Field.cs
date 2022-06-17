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
    internal class Field 
    {
        #region Fields
        private Cell[,] field;
        #endregion

        #region Constructors
        public Field(GameDifficulties difficulty)
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
            FieldSnake = new Snake(this);
            //initializing food
            FieldFood = new Food(this);
        }
        #endregion

        #region Properties
        public int FieldSize { get; } = 20;
        public Snake FieldSnake { get; set; }
        public Food FieldFood { get; set; }
        public GameDifficulties Difficulty { get; set; }
        #endregion

        #region Methods
        public Cell this[int rowInd, int colInd]
        {
            get { return field[rowInd, colInd]; }
        }

        public void SnakeFieldUpdate()
        {
            FieldSnake.SnakeUpdate();
            FieldFood.UpdateFood();
        }

        public bool FieldIsFull()
        {
            bool result= true;
            for (int i = 0; i < FieldSize; i++)
            {
                for (int j = 0; j < FieldSize; j++)
                {
                    if (field[i, j].CellType == CellTypes.EmptyCell)
                        result = false;
                }
            }
            return result;
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


    }
    enum GameDifficulties
    {
        Easy,
        Medium,
        Hard,
    }

}
