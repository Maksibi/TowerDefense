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

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(FILENAME, ref completionData);
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
        public bool TryIndex(int id, out Episode episode, out int score)
        {
            if (id >= 0 & id < completionData.Length)
            {
                episode = completionData[id].episode;
                score = completionData[id].score;
                return true;
            }
            episode = null;
            score = 0;
            return false;
        }
    }
}