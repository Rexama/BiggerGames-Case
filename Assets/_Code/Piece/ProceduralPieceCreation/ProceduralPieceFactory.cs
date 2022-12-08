using System.Collections.Generic;
using _Code.ScriptableObjects;
using UnityEngine;

namespace _Code.Piece.ProceduralPieceCreation
{
    public class ProceduralPieceFactory:MonoBehaviour
    {
        [SerializeField] private PieceColors pieceColors;
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private ProceduralFragmentFactory proceduralFragmentFactory;
        
        private int _pieceCount;
        private List<ProceduralPiece> _pieces;
        private int _totalFragmentCount;
        public List<PieceData> CreatePieces(int boardSize,int pieceCount)
        {
            _pieceCount = pieceCount;
            var fragments = proceduralFragmentFactory.CreateFragments(boardSize);
            _totalFragmentCount = fragments.Count;
            _pieces = PreparePieces(fragments);
            StartVoronoiDiagram();
            return GetPieceDataList();
        }

        private List<ProceduralPiece> PreparePieces(List<ProceduralFragment> fragments)
        {
            List<ProceduralPiece> pieces = new List<ProceduralPiece>();
            for (int i = 0; i < _pieceCount; i++)
            {
                var piece = Instantiate(piecePrefab, transform).GetComponent<ProceduralPiece>();
                pieces.Add(piece);
                
                var randomFragment = fragments.GetRandom();
                piece.PreparePiece(pieceColors.colors[i], randomFragment,i);
                fragments.Remove(randomFragment);
            }
            return pieces;
        }
        
        private void StartVoronoiDiagram()
        {
            int capturedFragments = _pieces.Count;
            while(capturedFragments < _totalFragmentCount)
            {
                foreach (var piece in _pieces)
                {
                    capturedFragments += piece.CaptureNextFragments();
                }
            }

            foreach (var piece in _pieces)
            {
                piece.CreatePieceHints();
            }
        }

        private List<PieceData> GetPieceDataList()
        {
            List<PieceData> pieceDataList = new List<PieceData>();
            foreach (var piece in _pieces)
            {
                pieceDataList.Add(piece.GetPieceData());
            }
            return pieceDataList;
        }


    }
}