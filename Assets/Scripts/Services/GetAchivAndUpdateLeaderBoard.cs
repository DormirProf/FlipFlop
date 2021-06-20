using UnityEngine;

namespace Scripts.Services
{
    public class GetAchivAndUpdateLeaderBoard
    {
        private const string _leaderBoard = "CgkI19K_ku8cEAIQBg";
        private const string _kingOfCoins = "CgkI19K_ku8cEAIQCw";
        private string[] _achivmentForLose = {"CgkI19K_ku8cEAIQCQ", "CgkI19K_ku8cEAIQCg"};

        private string[] _achivments =
        {
            "CgkI19K_ku8cEAIQAQ", "CgkI19K_ku8cEAIQAg", "CgkI19K_ku8cEAIQAw", "CgkI19K_ku8cEAIQBA", "CgkI19K_ku8cEAIQBQ"
        };

        private int[] _range = {10, 35, 100, 250, 500};

        public GetAchivAndUpdateLeaderBoard() => GetAchivmentForLose();

        public GetAchivAndUpdateLeaderBoard(int record)
        {
            LoadRecordInGPS(record);
            GetAchiveIsRecordInRange(record);
        }

        private void LoadRecordInGPS(int record)
        {
            Social.ReportScore(record, _leaderBoard, (bool success) =>
            {
                if (success) Debug.Log("The record has been successfully added to the leaderboard!");
            });
        }

        public static void LoadCoinsInGPS(int coins)
        {
            Social.ReportScore(coins, _kingOfCoins, (bool success) =>
            {
                if (success) Debug.Log("The record has been successfully added to the leaderboard!");
            });
        }

        private void GetAchivmentForLose()
        {
            for (int i = 0; i < _achivmentForLose.Length; i++)
            {
                SendAchivment(_achivmentForLose[i]);
            }
        }

        private void GetAchiveIsRecordInRange(int record)
        {
            for (int i = 0; i < _range.Length; i++)
            {
                if ((record >= _range[i]) && (record < _range[i + 1]))
                {
                    SendAchivment(_achivments[i]);
                }
            }
        }

        private void SendAchivment(string achivment)
        {
            Social.ReportProgress(achivment, 100.0f, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Achievement" + achivment + "completed!");
                }
            });
        }
    }
}
