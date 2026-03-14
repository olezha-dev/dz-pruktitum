using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pruktikum_dz_1
{
    public class PlayerProfile
    {
        public string PlayerName { get; }
        public int MaxLevel { get; private set; } 
        public int Score { get; private set; }

        public PlayerProfile(string playerName, int maxLevel, int score)
        {
            maxLevel = 1;
            if (maxLevel < 1 || maxLevel > 5)
                throw new ArgumentOutOfRangeException(nameof(maxLevel), "Уровень должен быть от 1 до 5.");

            PlayerName = playerName;
            MaxLevel = maxLevel;
            Score = score;
        }
        public bool TryIncreaseMaxLevel()
        {
            if (MaxLevel < 5)
            {
                MaxLevel++;
                return true;
            }
            return false;
        }
        public void scoric(int point)
        {
            Score += point;
        }
    }
}
