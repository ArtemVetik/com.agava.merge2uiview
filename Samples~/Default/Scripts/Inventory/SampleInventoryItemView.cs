using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class SampleInventoryItemView : InventoryItemView
    {
        [SerializeField] private Image _image;

        private Color _defaultColor;
        private Coroutine _flickingCoroutine;
        
        public override void Render(Sprite icon)
        {
            _image.sprite = icon;
            _defaultColor = _image.color;
        }

        public override void RenderFailedRemove()
        {
            if (_flickingCoroutine != null)
                StopCoroutine(_flickingCoroutine);
            
            _flickingCoroutine = StartCoroutine(Flicking());
        }

        private IEnumerator Flicking()
        {
            _image.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            _image.color = _defaultColor;
        }
    }
}