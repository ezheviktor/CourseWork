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
        private bool isEaten;
        #endregion

        #region Constructors
        public Food(SnakeField field)
        {
            Field = field;
            ChooseScoreValue();
            GenerateFood();
        }
        #endregion

        #region Properties
        public bool IsEaten
        {
            get => isEaten;
            set
            {
                isEaten = value;
                if (IsEaten)
                    NotifyFoodIsEaten?.Invoke(this);
            }
        }
        public int ScoreValue { get; set; }
        public SnakeField Field { get; init; }
        public Cell FoodCell { get; set; }
        #endregion

        #region Events
        public event Action<Food> NotifyFoodIsEaten;
        #endregion

        #region Methods
        public void ChooseScoreValue()
        {
            if (Field.Difficulty == GameDifficulties.Easy)
                ScoreValue = 100;
            else if (Field.Difficulty == GameDifficulties.Medium)
                ScoreValue = 200;
            else if (Field.Difficulty == GameDifficulties.Hard)
                ScoreValue = 300;
        }
        public void GenerateFood() //this method is not taking into account the case when all field is filled with snake and there is no empty place for new fruit
        {
            Random random = new Random();
            Cell RandomCellOnField;
            do
            {
                RandomCellOnField = Field[random.Next(0, Field.FieldSize), random.Next(0, Field.FieldSize)];
            } while (RandomCellOnField.CellType != CellTypes.EmptyCell);
            FoodCell = RandomCellOnField;
            FoodCell.CellType = CellTypes.FoodCell;
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
