using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal static class SnakeGameFileManager
    {
        #region Fields
        private static string ScoreFileName { get; set; }
        private static string DifficultyFileName { get; set; }
        #endregion

        #region Constructors
        static SnakeGameFileManager()
        {
            ScoreFileName = "ScoreCounterLog";
            DifficultyFileName = "DifficultyLog";
        }
        #endregion

        #region Methods
        public static void SaveScoreToFile(ScoreCounter newScore)
        {
            using StreamWriter writer = new StreamWriter(File.Open(ScoreFileName, FileMode.OpenOrCreate));
            {
                writer.WriteLine(newScore.Score);
            }
        }

        public static List<int> GetScoresFromFile()
        {
            List<int> scores = new List<int>();
            using (StreamReader reader = new StreamReader(File.Open(ScoreFileName, FileMode.Open)))
            {
                while (!reader.EndOfStream)
                {
                    scores.Add(int.Parse(reader.ReadLine()));
                }
            }
            return scores;
        }

        public static void SaveDifficultyToFile(string difficulty)
        {
            using (StreamWriter writer = new StreamWriter(File.Open(DifficultyFileName, FileMode.Create)))
            {
                writer.Write(difficulty);
            }
        }

        public static string GetDifficultyFromFile()
        {
            string difficulty;
            using (StreamReader reader = new StreamReader(File.Open(DifficultyFileName, FileMode.Open)))
            {
                difficulty = reader.ReadLine();
            }
            return difficulty;
        }
        #endregion
    }
}
