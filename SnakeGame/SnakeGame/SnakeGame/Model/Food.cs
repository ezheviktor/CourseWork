using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class Food
    {
        #region Events
        public event Action<Food> NotifyFoodIsEaten;
        #endregion

        #region Fields
        public Cell FoodCell { get; set; }
        private bool isEaten;
        public bool IsEaten
        {
            get => isEaten;
            set
            {
                isEaten = value;
                NotifyFoodIsEaten?.Invoke(this);
            }
        }
        public int ScoreValue { get; set; } = 100;

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
        public void GenerateFood() //this method is not taking into account the case when all field is filled with snake and there is no empty place for new fruit
        {
            Random random = new Random();
            Cell RandomCellOnField;
            do
            {
                RandomCellOnField = Field[random.Next(0, Field.FieldSize), random.Next(0, Field.FieldSize)];
            } while (RandomCellOnField.CellType != Cell.CellTypes.EmptyCell);
            FoodCell = RandomCellOnField;
            FoodCell.CellType = Cell.CellTypes.FoodCell;
            IsEaten = false;
        }
        public void UpdateFood()
        {
            if (IsEaten)
            {
                GenerateFood();
            }
        }
        #endregion
    }
}
