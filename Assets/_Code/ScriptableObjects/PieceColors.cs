using System.Collections.Generic;
using UnityEngine;

namespace _Code.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Piece Colors", menuName = "Piece Colors", order = 0)]
    public class PieceColors : ScriptableObject
    {
        public List<Color> colors;
    }
}