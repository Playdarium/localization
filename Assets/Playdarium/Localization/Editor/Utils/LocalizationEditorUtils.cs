using System.Linq;
using Playdarium.Localization.Runtime.Models;
using UnityEditor;
using UnityEngine;

namespace Playdarium.Localization.Utils
{
	public static class LocalizationEditorUtils
	{
		private static LocalizationData _localizationData;

		public static LocalizationData GetLocalizationData()
		{
			if (_localizationData != null)
				return _localizationData;

			var findAssets = AssetDatabase.FindAssets($"t:{nameof(LocalizationData)}");
			var asset = findAssets.FirstOrDefault();
			if (string.IsNullOrEmpty(asset))
			{
				Debug.LogError($"[{nameof(LocalizationEditorUtils)}] Cannot find {nameof(LocalizationData)}");
				return null;
			}

			_localizationData = AssetDatabase.LoadAssetAtPath<LocalizationData>(AssetDatabase.GUIDToAssetPath(asset));
			return _localizationData;
		}
	}
}