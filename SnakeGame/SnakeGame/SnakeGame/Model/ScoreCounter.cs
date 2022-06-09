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
        #region Fields
        private int score;
        #endregion

        #region Constructors
        public ScoreCounter()
        {
            Score = 0;
        }
        #endregion

        #region Properties
        public int Score
        {
            get => score; private set
            {
                score = value;
                OnPropertyChanged(nameof(Score));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Handlers
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods
        public void AddToScore(int ScoreIncreaseVal)
        {
            Score += ScoreIncreaseVal;
        }
        #endregion
    }
}
