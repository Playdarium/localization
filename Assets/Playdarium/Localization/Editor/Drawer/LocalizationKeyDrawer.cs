using Playdarium.Localization.Runtime.Zenject;
using Playdarium.Localization.Utils;
using UnityEditor;
using UnityEngine;

namespace Playdarium.Localization.Drawer
{
	[CustomPropertyDrawer(typeof(LocalizationKeyAttribute))]
	public class LocalizationKeyDrawer : PropertyDrawer
	{
		private static readonly Color ErrorColor = new Color(0.6f, 0f, 0f, 1f);
		
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			var localizationData = LocalizationEditorUtils.GetLocalizationData();
			if (localizationData != null)
			{
				var key = localizationData.Find(property.stringValue);
				if (key == null)
					DrawErrorColor(position, property);
			}

			EditorGUI.PropertyField(position, property, label);
		}

		private static void DrawErrorColor(Rect position, SerializedProperty property)
		{
			var previousColor = GUI.color;
			GUI.color = ErrorColor;

			var offsetPos = EditorGUI.IndentedRect(position);
			offsetPos.height = EditorGUI.GetPropertyHeight(property, GUIContent.none, true);
			EditorGUI.DrawRect(offsetPos, ErrorColor);

			GUI.color = previousColor;
		}
	}
}