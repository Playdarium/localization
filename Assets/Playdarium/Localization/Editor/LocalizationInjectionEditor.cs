using System.Linq;
using Playdarium.Localization.Runtime.Models;
using Playdarium.Serializer.Runtime.Fluent;
using Playdarium.Serializer.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Playdarium.Localization.Runtime.Zenject.Impls
{
	[CustomEditor(typeof(LocalizationInjection))]
	public class LocalizationInjectionEditor : Editor
	{
		private LocalizationData _localizationData;
		private string[] _languages;
		private int _selectedLanguage;


		private void OnEnable()
		{
			var findAssets = AssetDatabase.FindAssets($"t:{nameof(LocalizationData)}");
			var asset = findAssets.FirstOrDefault();
			if (string.IsNullOrEmpty(asset))
				return;

			_localizationData = AssetDatabase.LoadAssetAtPath<LocalizationData>(AssetDatabase.GUIDToAssetPath(asset));
			_languages = _localizationData.Languages.Select(s => s.ToString()).ToArray();
		}

		public override void OnInspectorGUI()
		{
			DrawHeader();
			base.OnInspectorGUI();
		}

		private void DrawHeader()
		{
			if (_localizationData == null)
			{
				EditorGUILayout.HelpBox($"Cannot find {nameof(LocalizationData)}", MessageType.Info);
				return;
			}

			_selectedLanguage = EditorGUILayout.Popup(_selectedLanguage, _languages);

			if (GUILayout.Button("Test Translate"))
			{
				var staticObjectsSp = serializedObject.FindProperty("staticObjects");
				var size = staticObjectsSp.arraySize;
				for (var i = 0; i < size; i++)
				{
					var elementAtIndex = staticObjectsSp.GetArrayElementAtIndex(i);
					ApplyLanguages(elementAtIndex);
				}
			}
		}

		private void ApplyLanguages(SerializedProperty element)
		{
			var componentSp = element.FindPropertyRelative("component");
			var keySp = element.FindPropertyRelative("key");

			var referenceValue = componentSp.objectReferenceValue;
			if (referenceValue == null)
				return;

			var key = _localizationData.Find(keySp.stringValue);
			if (key == null)
				return;

			var value = key.Get(_localizationData.Languages[_selectedLanguage]);
			if (referenceValue is TMP_Text tmpText)
			{
				tmpText.Modify().Set("m_text", value).Apply();
				tmpText.text = value;
				tmpText.SetVerticesDirty();
				tmpText.SetLayoutDirty();
			}
			else if (referenceValue is Text text)
			{
				text.Modify().Set("m_Text", value).Apply();
				text.text = value;
				text.SetVerticesDirty();
				text.SetLayoutDirty();
			}
		}
	}
}