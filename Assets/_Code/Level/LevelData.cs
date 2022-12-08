using System;
using System.Collections.Generic;
using _Code.Common;
using _Code.Piece.ProceduralPieceCreation;

namespace _Code.Level
{
    [Serializable]
    public class LevelData
    {
        public Difficulty difficulty;
        public int boardSize;
        public int piecesCount;
        public List<PieceData> pieceDataList;
        
        public LevelData (){}
        
        public LevelData(Difficulty difficulty, int boardSize, int piecesCount, List<PieceData> pieceDataList)
        {
            this.difficulty = difficulty;
            this.boardSize = boardSize;
            this.piecesCount = piecesCount;
            this.pieceDataList = pieceDataList;
        }
    }
}