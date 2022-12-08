using UnityEngine;

namespace _Code.Drag
{
    public class DragManager : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boardBounds;
            
        private DraggableObject _draggedObject = null;
        private Vector3 _mouseOffset;
        private int _orderInLayer = 20;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            HandleMouseButtonDown();

            HandleDrag();

            HandleMouseButtonUp();
        }

        private void HandleDrag()
        {
            if (_draggedObject != null)
            {
                var newTransform = _camera.ScreenToWorldPoint(Input.mousePosition) - _mouseOffset;
                _draggedObject.OnDrag(newTransform);
            }
        }

        private void HandleMouseButtonDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

                var objectOnTop = GetObjectOnTop(hits);
                if (objectOnTop != null)
                {
                    if (objectOnTop != null)
                    {
                        _draggedObject = objectOnTop;
                        _draggedObject.OnDragStart(_orderInLayer+=1);
                        _mouseOffset = _camera.ScreenToWorldPoint(Input.mousePosition) - _draggedObject.transform.position;
                    }
                }
            }
        }

        private static DraggableObject GetObjectOnTop(RaycastHit2D[] hits)
        {
            if (hits.Length != 0)
            {
                DraggableObject objOnTop = hits[0].collider.transform.parent.GetComponent<DraggableObject>();
                foreach (var hit in hits)
                {
                    var obj = hit.collider.gameObject;
                    var objDrag = obj.transform.parent.GetComponent<DraggableObject>();
                    if (objOnTop.SortingGroup.sortingOrder < objDrag.SortingGroup.sortingOrder)
                    {
                        objOnTop = objDrag;
                    }
                }

                return objOnTop;
            }
            return null;
        }

        private void HandleMouseButtonUp()
        {
            if (Input.GetMouseButtonUp(0) && _draggedObject != null)
            {
                _draggedObject.OnDragStop(boardBounds);
                _draggedObject = null;
            }
        }
    }
}