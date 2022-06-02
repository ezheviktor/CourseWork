using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class Snake : IEnumerable
    {
        public enum MovementDirections
        {
            Left, Up, Right, Down
        }

        #region Fields and Properties
        public int _snakeInitLength = 4;
        public MovementDirections SnakeDirection { get; set; }  
        public int SnakeHeadIndex { get; } =0;
        public List<Cell> SnakeCells { get;  set; }
                                
        public SnakeField Field { get; init; }
        #endregion

        #region Constructors
        public Snake(SnakeField field)
        {
            //reference to the field we create snake in
            Field = field;
            if (field.FieldSize < _snakeInitLength)
                throw new ArgumentException("Field is too samll. Snake can`t fit in it");

            //initializing snake in the middle of the map
            SnakeCells = new List<Cell>();
            for (int i = 0; i <_snakeInitLength; i++)
            {
                AddCellAt(SnakeCells.Count, Field[Field.FieldSize / 2, Field.FieldSize / 2 + i]);
            }
            //initializing snake direction
            SnakeDirection = MovementDirections.Left;
        }
        #endregion

        #region Methods
        //public void Move()
        //{
        //    //Clearing the las element of the snake
        //    RemoveCellAt(SnakeCells.Count - 1);

        //    //Picking the next element of the snake

        //    Cell newCell;
        //    switch (SnakeDirection)
        //    {
        //        case MovementDirections.Left:
        //            {
        //                newCell = SnakeCells[];    
        //            }
        //    }
        //    SnakeCells.Insert(0, )
        //}
        public void RemoveCellAt(int index)
        {
            SnakeCells[index].CellType = Cell.CellTypes.EmptyCell;
            SnakeCells.RemoveAt(index);
        }

        public void AddCellAt(int index, Cell cell)
        {
            int SnakeTailIndex = SnakeCells.Count;
            if (index != SnakeHeadIndex && index != SnakeTailIndex)
                throw new Exception("Can`t add cell to the middle of the snake");

            if(index==SnakeHeadIndex)
                SnakeCells.Insert(SnakeHeadIndex, cell);
            if(index== SnakeTailIndex)
                SnakeCells.Insert(SnakeTailIndex, cell);
            cell.CellType = Cell.CellTypes.SnakeCell;
        }
        public IEnumerator GetEnumerator()
        {
            return SnakeCells.GetEnumerator();
        }
        #endregion
    }
}
