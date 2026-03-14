using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlTypes;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using pruktikum_dz_1;
using static System.Formats.Asn1.AsnWriter;

namespace С_Metods
{
    class Program
    {
        private static List<PlayerProfile> players = new List<PlayerProfile>();
        private const string PlayersFileName = "players.txt";
        private const string LeaderboardFileName = "leaderboard.txt";
        static void Main()
        {
            LoadPlayers();
            Console.Write("введите имя игрока: ");
            string playerName = Console.ReadLine().Trim();
            PlayerProfile currentPlayer = players.FirstOrDefault(p => p.PlayerName.Equals(playerName, StringComparison.OrdinalIgnoreCase));
            if (currentPlayer == null)
            {
                currentPlayer = new PlayerProfile(playerName, 1, 0);
                players.Add(currentPlayer);
                try
                {
                    using (StreamWriter writer = new StreamWriter(PlayersFileName))
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                            var player = players[i];
                            writer.WriteLine($"PlayerName: {player.PlayerName}");
                            writer.WriteLine($"MaxLevel: {player.MaxLevel}");
                            writer.WriteLine($"Score: {player.Score}");
                            if (i < players.Count - 1)
                            {
                                writer.WriteLine("---");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при сохранении файла");
                }
                Console.WriteLine($"Создан новый игрок. Имя: {currentPlayer.PlayerName}, Уровень: {currentPlayer.MaxLevel}, Счёт: {currentPlayer.Score}");
            }
            else
            {
                Console.WriteLine($"Добро пожаловать назад, {currentPlayer.PlayerName}! Ваш максимальный уровень: {currentPlayer.MaxLevel}, Счёт: {currentPlayer.Score}");
            }
            Console.Write("доступные уровни: ");
            for (int i = 1; i < currentPlayer.MaxLevel + 1; i++)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            binaric(currentPlayer, currentPlayer.MaxLevel);
            SavePlayers();
            bool hochu = true;
            while (hochu == true)
            {
                Console.WriteLine("хотите еще сыграть? 1 - да, 2 - нет");
                string h = Console.ReadLine();
                if (int.TryParse(h, out int huch))
                {
                    if (huch > 2)
                    {
                        Console.WriteLine("ошибка ввода");
                    }
                    if (huch < 1)
                    {
                        Console.WriteLine("ошибка ввода");
                    }
                    if (huch == 2)
                    {
                        hochu = false;
                        Console.WriteLine("иди нахуй");
                        Display();
                    }
                    if (huch == 1)
                    {
                        hochu = true;
                        binaric(currentPlayer, currentPlayer.MaxLevel);
                        SavePlayers();
                    }
                }
                else
                {
                    Console.WriteLine("ошибка ввода");
                }
            }
        }

        static void SavePlayers()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(PlayersFileName))
                {
                    for (int i = 0; i < players.Count; i++)
                    {
                        var player = players[i];
                        writer.WriteLine($"PlayerName: {player.PlayerName}");
                        writer.WriteLine($"MaxLevel: {player.MaxLevel}");
                        writer.WriteLine($"Score: {player.Score}");
                        if (i < players.Count - 1)
                        {
                            writer.WriteLine("---");
                        }
                    }
                }
            }
            catch { }
            ;
        }
        static void LoadPlayers()
        {
            players.Clear();
            if (!File.Exists(PlayersFileName))
            {
                Console.WriteLine($"Файл {PlayersFileName} не найден");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines(PlayersFileName);
                PlayerProfile currentPlayer = null;
                int lineIndex = 0;

                while (lineIndex < lines.Length)
                {
                    string line = lines[lineIndex].Trim();
                    if (string.IsNullOrEmpty(line) || line == "---")
                    {
                        lineIndex++;
                        continue;
                    }
                    if (line.StartsWith("PlayerName:"))
                    {
                        string playerName = line.Substring("PlayerName:".Length).Trim();
                        lineIndex++;
                        if (lineIndex < lines.Length && lines[lineIndex].Trim().StartsWith("MaxLevel:"))
                        {
                            string maxLevelStr = lines[lineIndex].Trim().Substring("MaxLevel:".Length).Trim();
                            lineIndex++;
                            if (lineIndex < lines.Length && lines[lineIndex].Trim().StartsWith("Score:"))
                            {
                                string scoreStr = lines[lineIndex].Trim().Substring("Score:".Length).Trim();
                                lineIndex++;

                                if (int.TryParse(maxLevelStr, out int maxLevel) &&
                                    int.TryParse(scoreStr, out int score))
                                {
                                    if (maxLevel >= 1 && maxLevel <= 5)
                                    {
                                        currentPlayer = new PlayerProfile(playerName, maxLevel, score);
                                        players.Add(currentPlayer);
                                    }
                                    else
                                    {
                                        Console.WriteLine("скипаем");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("скипаем");
                                }
                            }
                            else
                            {
                                Console.WriteLine("скипаем");
                                while (lineIndex < lines.Length && lines[lineIndex].Trim() != "---")
                                    lineIndex++;
                            }
                        }
                        else
                        {
                            Console.WriteLine("скипаем");
                            while (lineIndex < lines.Length && lines[lineIndex].Trim() != "---")
                                lineIndex++;
                        }
                    }
                    else
                    {
                        lineIndex++;
                    }
                }
                Console.WriteLine($"Загружено игроков: {players.Count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке файла");
                players.Clear();
            }
        }
        static void binaric(PlayerProfile currentPlayer, int maxlevel)
        {
            Console.Write("доступные уровни: ");
            for (int i = 1; i < currentPlayer.MaxLevel + 1; i++)
            {
                Console.Write($"{i} ");
            }
            Console.WriteLine(" ");

            string h = Console.ReadLine();
            if (int.TryParse(h, out int huch))
            {
                if (huch <= maxlevel)
                {
                    if (huch > 0)
                    {
                        Console.WriteLine("принял");

                        int a = 0;
                        if (huch == 1)
                        {
                            a = 10;
                        }
                        if (huch == 2)
                        {
                            a = 50;
                        }
                        if (huch == 3)
                        {
                            a = 100;
                        }
                        if (huch == 4)
                        {
                            a = 250;
                        }
                        if (huch == 5)
                        {
                            a = 1000;
                        }
                        Random binar = new Random();
                        int ch = binar.Next(1, a + 1);
                        Console.WriteLine($"загадано число от 1 до {a}");
                        bool verno = false;
                        while (verno == false)
                        {
                            Console.WriteLine("отвечай давай");
                            string otv = Console.ReadLine();
                            if (int.TryParse(otv, out int otvet))
                            {
                                if (otvet == ch)
                                {
                                    verno = true;
                                    Console.WriteLine("угадал");
                                }
                            }
                        }
                        int pointsEarned = huch * huch * 10;
                        currentPlayer.scoric(pointsEarned);
                        Console.WriteLine($"За уровень {huch} ты получил {pointsEarned} очков!");
                        if (maxlevel < 5)
                        {
                            if (huch == maxlevel)
                            {
                                Console.WriteLine($"{maxlevel + 1} уровень разблокирован. Всего очков - {currentPlayer.Score}");
                                currentPlayer.TryIncreaseMaxLevel();
                            }
                            else
                            {
                                Console.WriteLine($"вы можете пройти и другие уровни. Всего очков - {currentPlayer.Score} ");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("ошибка ввода");
                    }
                }
                else
                {
                    Console.WriteLine("ошибка ввода");
                }
            }
            else
            {
                Console.WriteLine("ошибка ввода");
            }
        }
        static void Display()
        {
            List<(string Name, int Level, int Score)> leaders = new List<(string, int, int)>();
            if (File.Exists(LeaderboardFileName))
            {
                try
                {
                    string[] lines = File.ReadAllLines(LeaderboardFileName);
                    string currentName = null;
                    int currentLevel = 0;
                    int currentScore = 0;
                    int lineIndex = 0;

                    while (lineIndex < lines.Length)
                    {
                        string line = lines[lineIndex].Trim();

                        if (string.IsNullOrEmpty(line) || line == "---")
                        {
                            if (currentName != null)
                            {
                                leaders.Add((currentName, currentLevel, currentScore));
                                currentName = null;
                            }
                            lineIndex++;
                            continue;
                        }

                        if (line.StartsWith("PlayerName:"))
                        {
                            currentName = line.Substring("PlayerName:".Length).Trim();
                            lineIndex++;
                        }
                        else if (line.StartsWith("Level:"))
                        {
                            if (int.TryParse(line.Substring("Level:".Length).Trim(), out int lvl))
                                currentLevel = lvl;
                            lineIndex++;
                        }
                        else if (line.StartsWith("Score:"))
                        {
                            if (int.TryParse(line.Substring("Score:".Length).Trim(), out int scr))
                                currentScore = scr;
                            lineIndex++;
                        }
                        else
                        {
                            lineIndex++;
                        }
                    }
                    if (currentName != null)
                    {
                        leaders.Add((currentName, currentLevel, currentScore));
                    }
                }
                catch
                {

                }
            }
            if (leaders.Count == 0)
            {
                leaders = players.Select(p => (p.PlayerName, p.MaxLevel, p.Score)).ToList();
            }
            var sortedLeaders = leaders.OrderByDescending(l => l.Score).ToList();
            Console.WriteLine("Таблица лидеров:");
            Console.WriteLine("_________________");
            int nameWidth = Math.Max(5, sortedLeaders.Max(l => l.Name.Length) + 1);
            int levelWidth = 8;
            int scoreWidth = 8;

            string format = $"| {{0,{-nameWidth}}} | {{1,{levelWidth}}} | {{2,{scoreWidth}}} |";
            Console.WriteLine(format, "Игрок", "Уровень", "Счёт");
            Console.WriteLine(new string('-', nameWidth + levelWidth + scoreWidth ));

            foreach (var leader in sortedLeaders)
            {
                Console.WriteLine(format, leader.Name, leader.Level, leader.Score);
            }
            Console.WriteLine("----------------");
            try
            {
                using (StreamWriter writer = new StreamWriter(LeaderboardFileName))
                {
                    for (int i = 0; i < sortedLeaders.Count; i++)
                    {
                        var leader = sortedLeaders[i];
                        writer.WriteLine($"PlayerName: {leader.Name}");
                        writer.WriteLine($"Level: {leader.Level}");
                        writer.WriteLine($"Score: {leader.Score}");
                    }
                }
            }
            catch
            {
            }
        }
    }
}

