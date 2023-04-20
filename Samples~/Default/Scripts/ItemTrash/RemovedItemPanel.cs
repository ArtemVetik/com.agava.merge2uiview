using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class RemovedItemPanel : MonoBehaviour
    {
        [SerializeField] private Transform _toggleContainer;
        
        public void Setup(RemoveItemToggle removeItemToggle)
        {
            removeItemToggle.transform.SetParent(_toggleContainer);
        }
    }
}