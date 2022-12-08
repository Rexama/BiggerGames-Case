using UnityEngine;

namespace _Code.Board
{
    public class Grid : MonoBehaviour
    {
        private int _contactCount = 0;

        public int GetContactCount()
        {
            return _contactCount;
        }
    
        private void OnTriggerEnter2D(Collider2D col)
        {
            _contactCount++;
        }
    
        private void OnTriggerExit2D(Collider2D col)
        {
            _contactCount--;
        }
    
    }
}