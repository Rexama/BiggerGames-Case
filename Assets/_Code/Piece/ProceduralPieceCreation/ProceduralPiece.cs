using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Code.Piece.ProceduralPieceCreation
{
    public class ProceduralPiece : MonoBehaviour
    {
        private Color _pieceColor;
        private ProceduralPieceHints _proceduralPieceHints;
        private SortingGroup _sortingGroup;
        
        private Dictionary<Vector3,List<ProceduralFragment>> _pieceFragments = new Dictionary<Vector3,List<ProceduralFragment>>();
        private List<ProceduralFragment> _borderFragments = new List<ProceduralFragment>();
        private ProceduralFragment _centerFragment;
        private void Awake()
        {
            _proceduralPieceHints = GetComponent<ProceduralPieceHints>();
            _sortingGroup = GetComponent<SortingGroup>();
        }
        
        public void PreparePiece(Color color,ProceduralFragment proceduralFragment,int orderInLayer)
        {
            SetColor(color);
            AddFragment(proceduralFragment);
            SetOrderInLayer(orderInLayer);
        }

        public int CaptureNextFragments()
        {
            var capturedFragments = 0;
            var newBorderFragments = new List<ProceduralFragment>();
            foreach(var fragment in _borderFragments)
            {
                foreach (var neighbourFragment in fragment.GetNeighbourFragments())
                {
                    if(neighbourFragment.TryCaptureFragment(_pieceColor))
                    {
                        neighbourFragment.SetParent(transform);
                        newBorderFragments.Add(neighbourFragment);
                        _pieceFragments.AddFragmentToDictionary(neighbourFragment);
                        capturedFragments++;
                    }
                }
            }
            
            _borderFragments = newBorderFragments;
            
            return capturedFragments;
        }
        
        private void SetColor(Color color)
        {
            _pieceColor = color;
        }
        private void SetOrderInLayer(int order)
        {
            _sortingGroup.sortingOrder = order;
        }
        
        private void AddFragment(ProceduralFragment proceduralFragment)
        {
            transform.position = proceduralFragment.transform.position;
            _centerFragment = proceduralFragment;
            _pieceFragments.AddFragmentToDictionary(proceduralFragment);
            proceduralFragment.TryCaptureFragment(_pieceColor);
            proceduralFragment.SetParent(transform);
            _borderFragments.Add(proceduralFragment);
        }
        
        public void CreatePieceHints()
        {
            _proceduralPieceHints.CreateHintDots(_pieceFragments);
        }

        public PieceData GetPieceData()
        {
            List<TransformData> transformDataList = new List<TransformData>();
            List<Vector3> hintPositions = new List<Vector3>();
            
            foreach (var fragmentList in _pieceFragments.Values)
            {
                foreach (var fragment in fragmentList)
                {
                    var transform1 = fragment.transform;
                    var fragmentTransformData = new TransformData(transform1.position,transform1.rotation,transform1.localScale);
                    transformDataList.Add(fragmentTransformData);
                }
            }

            foreach (var hintDot in _proceduralPieceHints.HintDots)
            {
                hintPositions.Add(hintDot.transform.position);
            }

            PieceData pieceData = new PieceData(_pieceColor,hintPositions,transformDataList,_centerFragment.transform.position);
            return pieceData;
        }
        
    }
}
