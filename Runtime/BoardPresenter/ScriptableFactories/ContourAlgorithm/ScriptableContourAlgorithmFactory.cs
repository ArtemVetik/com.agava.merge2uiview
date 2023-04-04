using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ScriptableContourAlgorithmFactory : ScriptableObject
    {
        public abstract IContourAlgorithm Create();
    }
}