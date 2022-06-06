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
            gameDifficulty = StrToDifficultyConvert(SnakeGameFileManager.GetDifficultyFromFile());
            Field = new SnakeField(gameDifficulty);
            ScoreCounter = new ScoreCounter();
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.25 - 0.0750 * (int)gameDifficulty) };
            


            Field.MySnake.NotifySnakeIsDead += () => { GameState = GameStates.NotInGame; };
            Field.MySnake.NotifySnakeIsDead += () => { SnakeGameFileManager.SaveScoreToFile(ScoreCounter); };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { ScoreCounter.AddToScore(eatenFood.ScoreValue); };
            NotifyGameStateChanged += StateGameChanged_Handler;
            //NotifyDifficultyGameChanged += DifficultyGameChanged_Handler;
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
                //NotifyDifficultyGameChanged?.Invoke(value);
            }
        }
        #endregion

        #region Events
        internal event Action<GameStates> NotifyGameStateChanged;
        //internal event Action<GameDifficulties> NotifyDifficultyGameChanged;
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
                    break;

            }
        }

        //private void DifficultyGameChanged_Handler(GameDifficulties difficulty)
        //{
        //    Field.Difficulty = difficulty;
        //}
        private void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (GameState == GameStates.InGame)
            {
                Field.SnakeFieldUpdate();
            }
        }
        #endregion

        #region Methods
        //public void PauseGame()
        //{
        //    Timer.Stop();
        //}
        //public void RunGame()
        //{
        //    Timer.Start();
        //}
        //public void EndGame()
        //{
        //    Timer.Stop();
        //    //something else here
        //}

        public void ChangeSnakeDirection(MovementDirections newDirect)
        {
            Field.MySnake.SnakeDirection = newDirect;
        }

        public GameDifficulties StrToDifficultyConvert(string difficulty)
        {
            switch (difficulty)
            {
                case "hard":
                    return GameDifficulties.Hard;
                case "medium":
                    return GameDifficulties.Medium;
                case "easy":
                    return GameDifficulties.Easy;
                default:
                    return GameDifficulties.Easy;
            }
        }
        #endregion
    }
    enum GameStates
    {
        InGame, NotInGame, Paused
    }

}
