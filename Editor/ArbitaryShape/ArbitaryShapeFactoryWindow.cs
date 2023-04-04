using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    public class ArbitaryShapeFactoryWindow : EditorWindow
    {
        private static ArbitaryShapeFactory _shape;
        private static int _width;
        private static int _height;
        private static float _scaling = 50f;
        private static Vector2 _scrollPos;

        public static void ShowWindow(ArbitaryShapeFactory arbitaryShapeFactory)
        {
            GetWindow<ArbitaryShapeFactoryWindow>("Arbitary Shape Editor");
            _shape = arbitaryShapeFactory;
            _width = 0;
            _height = 0;
        }

        private void OnGUI()
        {
            if (_shape == null)
            {
                EditorGUI.HelpBox(new Rect(new Vector2(0, 0), position.size), "Press the editor button again", MessageType.Warning);
                return;
            }

            ComputMaxWidthHeight(out int maxBoardWidth, out int maxBoardHeight);


            _width = EditorGUILayout.IntField("Width", Mathf.Max(_width, maxBoardWidth));
            _height = EditorGUILayout.IntField("Height", Mathf.Max(_height, maxBoardHeight));
            _scaling = EditorGUILayout.FloatField("Scaling", Mathf.Clamp(_scaling, 0, int.MaxValue));

            var options = new List<GUIContent>();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_shape.Coordinates.Contains(new Vector2Int(x, y)))
                        options.Add(new GUIContent($"{x};{y}"));
                    else
                        options.Add(new GUIContent());
                }
            }

            var lastRect = GUILayoutUtility.GetLastRect();
            var startY = lastRect.yMax + lastRect.height / 2f;
            var viewRect = new Rect(lastRect.x, startY, lastRect.width, position.height - startY);
            var gridRect = new Rect(lastRect.x, startY, _width * _scaling, _height * _scaling);
            
            _scrollPos = GUI.BeginScrollView(viewRect, _scrollPos, gridRect);
            var selected = GUI.SelectionGrid(gridRect, -1, options.ToArray(), _width);
            GUI.EndScrollView();

            if (selected < 0)
                return;

            var coordinate = new Vector2Int(selected % _width, selected / _width);

            if (_shape.Coordinates.Contains(coordinate))
                _shape.Coordinates.Remove(coordinate);
            else
                _shape.Coordinates.Add(coordinate);

            if (GUI.changed)
                EditorUtility.SetDirty(_shape);
        }

        private void ComputMaxWidthHeight(out int width, out int height)
        {
            width = -1;
            height = -1;

            foreach (var coordinate in _shape.Coordinates)
            {
                if (coordinate.x > width)
                    width = coordinate.x;
                if (coordinate.y > height)
                    height = coordinate.y;
            }

            width++;
            height++;
        }
    }
}
