using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SnakeGame.f_ViewModel
{
    internal class ViewModel
    {

        #region Fields
        private GameStates gameState;
        private GameDifficulties gameDifficulty;
        #endregion

        #region Constructors
        public ViewModel()
        {
            gameDifficulty = SnakeGameFileManager.GetDifficultyFromFile();
            Field = new SnakeField(gameDifficulty);
            ScoreCounter = new ScoreCounter();
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.25 - 0.0750 * (int)gameDifficulty) };


            Field.MySnake.NotifySnakeIsDead += () => { GameState = GameStates.NotInGame; };
            //Field.MySnake.NotifySnakeIsDead += () => {  };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { ScoreCounter.AddToScore(eatenFood.ScoreValue); };
            NotifyGameStateChanged += StateGameChanged_Handler;
            Timer.Tick += DispatcherTimer_Tick;
        }

        #endregion

        #region Properties
        internal SnakeField Field { get; set; }
        internal ScoreCounter ScoreCounter { get; set; }
        internal DispatcherTimer Timer { get; set; }
        internal GameStates GameState
        {
            get => gameState;
            set
            {
                gameState = value;
                NotifyGameStateChanged?.Invoke(value);
            }
        }
        internal GameDifficulties GameDifficulty
        {
            get => gameDifficulty;
            set
            {
                gameDifficulty = value;
            }
        }
        #endregion

        #region Events
        internal event Action<GameStates> NotifyGameStateChanged;
        #endregion

        #region Handlers
        private void StateGameChanged_Handler(GameStates state)
        {
            switch (state)
            {
                case GameStates.Paused:
                    Timer.Stop();
                    break;
                case GameStates.InGame:
                    Timer.Start();
                    break;
                case GameStates.NotInGame:
                    Timer.Stop();
                    SnakeGameFileManager.SaveStatistics(new StatsItem(gameDifficulty, DateTime.Now, ScoreCounter.Score));
                    break;

            }
        }

        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (GameState == GameStates.InGame)
            {
                Field.SnakeFieldUpdate();
            }
        }
        #endregion

        #region Methods

        public void ChangeSnakeDirection(MovementDirections newDirect)
        {
            Field.MySnake.SnakeDirection = newDirect;
        }
        #endregion
    }
    enum GameStates
    {
        InGame, NotInGame, Paused
    }

}
