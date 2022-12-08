using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Code.Piece.ProceduralPieceCreation
{
    [Serializable]
    public class PieceData
    {
        public Color color;
        public List<Vector3> hintPositions;
        public List<TransformData> fragmentTransform;
        public Vector3 position;
        
        public PieceData(Color color, List<Vector3> hintPositions, List<TransformData> fragmentTransform, Vector3 position)
        {
            this.color = color;
            this.hintPositions = hintPositions;
            this.fragmentTransform = fragmentTransform;
            this.position = position;
        }
        

    }
    [Serializable]
    public class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        
        public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
    }
}