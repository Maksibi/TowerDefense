using System;
using UnityEngine;

namespace TowerDefense
{
    public class MapsCompletion : MonoSingleton<MapsCompletion>
    {
        public const string FILENAME = "Completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode episode;
            public int score;
        }
        [SerializeField] private EpisodeScore[] completionData;
        [SerializeField] private EpisodeScore[] branchCompletionData;


        private int totalScore;
        public int TotalScore => totalScore; 

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(FILENAME, ref completionData);
            foreach (EpisodeScore episodeScore in completionData)
            {
                totalScore += episodeScore.score; 
            }
        }
        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance) Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
        }
        public static void ResetSaveData()
        {
            Saver<EpisodeScore[]>.FileHandler.Reset(FILENAME);
        }
        private void SaveResult(Episode episode, int result)
        {
            foreach (EpisodeScore item in completionData)
            {
                if (item.episode == episode && item.score < result)
                {
                    item.score = result;
                    Saver<EpisodeScore[]>.Save(FILENAME, completionData);
                }
            }
        }
        public int GetEpisodeScore(Episode episode)
        {
            foreach (EpisodeScore data in completionData)
            {
                if (data.episode == episode) return data.score;
            }
            return 0;
        }
    }
}