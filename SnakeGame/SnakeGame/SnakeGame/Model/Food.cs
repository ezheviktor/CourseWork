using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class Food
    {
        #region Fields
        public Cell FoodCell { get; set; }

        public SnakeField Field { get; init; }
        #endregion

        #region Constructors
        public Food(SnakeField field)
        {
            Field = field;
            GenerateFood();
        }
        #endregion

        #region Methods
        public void GenerateFood()
        {
            Random random = new Random();
            Cell RandomCellOnField;
            do
            {
                RandomCellOnField = Field[random.Next(0, Field.FieldSize), random.Next(0, Field.FieldSize)]; 
            } while (RandomCellOnField.CellType==Cell.CellTypes.SnakeCell || RandomCellOnField.CellType==Cell.CellTypes.FoodCell);
            FoodCell = RandomCellOnField;
            FoodCell.CellType = Cell.CellTypes.FoodCell;
        }
        #endregion
    }
}
