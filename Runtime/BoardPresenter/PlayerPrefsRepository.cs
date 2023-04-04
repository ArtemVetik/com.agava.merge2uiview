using Agava.Merge2.Core;
using UnityEngine;

namespace Agava.Merge2UIView
{
    internal class PlayerPrefsRepository : IJsonSaveRepository
    {
        private readonly string _saveKey;

        public PlayerPrefsRepository(string saveKey)
        {
            _saveKey = saveKey;
        }

        public bool HasSave => PlayerPrefs.HasKey(_saveKey);

        public void Save(string json)
        {
            PlayerPrefs.SetString(_saveKey, json);
            PlayerPrefs.Save();
        }

        public string Load()
        {
            return PlayerPrefs.GetString(_saveKey);
        }
    }
}