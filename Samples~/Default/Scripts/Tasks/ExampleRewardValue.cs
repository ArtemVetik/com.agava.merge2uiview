using Agava.Merge2.Tasks;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    [CreateAssetMenu(menuName = "Merge2UIView/Tasks/Samples/ExampleRewardValue", fileName = "ExampleRewardValue", order = 51)]
    public class ExampleRewardValue : RewardValueFactory
    {
        public override IRewardValue Create()
        {
            return new RewardValue();
        }

        private class RewardValue : IRewardValue
        {
            public int Reward(Task task)
            {
                return task.TotalItems.Sum(task => task.Level + 1);
            }
        }
    }
}
