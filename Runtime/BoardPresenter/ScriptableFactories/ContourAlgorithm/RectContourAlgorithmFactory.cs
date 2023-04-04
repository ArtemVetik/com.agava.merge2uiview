using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/BoardPresenter/ContourAlgorithmFactory/RectContourAlgorithmFactory", fileName = "RectContourAlgorithmFactory", order = 56)]
    public sealed class RectContourAlgorithmFactory : ScriptableContourAlgorithmFactory
    {
        public override IContourAlgorithm Create()
        {
            return new RectContourAlgorithm();
        }
    }
}