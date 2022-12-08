using System.Collections.Generic;
using UnityEngine;

namespace _Code.Piece
{
    public class ProceduralFragment : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private bool _isCaptured = false;
        private List<ProceduralFragment> _neighbourFragments = new List<ProceduralFragment>();
        
        public void AddNeighbourFragments(ProceduralFragment frag1,ProceduralFragment frag2 = null)
        {
            _neighbourFragments.Add(frag1);
            
            if(frag2!=null)
                _neighbourFragments.Add(frag2);
        }
        
        public bool TryCaptureFragment(Color color)
        {
            if(_isCaptured) return false;
            
            _isCaptured = true;
            spriteRenderer.color = color;
            return true;
        }
        
        public List<ProceduralFragment> GetNeighbourFragments()
        {
            return _neighbourFragments;
        }
        
        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }

    }
}