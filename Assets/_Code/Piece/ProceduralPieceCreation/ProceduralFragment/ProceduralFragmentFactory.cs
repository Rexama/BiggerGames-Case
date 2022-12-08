using System.Collections.Generic;
using UnityEngine;

namespace _Code.Piece
{
    public class ProceduralFragmentFactory : MonoBehaviour
    {
        [SerializeField] private GameObject fragmentPrefab;

        private List<ProceduralFragment> fragments = new List<ProceduralFragment>();
        private float _offset = 0;

        List<ProceduralFragment> _prevRight = new List<ProceduralFragment>();
        ProceduralFragment _prevUp = null;

        public List<ProceduralFragment> CreateFragments(int boardSize)
        {
            _offset = -0.5f * (boardSize - 1);
            while (_prevRight.Count < boardSize)
            {
                _prevRight.Add(null);
            }

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var rightFragment = CreateFragment(i, j, 0);
                    var upFragment = CreateFragment(i, j, 90);
                    var leftFragment = CreateFragment(i, j, 180);
                    var downFragment = CreateFragment(i, j, 270);

                    AddNeighboursToFragments(leftFragment, upFragment, rightFragment, downFragment);

                    AddPrevNeighboursToFragments(j, leftFragment, downFragment);

                    _prevRight[j] = rightFragment;
                    _prevUp = upFragment;
                }
            }

            return fragments;
        }

        private void AddPrevNeighboursToFragments(int j, ProceduralFragment leftProceduralFragment, ProceduralFragment downProceduralFragment)
        {
            if (_prevRight[j] != null)
            {
                _prevRight[j].AddNeighbourFragments(leftProceduralFragment);
                leftProceduralFragment.AddNeighbourFragments(_prevRight[j]);
            }

            if (_prevUp != null && j != 0)
            {
                _prevUp.AddNeighbourFragments(downProceduralFragment);
                downProceduralFragment.AddNeighbourFragments(_prevUp);
            }
        }

        private void AddNeighboursToFragments(ProceduralFragment leftProceduralFragment, ProceduralFragment upProceduralFragment, ProceduralFragment rightProceduralFragment,
            ProceduralFragment downProceduralFragment)
        {
            rightProceduralFragment.AddNeighbourFragments(upProceduralFragment, downProceduralFragment);
            upProceduralFragment.AddNeighbourFragments(leftProceduralFragment, rightProceduralFragment);
            leftProceduralFragment.AddNeighbourFragments(downProceduralFragment, upProceduralFragment);
            downProceduralFragment.AddNeighbourFragments(rightProceduralFragment, leftProceduralFragment);
        }

        private ProceduralFragment CreateFragment(int i, int j, int deg)
        {
            var fragment = Instantiate(fragmentPrefab, transform);
            fragment.transform.localPosition = new Vector3(_offset + i, _offset + j, 0);
            fragment.transform.rotation = Quaternion.Euler(0, 0, deg);

            var fragmentScript = fragment.GetComponent<ProceduralFragment>();
            fragments.Add(fragmentScript);
            return fragmentScript;
        }
    }
}