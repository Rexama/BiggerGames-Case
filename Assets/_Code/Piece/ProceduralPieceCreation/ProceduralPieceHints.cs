using System.Collections.Generic;
using UnityEngine;

namespace _Code.Piece.ProceduralPieceCreation
{
    public class ProceduralPieceHints : MonoBehaviour
    {
        [SerializeField] private GameObject hintDotPrefab;
        
        private List<GameObject> _hintDots = new List<GameObject>();
        public List<GameObject> HintDots => _hintDots;
        

        public void CreateHintDots(Dictionary<Vector3,List<ProceduralFragment>> pieceFragments)
        {
            foreach (var fragmentPosition in pieceFragments.Keys)
            {
                if(pieceFragments[fragmentPosition].Count < 4)
                {
                    var hintDot = Instantiate(hintDotPrefab, fragmentPosition, Quaternion.identity, transform);
                    hintDot.SetActive(false);
                    _hintDots.Add(hintDot);
                }
            }
        }

    }
}