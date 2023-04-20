using Agava.Merge2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.Merge2UIView
{
    [CreateAssetMenu(menuName = "Merge2/OpeningDelayRepository", fileName = "OpeningDelayRepository", order = 51)]
    public class OpeningDelayRepositoryFactory : ScriptableObject
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private ItemOpeningDelaySetting[] _settings;
        [Header("If null - local time")]
        [SerializeField] private UnityEngine.Object _timeProvider;

        public bool Initialized { get; private set; } = false;
        public OpeningDelayRepository Repository { get; private set; }

        private void OnValidate()
        {
            if (_timeProvider is not ITimeProvider)
                _timeProvider = null;
        }

        public void Initialize()
        {
            if (Initialized)
                throw new InvalidOperationException(nameof(CooldownCommandFactory) + " already initialized");

            var settings = new OpeningDelaySettings(_settings.Select(setting =>
                new KeyValuePair<Item, int>( new Item(setting.ItemID, setting.ItemLevel), setting.DelaySeconds))
            );

            var timeProvider = _timeProvider ? _timeProvider as ITimeProvider : new LocalTimeProvider();

            Repository = new OpeningDelayRepository(settings, timeProvider, new PlayerPrefsRepository(_saveKey));
            Repository.Load();
            Initialized = true;
        }

        private void OnDisable()
        {
            Initialized = false;
        }

        [Serializable]
        private class ItemOpeningDelaySetting
        {
            [field: SerializeField, ItemId] public string ItemID { get; private set; }
            [field: SerializeField] public int ItemLevel { get; private set; }
            [field: SerializeField] public int DelaySeconds { get; private set; }
        }

        private class LocalTimeProvider : ITimeProvider
        {
            public long NowTicks => DateTime.Now.Ticks;
        }
    }
}
