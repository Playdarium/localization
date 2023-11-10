using System;
using System.Collections.Generic;
using System.Linq;
using Playdarium.Localization.Runtime.Models;
using Playdarium.Serializer;
using Playdarium.SpreadsheetDownloader.Helpers;
using UnityEditor;
using UnityEngine;

namespace Playdarium.Localization
{
	[CustomEditor(typeof(LocalizationData))]
	public class LocalizationDataEditor : MultipleSpreadsheetDownloader
	{
		private const string TRANSLATION = "Translation";

		protected override string[] SpreadsheetNames => new[]
		{
			TRANSLATION,
		};

		public override void OnInspectorGUI()
		{
			GUILayout.Space(5);
			if (GUILayout.Button("PrintCharSet"))
				PrintCharArray();

			base.OnInspectorGUI();
		}

		protected override void Serialize(Dictionary<string, string> jsonByTables)
		{
			var keys = SerializeTranslations(jsonByTables[TRANSLATION]);

			foreach (var group in keys.GroupBy(s => s.Name))
			{
				if (group.Count() > 1)
					Debug.LogError($"[{nameof(LocalizationDataEditor)}] Has key duplicate '{group.Key}'");
			}

			var array = serializedObject.FindProperty("keys");
			SerializedPropertySerializer.SerializeValueProperty(keys, array);
		}

		private Key[] SerializeTranslations(string json)
		{
			var localizationData = (LocalizationData)target;
			return Read<KeyJson>(json)
				.Where(f => !f.IsEmpty())
				.Select(keyJson => keyJson.ToKey(localizationData.LanguagesMap))
				.ToArray();
		}

		private void PrintCharArray()
		{
			var localizationData = (LocalizationData)target;

			var charsArray = localizationData.Keys
				.SelectMany(s => s.Values)
				.SelectMany(s => s.Text.ToCharArray())
				.SelectMany(s => new[] { char.ToUpper(s), char.ToLower(s) })
				.Concat("!\"#$%&'()*+,-./0123456789:>=<;?@[\\]^_~{|}")
				.Distinct()
				.ToArray();

			Debug.Log($"[{nameof(LocalizationDataEditor)}] {new string(charsArray)}");
		}

		[Serializable]
		public class KeyJson
		{
			[SerializeField] private string key;
			[SerializeField] private string[] languages;

			public bool IsEmpty() => string.IsNullOrEmpty(key);

			public Key ToKey(IEnumerable<SystemLanguage> languagesMap) => new()
			{
				Name = key,
				Values = languagesMap.Select((language, i) => new Value(language, languages[i])).ToArray()
			};
		}
	}
}