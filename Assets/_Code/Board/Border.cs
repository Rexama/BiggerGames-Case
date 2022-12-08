using UnityEngine;

namespace _Code.Board
{
    public class Border : MonoBehaviour
    {
        [SerializeField] private float borderThickness = 0.1f;

        public void SetBorderSize(int boardSize)
        {
            transform.localScale = new Vector3(boardSize + borderThickness, boardSize + borderThickness, 1);
        }
    }
}