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
        #region Fields
        private bool canChangeDirection; //fixes bug of changing direction twice per update
        private MovementDirections snakeDirection;
        private bool isDead;
        private int snakeInitLength = 4;
        private const int snakeHeadIndex = 0;
        #endregion

        #region Constructors
        public Snake(Field field)
        {
            //reference to the field we create snake in
            Field = field;
            if (field.FieldSize < snakeInitLength)
                throw new ArgumentException("Field is too samll. Snake can`t fit in it");

            //initializing snake in the middle of the map
            SnakeCells = new List<Cell>();
            for (int i = 0; i < snakeInitLength; i++)
            {
                AddCellAt(SnakeCells.Count, Field[Field.FieldSize / 2, Field.FieldSize / 2 + i]);
            }
            //initializing snake direction
            canChangeDirection = true;
            SnakeDirection = MovementDirections.Left;
            //initializing other fields
            IsDead = false;
        }
        #endregion

        #region Properties
        public List<Cell> SnakeCells { get; set; }
        public Field Field { get; init; }
        public MovementDirections SnakeDirection //needs refactoring to make logic more transparent
        {
            get { return snakeDirection; }
            set
            {
                if (Convert.ToBoolean(((int)value + (int)SnakeDirection) % 2) && canChangeDirection)
                {
                    snakeDirection = value;
                    canChangeDirection = false;
                }
            }
        }
        public bool IsDead
        {
            get => isDead;
            set
            {
                isDead = value;
                if (value == true)
                    NotifySnakeIsDead?.Invoke();
            }
        }
        #endregion

        #region Events
        public event Action NotifySnakeIsDead;
        #endregion

        #region Methods
        public void RemoveCellAt(int index)
        {
            SnakeCells[index].CellType = CellTypes.EmptyCell;
            SnakeCells.RemoveAt(index);
        }

        public void AddCellAt(int index, Cell cell)
        {
            int SnakeTailIndex = SnakeCells.Count;
            //if (index != snakeHeadIndex && index != SnakeTailIndex)
            //    throw new Exception("Can`t add cell to the middle of the snake");

            if(index==snakeHeadIndex|| index==SnakeTailIndex)
            {
            if (index == snakeHeadIndex)
                SnakeCells.Insert(snakeHeadIndex, cell);
            else if (index == SnakeTailIndex)
                SnakeCells.Insert(SnakeTailIndex, cell);
            cell.CellType = CellTypes.SnakeCell;
            }
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
            int RowCoord = SnakeCells[snakeHeadIndex].RowCoord + deltaRow;
            int ColCoord = SnakeCells[snakeHeadIndex].ColCoord + deltaCol;

            //this conditional doesn`t need to be here(needed refactoring)
            if (Field.Difficulty == GameDifficulties.Hard &&
                (RowCoord > Field.FieldSize - 1 || RowCoord < 0 || ColCoord > Field.FieldSize - 1 || ColCoord < 0))
            {
                IsDead = true;
            }

            RowCoord = RowCoord >= 0 ? (RowCoord % 20) : (RowCoord + Field.FieldSize);
            ColCoord = ColCoord >= 0 ? (ColCoord % 20) : (ColCoord + Field.FieldSize);

            return Field[RowCoord, ColCoord];
        }

        public void MoveOneStep(Cell nextCell)
        {
            RemoveCellAt(SnakeCells.Count - 1);
            AddCellAt(snakeHeadIndex, nextCell);
        }

        public void Eat(Cell NextCell)
        {
            Cell GrowingCell = SnakeCells[SnakeCells.Count - 1];
            MoveOneStep(NextCell);
            AddCellAt(SnakeCells.Count, GrowingCell);
            Field.FieldFood.IsEaten = true;
        }
        public void SnakeUpdate()
        {
            Cell NextCell = GetNextCell();
            if (!IsDead)
            {
                switch (NextCell.CellType)
                {
                    case CellTypes.EmptyCell:
                        MoveOneStep(NextCell);
                        break;
                    case CellTypes.FoodCell:
                        Eat(NextCell);
                        break;
                    case CellTypes.SnakeCell:
                        IsDead = true;
                        break;
                }
            }
            canChangeDirection = true;
        }

        #endregion
    }
    public enum MovementDirections
    {
        Left = 0,
        Up = 1,
        Right = 2,
        Down = 3,
    }
}
