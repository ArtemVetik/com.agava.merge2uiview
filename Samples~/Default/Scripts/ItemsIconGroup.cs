using TMPro;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class ItemsIconGroup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private ItemIcon _iconTemplate;
        [SerializeField] private Transform _iconContainer;

        public void Render(string label, Sprite[] itemsIcon, int selectedIcon = -1)
        {
            _label.text = label;

            for (int i = 0; i < itemsIcon.Length; i++)
            {
                var icon = itemsIcon[i];
                var spawnedImage = Instantiate(_iconTemplate, _iconContainer);
                spawnedImage.ChangeSprite(icon);;

                if (selectedIcon == i)
                    spawnedImage.EnableFrame();
            }
        }
    }
}