using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class StatsItem:INotifyPropertyChanged
    {
        #region Fields
        private GameDifficulties gameDifficulty;
        private DateTime gameDate;
        private int gameScore;
        #endregion

        #region Constructors

        public StatsItem(GameDifficulties GameDifficulty, DateTime GameDate, int GameScore )
        {
            this.GameDifficulty = GameDifficulty;
            this.GameDate = GameDate;
            this.GameScore = GameScore;

        }
        #endregion

        #region Properties
        public GameDifficulties GameDifficulty
        {
            get => gameDifficulty;
            set
            {
                gameDifficulty = value;
                OnPropertyChanged(nameof(GameDifficulty));
            }
        }
        public DateTime GameDate
        {
            get => gameDate;
            set
            {
                gameDate = value;
                OnPropertyChanged(nameof(GameDate));
            }
        }
        public int GameScore
        {
            get => gameScore;
            set
            {
                gameScore = value;
                OnPropertyChanged(nameof(GameScore));
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
        #endregion
    }
}
