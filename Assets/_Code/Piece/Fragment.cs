using UnityEngine;

namespace _Code.Piece
{
    public class Fragment : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }
    }
}