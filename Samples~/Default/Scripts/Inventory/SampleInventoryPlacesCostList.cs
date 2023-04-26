using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class SampleInventoryPlacesCostList : MonoBehaviour, IInventoryPlacesCostList
    {
        [SerializeField, Min(0)] private int _startCost;
        
        public int CostBy(int placeIndex) => _startCost + placeIndex;
    }
}