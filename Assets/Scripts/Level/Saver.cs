using System;
using System.IO;
using UnityEngine;

namespace TowerDefense
{
    [Serializable]
    public class Saver<T>
    {
        public static class FileHandler
        {
            public static string Path(string filename)
            {
                return $"{Application.persistentDataPath}/{filename}";
            }
            public static void Reset(string filename)
            {
                string path = Path(filename);
                if (File.Exists(path)) File.Delete(path);
            }

            public static bool CheckFile(string filename)
            {
                string path = Path(filename);
                return File.Exists(path);
            }
        }
        public static void TryLoad(string filename, ref T data)
        {
            string path = FileHandler.Path(filename); 
            if (File.Exists(path))
            {
                Debug.Log($"Loading from {path}");

                string dataString = File.ReadAllText(path);
                Saver<T> saver = JsonUtility.FromJson<Saver<T>>(dataString);
                data = saver.data;

                Debug.Log("Loaded");
            }
            else Debug.LogError($"No file at {path}");
        }

        public static void Save(string filename, T data)
        {
            Debug.Log($"Saving to: {FileHandler.Path(filename)}");

            Saver<T> wrapper = new Saver<T> { data = data};
            string dataString = JsonUtility.ToJson(wrapper);

            File.WriteAllText(FileHandler.Path(filename), dataString);

            Debug.Log("Saving completed");
        }
        public T data;
    }
}