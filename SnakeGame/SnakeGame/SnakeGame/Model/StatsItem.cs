using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class StatsItem:INotifyPropertyChanged
    {
        #region Fields
        private string gameDifficulty;
        private string gameDate;
        private string score;
        #endregion

        #region Constructors
        public StatsItem(string statString)
        {
            List<string> statStringParts=new List<string>(statString.Split());
            GameDifficulty=statStringParts[0];
            GameDate=statStringParts[1]+statString[2];
            Score=statStringParts[4];

        }
        #endregion

        #region Properties
        public string GameDate
        {
            get => gameDate;
            set
            {
                gameDate = value;
                OnPropertyChanged(nameof(GameDate));
            }
        }
        public string Score
        {
            get => score;
            set
            {
                score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public string GameDifficulty
        {
            get => gameDifficulty;
            set
            {
                gameDifficulty = value;
                OnPropertyChanged(nameof(GameDifficulty));
            }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Methods
        #endregion
    }
}
