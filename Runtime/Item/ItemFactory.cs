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
        private readonly IDictionary<string, string[]> _icons;

        public ItemFactory(ItemPresenter template, Transform parent, IEnumerable<KeyValuePair<string, string[]>> icons)
        {
            _template = template;
            _parent = parent;
            _icons = new Dictionary<string, string[]>(icons);
        }

        public ItemPresenter Load(Item item)
        {
            var icon = Resources.Load<Sprite>(new ResourcesPath(_icons[item.Id][item.Level]).Value());
            var inst = Object.Instantiate(_template, _parent);
            inst.Init(item, icon);

            return inst;
        }
    }
}
