using Agava.Merge2.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/BoardPresenter/ShapeFactory/ArbitaryShapeFactory", fileName = "ArbitaryShape", order = 56)]
    public class ArbitaryShapeFactory : ScriptableShapeFactory
    {
        [SerializeField] internal List<Vector2Int> Coordinates;

        private void OnValidate()
        {
            for (int i = 0; i < Coordinates.Count; i++)
            {
                var x = Coordinates[i].x;
                var y = Coordinates[i].y;

                if (x < 0 || y < 0)
                    Coordinates[i] = new Vector2Int(Mathf.Clamp(x, 0, int.MaxValue), Mathf.Clamp(y, 0, int.MaxValue));
            }
        }

        public override IShape Create()
        {
            return new ArbitaryShape(Coordinates.Select(coordinate => new MapCoordinate(coordinate.x, coordinate.y)));
        }
    }
}
