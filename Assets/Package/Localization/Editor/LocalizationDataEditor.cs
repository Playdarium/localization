using System;
using System.Collections.Generic;
using System.Linq;
using Package.Localization.Runtime.Models;
using Package.Serializer;
using Package.SpreadsheetDownloader.Helpers;
using UnityEditor;
using UnityEngine;

namespace Package.Localization
{
	[CustomEditor(typeof(LocalizationData))]
	public class LocalizationDataEditor : MultipleSpreadsheetDownloader
	{
		private const string TRANSLATION = "Translation";

		public override void OnInspectorGUI()
		{
			GUILayout.Space(5);
			if (GUILayout.Button("PrintCharSet"))
			{
				var localizationData = (LocalizationData) target;

				var charsArray = localizationData.Keys
					.Select(k => k.Values)
					.Aggregate((values, values1) => values.Concat(values1).ToArray())
					.Select(v => v.Text.ToCharArray())
					.Aggregate((chars, chars1) => chars.Concat(chars1).ToArray())
					.Select(c => new[] {char.ToUpper(c), char.ToLower(c)})
					.Aggregate((chars, chars1) => chars.Concat(chars1).ToArray())
					.Concat("!\"#$%&'()*+,-./0123456789:;<=>?@[\\]^_{|}~")
					.Distinct()
					.ToArray();

				Debug.Log($"[{nameof(LocalizationDataEditor)}] {new string(charsArray)}");
			}

			base.OnInspectorGUI();
		}

		protected override string[] SpreadsheetNames => new[]
		{
			TRANSLATION,
		};

		protected override void Serialize(Dictionary<string, string> jsonByTables)
		{
			var keys = SerializeTranslations(jsonByTables[TRANSLATION]);

			foreach (var key in keys)
			{
				keys.Single(s => s.Name == key.Name);
			}

			var array = serializedObject.FindProperty("keys");
			SerializedPropertySerializer.SerializeArrayProperty(keys, array);
		}

		private Key[] SerializeTranslations(string json)
		{
			var translations = Read<KeyJson>(json);
			translations.RemoveAll(f => f.IsEmpty());
			var languagesMapSp = serializedObject.FindProperty("languagesMap");
			var languagesMap = new SystemLanguage[languagesMapSp.arraySize];
			var languages = (SystemLanguage[]) Enum.GetValues(typeof(SystemLanguage));
			for (var i = 0; i < languagesMap.Length; i++)
			{
				var arrayElement = languagesMapSp.GetArrayElementAtIndex(i);
				languagesMap[i] = languages[arrayElement.enumValueIndex];
			}

			return translations.Select(keyJson => keyJson.ToKey(languagesMap)).ToArray();
		}

		[Serializable]
		public class KeyJson
		{
			[SerializeField] private string key;
			[SerializeField] private string[] languages;

			public bool IsEmpty() => string.IsNullOrEmpty(key);

			public Key ToKey(SystemLanguage[] languagesMap) => new()
			{
				Name = key,
				Values = languagesMap.Select((language, i) => new Value(language, languages[i])).ToArray()
			};
		}
	}
}