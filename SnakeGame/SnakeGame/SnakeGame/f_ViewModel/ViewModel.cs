﻿using SnakeGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace SnakeGame.f_ViewModel
{
    internal class ViewModel
    {
        #region Fields
        private GameStates gameState;
        private GameDifficulties gameDifficulty=GameDifficulties.Easy;
        #endregion

        #region Constructors
        public ViewModel()
        {
            Field = new SnakeField(gameDifficulty);
            ScoreCounter = new ScoreCounter();
            Timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(0.2), };


            Field.MySnake.NotifySnakeIsDead += () => { GameState = GameStates.NotInGame; };
            Field.MySnake.NotifySnakeIsDead += () => { SnakeGameFileManager.SaveScoreToFile(ScoreCounter); };
            Field.MyFood.NotifyFoodIsEaten += (Food eatenFood) => { ScoreCounter.AddToScore(eatenFood.ScoreValue); };
            NotifyGameStateChanged += StateGameChanged_Handler;
            NotifyDifficultyGameChanged += DifficultyGameChanged_Handler;
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
                NotifyDifficultyGameChanged?.Invoke(value);
            }
        }
        #endregion

        #region Events
        internal event Action<GameStates> NotifyGameStateChanged;
        internal event Action<GameDifficulties> NotifyDifficultyGameChanged;
        #endregion

        #region Handlers
        private void StateGameChanged_Handler(GameStates state)
        {
            switch (state)
            {
                case GameStates.Paused:
                    PauseGame();
                    break;
                case GameStates.InGame:
                    RunGame();
                    break;
                case GameStates.NotInGame:
                    EndGame();
                    break;

            }
        }

        private void DifficultyGameChanged_Handler(GameDifficulties difficulty)
        {
            Field.Difficulty = difficulty;
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
        public void PauseGame()
        {
            Timer.Stop();
        }
        public void RunGame()
        {
            Timer.Start();
        }
        public void EndGame()
        {
            Timer.Stop();
            //something else here
        }
        public void ResetGame()
        {

        }
        public void ChangeSnakeDirection(Snake.MovementDirections newDirect)
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