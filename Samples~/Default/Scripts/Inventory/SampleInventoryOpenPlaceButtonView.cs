using TMPro;
using UnityEngine;

namespace Agava.Merge2UIView.Samples
{
    public class SampleInventoryOpenPlaceButtonView : InventoryOpenPlaceButtonView
    {
        [SerializeField] private TMP_Text _costText;
        
        public override void Render(int placeCost)
        {
            _costText.text = placeCost.ToString();
        }
    }
}