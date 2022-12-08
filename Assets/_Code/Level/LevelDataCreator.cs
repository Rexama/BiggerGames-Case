using System;
using System.IO;
using _Code.Common;
using _Code.Piece.ProceduralPieceCreation;
using UnityEngine;

namespace _Code.Level
{
    public class LevelDataCreator : MonoBehaviour
    {
        [SerializeField] private Difficulty difficulty;
        [SerializeField] private int levelNumber;
        [SerializeField] ProceduralPieceFactory proceduralPieceFactory;

        private string _fileName;
        private void Start()
        {
            _fileName = "level" + levelNumber + ".txt";
            CreateLevel();
        }

        private void CreateLevel()
        {
            LevelData level = CreateLevelData();
            string json = JsonUtility.ToJson(level);
            WriteToFile(json);
            Debug.Log("Level Created and file written in Resources folder");
        }

        private LevelData CreateLevelData()
        {
            switch (difficulty)
            {
                case Difficulty.Easy:
                    return new LevelData(difficulty, 4, 5,proceduralPieceFactory.CreatePieces(4,5));
                case Difficulty.Medium:
                    return new LevelData(difficulty, 5, 8,proceduralPieceFactory.CreatePieces(5,8));
                case Difficulty.Hard:
                    return new LevelData(difficulty, 6, 12,proceduralPieceFactory.CreatePieces(6,12));
                default:
                    throw new ArgumentOutOfRangeException(nameof(difficulty), difficulty, null);
            }
        }
        
        private void WriteToFile(string json)
        {
            string path = GetFilePath();
            FileStream fileStream = new FileStream(path, FileMode.Create);

            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }
            fileStream.Close();
        }
        
        private string GetFilePath()
        {
            return Application.dataPath + "/Resources/Levels/" + _fileName;
        }
    }
}