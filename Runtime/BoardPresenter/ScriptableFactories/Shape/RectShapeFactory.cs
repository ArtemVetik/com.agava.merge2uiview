using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/BoardPresenter/ShapeFactory/RectShapeFactory", fileName = "RectShape", order = 56)]
    public sealed class RectShapeFactory : ScriptableShapeFactory
    {
        [SerializeField, Min(1)] private int _width;
        [SerializeField, Min(1)] private int _height;
        
        public override IShape Create()
        {
            return new RectShape(_width, _height);
        }
    }
}