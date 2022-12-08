using System.Collections.Generic;
using UnityEngine;

namespace _Code.Board
{
    public class Board : MonoBehaviour
    {
        
    
        [Header("Board Components")]
        [SerializeField] private Border border;
        [SerializeField] private Transform gridsTransform;

        [Header("Board Prefabs")]
        [SerializeField] private GameObject gridPrefab;

        private List<Grid> grids = new List<Grid>();
        private int _boardSize;
    
        public void PrepareBoard(int size)
        {
            _boardSize = size;
            border.SetBorderSize(_boardSize);
            CreateBoard();
            ReSizeCamera();
        }
    
        private void CreateBoard()
        {
            var offset = -0.5f*(_boardSize-1);
            for (int i = 0; i < _boardSize; i++)
            {
                for (int j = 0; j < _boardSize; j++)
                {
                    GameObject grid = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity,gridsTransform);
                    grids.Add(grid.GetComponent<Grid>());
                    grid.transform.localPosition = new Vector3(i+offset, j+offset, 0);
                }
            }
        }
    
        private void ReSizeCamera()
        {
            if (Camera.main != null) Camera.main.orthographicSize = _boardSize + 1;
        }

        public int GetGridsContactCount()
        {
            int count = 0;
            foreach (var grid in grids)
            {
                var contactCount = grid.GetContactCount();
                if (contactCount > 4)
                {
                    count += 4;
                }
                else
                {
                    count += contactCount;
                }
            }
            return count;
        }
    }
}
