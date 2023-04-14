using Agava.Merge2.Core;
using System.Collections.Generic;
using UnityEngine;
using YellowSquad.AssetPath;

namespace Agava.Merge2UIView
{
    internal class ItemFactory
    {
        private readonly ItemPresenter _template;
        private readonly Transform _parent;
        private readonly IDictionary<string, ResourcesReference<Sprite>[]> _icons;

        public ItemFactory(ItemPresenter template, Transform parent, IEnumerable<KeyValuePair<string, ResourcesReference<Sprite>[]>> icons)
        {
            _template = template;
            _parent = parent;
            _icons = new Dictionary<string, ResourcesReference<Sprite>[]>(icons);
        }

        public ItemPresenter Load(Item item)
        {
            var icon = _icons[item.Id][item.Level].Load();
            var inst = Object.Instantiate(_template, _parent);
            inst.Init(item, icon);

            return inst;
        }
    }
}
