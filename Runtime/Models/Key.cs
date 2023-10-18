using System;
using System.Collections.Generic;
using UnityEngine;

namespace Package.Localization.Runtime.Models
{
    [Serializable]
    public class Key
    {
        [SerializeField] private string name;

        [SerializeField] private Value[] values = Array.Empty<Value>();

        public string Name
        {
            get => name;
            set => name = value;
        }

        public Value[] Values
        {
            get => values;
            set => values = value;
        }

        public bool Exists(SystemLanguage languageCode) => GetValue(languageCode) != null;

        public string Get(SystemLanguage languageCode) => GetValue(languageCode)?.Text ?? string.Empty;

        public void Set(SystemLanguage languageCode, string text)
        {
            var item = GetValue(languageCode);
            if (item != null)
                item.Text = text;
            else
                values = new List<Value>(values) { new(languageCode, text) }.ToArray();
        }

        private Value GetValue(SystemLanguage languageCode)
        {
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];
                if (value.Language == languageCode)
                    return value;
            }

            return null;
        }
    }
}