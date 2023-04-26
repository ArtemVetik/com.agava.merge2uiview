using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class SamplePaySource : MonoBehaviour, IPaySource
    {
        public bool Has(int value) => true;
        public void Pay(int value) { }
    }
}