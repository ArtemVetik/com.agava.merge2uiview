using UnityEngine;
using UnityEngine.UI;

namespace Agava.Merge2UIView.Samples
{
    public class ItemIcon : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Image _frame;

        public void ChangeSprite(Sprite sprite) => _image.sprite = sprite;

        public void EnableFrame() => _frame.enabled = true;
    }
}