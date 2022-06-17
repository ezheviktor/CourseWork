using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
    internal static class SnakeGameFileManager
    {
        #region Fields
        private static string StatsFileName { get; set; } = "ScoreCounterLog.json";
        private static string DifficultyFileName { get; set; } = "DifficultyLog.json";
        #endregion

        #region Constructors
        #endregion

        #region Methods


        public static void SaveStatistics(StatsItem sessionStats)
        {
            using (StreamWriter writer = new StreamWriter(File.Open(StatsFileName, FileMode.Append)))
            {
                writer.WriteLine(JsonSerializer.Serialize(sessionStats));
            }
        }

        public static List<StatsItem> GetStatistics()
        {
            List<StatsItem> statistics = new List<StatsItem>();
            using (StreamReader reader = new StreamReader(File.Open(StatsFileName, FileMode.Open)))
            {
                while (!reader.EndOfStream)
                {
                    string readItem = reader.ReadLine() ?? string.Empty;
                    if (readItem != string.Empty)
                        statistics.Add(JsonSerializer.Deserialize<StatsItem>(readItem));
                }
            }
            statistics.Reverse();
            return statistics;
        }

        public static void SaveDifficultyToFile(GameDifficulties difficulty)
        {
            using (StreamWriter writer = new StreamWriter(File.Open(DifficultyFileName, FileMode.Create)))
            {
                writer.Write(JsonSerializer.Serialize(difficulty));
            }

        }

        public static GameDifficulties GetDifficultyFromFile()
        {
            GameDifficulties difficulty;
            using (StreamReader reader = new StreamReader(File.Open(DifficultyFileName, FileMode.Open)))
            {
                string jsonDiff = reader.ReadLine();
                if (jsonDiff != null)
                    difficulty = JsonSerializer.Deserialize<GameDifficulties>(jsonDiff);
                else
                    throw new Exception("Difficulty file is empty.");
            }
            return difficulty;
        }
        #endregion
    }
}
