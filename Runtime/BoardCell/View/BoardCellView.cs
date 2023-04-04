using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView
{
    internal class BoardCellView : CellView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _closeIcons;
        [SerializeField] private Sprite[] _contourIcons;

        private CellRenderType? _renderType;

        internal CellRenderType RenderType => _renderType.Value;

        protected internal override void Render(CellRenderType renderType)
        {
            if (_renderType != null && _renderType == renderType)
                return;

            _renderType = renderType;

            if (RenderType == CellRenderType.Closed)
                ApplyRandomTexture(_closeIcons);
            else if (RenderType == CellRenderType.Contour)
                ApplyRandomTexture(_contourIcons);
            else
                _image.enabled = false;
        }

        private void ApplyRandomTexture(Sprite[] sprites)
        {
            int index = Random.Range(0, sprites.Length);
            Sprite sprite = sprites[index];

            _image.sprite = sprite;
            _image.enabled = true;
        }
    }
}
