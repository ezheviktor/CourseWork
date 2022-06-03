using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal class ScoreCounter
    {
        #region Fields
        public int Score { get; private set; }

        #endregion

        #region Constructors
        public ScoreCounter()
        {
            Score = 0;
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
