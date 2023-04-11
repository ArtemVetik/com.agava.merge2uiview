using Agava.Merge2.Tasks;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class RewardValueFactory : ScriptableObject
    {
        public abstract IRewardValue Create();
    }
}
