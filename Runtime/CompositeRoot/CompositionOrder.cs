using System.Collections.Generic;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class CompositionOrder : MonoBehaviour
    {
        [SerializeField] private BoardMergeRoot _boardMergeRoot;
        [SerializeField] private List<CompositeRoot> _order;

        private void Awake()
        {
            _boardMergeRoot.Compose();
            
            foreach (var compositionRoot in _order)
            {
                compositionRoot.Compose(_boardMergeRoot);
                compositionRoot.enabled = true;
            }
        }
    }
}