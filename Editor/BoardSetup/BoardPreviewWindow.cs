using System.Collections.Generic;
using System.Linq;
using Agava.Merge2.Core;
using UnityEditor;
using UnityEngine;

namespace Agava.Merge2UIView.Editor
{
    internal class BoardPreviewWindow : EditorWindow
    {
        private readonly Color _openedCellColor = Color.white;
        private readonly Color _contourCellColor = Color.yellow;
        private readonly Color _closedCellColor = Color.grey;

        private static BoardSetupFactory _boardSetupFactory;
        private static IBoardPreviewEditCommand _editCommand;

        public static void ShowWindow(BoardSetupFactory boardSetupFactory, IBoardPreviewEditCommand editCommand)
        {
            GetWindow<BoardPreviewWindow>("BoardPreview");
            _boardSetupFactory = boardSetupFactory;
            _editCommand = editCommand;
        }

        private void OnGUI()
        {
            if (_boardSetupFactory == null)
            {
                EditorGUI.HelpBox(new Rect(new Vector2(0, 0), position.size), "Press the preview button again", MessageType.Warning);
                return;
            }

            var shape = _boardSetupFactory.Shape;
            float cellSize = CalculateCellSize(shape);
            
            List<MapCoordinate> openedPositions = _boardSetupFactory.OpenedPositions().ToList();
            List<MapCoordinate> contourPositions = _boardSetupFactory.ContourAlgorithm.Compute(openedPositions).ToList();

            List<(MapCoordinate, (string, int))> items = _boardSetupFactory.Items().ToList();

            for (int y = 0; y < shape.Height; y++)
            {
                for (int x = 0; x < shape.Width; x++)
                {
                    var currentPosition = new MapCoordinate(x, y);
                    
                    if (shape.Contains(currentPosition) == false)
                        continue;
                    
                    string cellInfo = currentPosition.ToString();

                    if (items.Any(item => item.Item1 == currentPosition))
                    {
                        var currentItem = items.Find(item => item.Item1 == currentPosition);
                        cellInfo += $"\n{currentItem.Item2.Item1}\nLvl: {currentItem.Item2.Item2}";
                    }

                    var cellRect = ToGrid(currentPosition, cellSize);
                    var cellColor = CellColor(currentPosition, openedPositions, contourPositions);
                    
                    if (CellButton(cellRect, cellColor, cellInfo))
                        _editCommand.Execute(currentPosition);
                }
            }

            Repaint();
        }

        private bool CellButton(Rect cellRect, Color cellColor, string cellInfo)
        {
            Color lastColor = GUI.color;
            GUI.color = cellColor;
            
            bool clicked = GUI.Button(cellRect, "");
            
            GUI.color = lastColor;
            
            float cellSize = cellRect.size.x;
            Handles.Label(cellRect.position + new Vector2(cellSize * 0.1f, cellSize * 0.1f), cellInfo, 
                 new GUIStyle { fontSize = (int)(cellSize * 0.3f * (1f - cellInfo.Count(c => c == '\n') * 0.1f))});

            return clicked;
        }

        private float CalculateCellSize(IShape shape)
        {
            float sizeY = position.height / shape.Height;
            float sizeX = position.width / shape.Width;

            return Mathf.Min(sizeX, sizeY);
        }

        private Color CellColor(MapCoordinate cellPosition, List<MapCoordinate> openedPositions, List<MapCoordinate> contourPositions)
        {
            Color cellColor = openedPositions.Contains(cellPosition) ? _openedCellColor : _closedCellColor;
            cellColor = contourPositions.Contains(cellPosition) ? _contourCellColor : cellColor;

            return cellColor;
        }

        private Rect ToGrid(MapCoordinate itemPosition, float cellSize) => 
            new(itemPosition.X * cellSize, position.size.y - itemPosition.Y * cellSize - cellSize, cellSize * 0.95f, cellSize * 0.95f);
    }
}
    