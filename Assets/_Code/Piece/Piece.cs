using System.Collections.Generic;
using _Code.Drag;
using UnityEngine;

namespace _Code.Piece
{
    public class Piece : MonoBehaviour
    {
        private List<GameObject> _pieceHints;
        private List<Fragment> _fragments;
        public List<Fragment> Fragments => _fragments;
        private DraggableObject _draggableObject;
        public void PreparePiece(List<GameObject> hints, List<Fragment> fragments ,Vector3 pos,int sortingOrder)
        {
            _pieceHints = hints;
            _fragments = fragments;
            transform.position = pos;
            _draggableObject = GetComponent<DraggableObject>();
            _draggableObject.SetSortingGroupOrder(sortingOrder);
        }

        public void SetHintsActive(bool isActive)
        {
            foreach (var hintDot in _pieceHints)
            {
                hintDot.SetActive(isActive);
            }
        }
    }
}