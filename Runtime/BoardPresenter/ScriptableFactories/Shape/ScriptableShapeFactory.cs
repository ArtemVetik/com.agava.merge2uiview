using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    public abstract class ScriptableShapeFactory : ScriptableObject
    {
        public abstract IShape Create();
    }
}
