using Agava.Merge2.Tasks;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class ExampleRewardCurrency : MonoBehaviour, IRewardCurrency
    {
        public void Add(int value)
        {
            Debug.Log("Added " + value + " test currency");
        }
    }
}
