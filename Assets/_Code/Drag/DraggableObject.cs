using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Code.Drag
{
    public class DraggableObject : MonoBehaviour
    {
        private Piece.Piece _piece;
        private SortingGroup _sortingGroup;
        public SortingGroup SortingGroup => _sortingGroup;
        private bool _isSnapped = false;

        public static event Action OnSnap;
        public static event Action OnUnSnap;

        private void Awake()
        {
            _sortingGroup = GetComponent<SortingGroup>();
            _piece = GetComponent<Piece.Piece>();
        }

        public void OnDragStart(int orderInLayer)
        {
            _piece.SetHintsActive(true);
            _sortingGroup.sortingOrder = orderInLayer;

            if (_isSnapped)
            {
                OnUnSnap?.Invoke();
                _isSnapped = false;
            }
        }

        public void OnDragStop(BoxCollider2D boardBounds)
        {
            _piece.SetHintsActive(false);
            if (IsObjectOnBoard(boardBounds))
            {
                SnapObject();
                if (!_isSnapped)
                {
                    OnSnap?.Invoke();
                    _isSnapped = true;
                }
            }
        }

        public void OnDrag(Vector3 position)
        {
            transform.position = position;
        }
        public void SetSortingGroupOrder(int order)
        {
            _sortingGroup.sortingOrder = order;
        }

        private bool IsObjectOnBoard(BoxCollider2D boardBounds)
        {
            var pieceFragments = _piece.Fragments;
            foreach (var fragment in pieceFragments)
            {
                if (!boardBounds.bounds.Contains(fragment.transform.position))
                {
                    return false;
                }
            }

            return true;
        }

        private void SnapObject()
        {
            var position = transform.position;
            var positionY = Mathf.Round(position.y * 2) * 0.5F;
            var positionX = Mathf.Round(position.x * 2) * 0.5F;
            if (positionY % 1 == 0)
            {
                if (position.y > positionY)
                {
                    positionY += 0.5f;
                }
                else
                {
                    positionY -= 0.5f;
                }
            }

            if (positionX % 1 == 0)
            {
                if (position.x > positionX)
                {
                    positionX += 0.5f;
                }
                else
                {
                    positionX -= 0.5f;
                }
            }

            transform.position = new Vector3(positionX, positionY, position.z);
        }
    }
}