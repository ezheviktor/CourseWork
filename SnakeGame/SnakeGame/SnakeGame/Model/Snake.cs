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
        public int SnakeHeadIndex { get; } = 0;
        public bool IsDead { get; set; }
        public MovementDirections SnakeDirection { get; set; }
        public List<Cell> SnakeCells { get; set; }

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
            for (int i = 0; i < _snakeInitLength; i++)
            {
                AddCellAt(SnakeCells.Count, Field[Field.FieldSize / 2, Field.FieldSize / 2 + i]);
            }
            //initializing snake direction
            SnakeDirection = MovementDirections.Left;
            //initializing other fields
            IsDead = false;
        }
        #endregion

        #region Methods

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

            if (index == SnakeHeadIndex)
                SnakeCells.Insert(SnakeHeadIndex, cell);
            if (index == SnakeTailIndex)
                SnakeCells.Insert(SnakeTailIndex, cell);
            cell.CellType = Cell.CellTypes.SnakeCell;
        }

        public Cell GetNextCell()
        {
            int deltaRow = 0, deltaCol = 0;
            switch (SnakeDirection)
            {
                case MovementDirections.Up:
                    deltaCol = 1;
                    break;
                case MovementDirections.Right:
                    deltaRow = 1;
                    break;
                case MovementDirections.Down:
                    deltaCol = -1;
                    break;
                case MovementDirections.Left:
                    deltaRow = -1;
                    break;
            }
            //refactoring needed here
            //the trick is that we divide final coordinates by twenty to take into acoount he fact that we can cross the border and coordianted will drop
            return Field[(SnakeCells[SnakeHeadIndex].RowCoord + deltaRow) % Field.FieldSize, (SnakeCells[SnakeHeadIndex].RowCoord + deltaCol) % Field.FieldSize];
        }

        public void MoveOneStep()
        {
            RemoveCellAt(SnakeCells.Count);
            AddCellAt(SnakeHeadIndex, GetNextCell());
        }

        public void Eat()
        {
            Cell GrowingCell = SnakeCells[SnakeCells.Count - 1];
            MoveOneStep();
            AddCellAt(SnakeCells.Count, GrowingCell);
            Field.MyFood.IsEaten = true;
        }
        public void SnakeUpdate()
        {
            if (!IsDead)
            {
                Cell NextCell = GetNextCell();
                switch (NextCell.CellType)
                {
                    case Cell.CellTypes.EmptyCell:
                        MoveOneStep();
                        break;
                    case Cell.CellTypes.FoodCell:
                        Eat();
                        break;
                    case Cell.CellTypes.SnakeCell:
                        IsDead = true;
                        break;
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            return SnakeCells.GetEnumerator();
        }
        #endregion
    }
}
