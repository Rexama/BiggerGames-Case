using System.IO;
using UnityEngine;

namespace _Code.Level
{
    public class LevelDataLoader : MonoBehaviour
    {
        public LevelData LoadLevel(string fileName)
        {
            var level = new LevelData();
            string json = ReadFromFIle(fileName);
            JsonUtility.FromJsonOverwrite(json, level);
            Debug.Log("Loaded file: " + fileName);
            return level;
        }

        private string ReadFromFIle(string fileName)
        {
            string path = GetFilePath(fileName);
            if (File.Exists(path))
            {
                using StreamReader reader = new StreamReader(path);
                string json = reader.ReadToEnd();
                return json;
            }
            else
            {
                Debug.LogWarning("File not found");
            }

            return "Success";
        }

        private string GetFilePath(string fileName)
        {
            return Application.dataPath + "/Resources/Levels/" + fileName;
        }
    }
}