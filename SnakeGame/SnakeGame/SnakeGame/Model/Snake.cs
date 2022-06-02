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
        private enum MovementDirections
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
            SnakeCells = new List<Cell>();

            //initializing snake in the middle of the map
            for (int i = 0; i <_snakeInitLength; i++)
            {
                SnakeCells.Add( Field.Field[Field.FieldSize / 2 , Field.FieldSize / 2+i]);
            }
            foreach(Cell cell in SnakeCells)
            {
                cell.CellType = Cell.CellTypes.SnakeCell;
            }
        }
        #endregion

        #region Methods
        public void Move()
        {

        }

        public IEnumerator GetEnumerator()
        {
            return SnakeCells.GetEnumerator();
        }
        #endregion
    }
}
