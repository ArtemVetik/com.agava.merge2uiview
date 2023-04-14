using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/CooldownRepository", fileName = "CooldownRepository", order = 51)]
    public class CooldownRepositoryFactory : ScriptableObject
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private ItemCooldownSetting[] _settings;

        public bool Initialized { get; private set; } = false;
        public CooldownRepository Repository { get; private set; }

        public void Initialize()
        {
            if (Initialized)
                throw new InvalidOperationException(nameof(CooldownCommandFactory) + " already initialized");

            var settings = new CooldownSettings(_settings.Select(setting =>
                new KeyValuePair<Item, Cooldown>(
                    new Item(setting.ItemID, setting.ItemLevel),
                    new Cooldown(setting.MaxClicks, setting.CooldownSeconds))
                )
            );

            Repository = new CooldownRepository(settings, new LocalTimeProvider(), new PlayerPrefsRepository(_saveKey));
            Repository.Load();
            Initialized = true;
        }

        private void OnDisable()
        {
            Initialized = false;
        }

        [Serializable]
        private class ItemCooldownSetting
        {
            [field: SerializeField, ItemId] public string ItemID { get; private set; }
            [field: SerializeField] public int ItemLevel { get; private set; }
            [field: SerializeField] public int MaxClicks { get; private set; }
            [field: SerializeField] public int CooldownSeconds { get; private set; }
        }

        private class LocalTimeProvider : ITimeProvider
        {
            public long NowTicks => DateTime.Now.Ticks;
        }
    }
}
