using System.Collections.Generic;
using _Code.Piece.ProceduralPieceCreation;
using UnityEngine;

namespace _Code.Piece
{
    public class PieceFactory : MonoBehaviour
    {
        [Header("Piece Prefabs")]
        [SerializeField] private GameObject piecePrefab;
        [SerializeField] private GameObject fragmentPrefab;
        [SerializeField] private GameObject hintPrefab;
        
        [Space(10)]
        [Header("Piece Parent")]
        [SerializeField] private Transform piecesParent;
        
        private BoxCollider2D _pieceArea;
        private void Awake()
        {
            _pieceArea = GetComponent<BoxCollider2D>();
        }

        public List<Piece> CreatePieces(List<PieceData> pieceDataList)
        {
            var pieces = new List<Piece>();
            var sortingOrder = 0;
            foreach (var pieceData in pieceDataList)
            {
                var pieceObj = Instantiate(piecePrefab, pieceData.position, Quaternion.identity, piecesParent);
                var piece = pieceObj.GetComponent<Piece>();
                
                var pieceHints = GetPieceHintsList(pieceData,pieceObj);
                var fragments = GetFragmentsList(pieceData, pieceObj);
                var pos=_pieceArea.bounds.GetRandomPointInBounds();
                
                piece.PreparePiece(pieceHints, fragments,pos,sortingOrder++);
                
                pieces.Add(piece);
            }

            return pieces;
        }

        private List<GameObject> GetPieceHintsList(PieceData pieceData, GameObject pieceObj)
        {
            var pieceHints = new List<GameObject>();
            foreach (var hintPosition in pieceData.hintPositions)
            {
                var hintObj = Instantiate(hintPrefab, hintPosition, Quaternion.identity, pieceObj.transform);
                hintObj.SetActive(false);
                pieceHints.Add(hintObj);
            }

            return pieceHints;
        }

        private List<Fragment> GetFragmentsList(PieceData pieceData, GameObject pieceObj)
        {
            var fragments = new List<Fragment>();
            foreach (var fragmentTransformData in pieceData.fragmentTransform)
            {
                var fragmentObj = Instantiate(fragmentPrefab, fragmentTransformData.position, Quaternion.identity,
                    pieceObj.transform);
                fragmentObj.transform.rotation = fragmentTransformData.rotation;
                var fragment = fragmentObj.GetComponent<Fragment>();
                fragment.SetColor(pieceData.color);
                fragments.Add(fragment);
            }
            return fragments;
        }
    }
}