using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class Snake
    {
        #region Events
        public event Action NotifySnakeIsDead;
        #endregion

        #region Fields and Properties
        public enum MovementDirections
        {
            Left, Up, Right, Down
        }
        public MovementDirections SnakeDirection { get; set; }

        private bool isDead;
        public bool IsDead
        {
            get => isDead;
            set
            {
                if (value == true)
                    isDead = value;
                //NotifySnakeIsDead;
            }
        }
        public int _snakeInitLength = 4;

        public int SnakeHeadIndex { get; } = 0;
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
                    deltaRow = -1;
                    break;
                case MovementDirections.Right:
                    deltaCol = 1;
                    break;
                case MovementDirections.Down:
                    deltaRow = 1;
                    break;
                case MovementDirections.Left:
                    deltaCol = -1;
                    break;
            }
            //refactoring needed here
            //the trick is that we divide final coordinates by twenty to take into acoount he fact that we can cross the border and coordianted will drop
            int RowCoord = SnakeCells[SnakeHeadIndex].RowCoord + deltaRow;
            int ColCoord = SnakeCells[SnakeHeadIndex].ColCoord + deltaCol;

            RowCoord = RowCoord >= 0 ? (RowCoord % 20) : (RowCoord + Field.FieldSize);
            ColCoord=ColCoord>=0? (ColCoord % 20) : (ColCoord + Field.FieldSize);
            return Field[RowCoord, ColCoord];
        }

        public void MoveOneStep()
        {
            RemoveCellAt(SnakeCells.Count-1);
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
        #endregion
    }
}
