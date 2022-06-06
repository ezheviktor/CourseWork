using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class ScoreCounter : INotifyPropertyChanged
    {
        private int score;
        #region Fields
        public int Score
        {
            get => score; private set
            {
                score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        #endregion

        #region Constructors
        public ScoreCounter()
        {
            Score = 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Methods
        public void AddToScore(int ScoreIncreaseVal)
        {
            Score += ScoreIncreaseVal;
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
