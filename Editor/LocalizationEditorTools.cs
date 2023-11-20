using System;
using System.Collections;
using System.IO;
using Playdarium.Localization.Runtime.Models;
using Playdarium.Localization.Runtime.Zenject.Impls;
using Playdarium.Localization.Utils;
using Playdarium.SpreadsheetDownloader.Helpers;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

namespace Playdarium.Localization
{
	public static class LocalizationEditorTools
	{
		[MenuItem("Tools/Localization/Download")]
		public static void Download() => DownloadInternal();

		[MenuItem("Tools/Localization/Check Has Null Static")]
		public static void CheckNullStatic()
		{
			ProcessAllPrefabsWithLocalizationInjection(Process);
			return;

			void Process(PrefabInfo prefabInfo, GameObject gameObject)
			{
				var localizationInjection = gameObject.GetComponent<LocalizationInjection>();
				if (!localizationInjection.EditorHasNullReferences())
					return;

				Debug.LogError($"[{nameof(LocalizationEditorTools)}] Has null references: {prefabInfo.AssetPath}",
					prefabInfo.Prefab);
			}
		}

		[MenuItem("Tools/Localization/Remove All Null Static")]
		public static void ClearNullStatic()
		{
			ProcessAllPrefabsWithLocalizationInjection(Process);
			return;

			void Process(PrefabInfo prefabInfo, GameObject gameObject)
			{
				var localizationInjection = gameObject.GetComponent<LocalizationInjection>();
				var removed = localizationInjection.EditorClearNullReferences();
				if (removed == 0)
					return;

				Debug.LogError($"[{nameof(LocalizationEditorTools)}] Remove {removed} references from: {prefabInfo.AssetPath}",
					prefabInfo.Prefab);
			}
		}

		private static void DownloadInternal()
		{
			var localizationData = LocalizationEditorUtils.GetLocalizationData();
			if (localizationData == null)
				return;

			var editor = Editor.CreateEditor(localizationData);
			if (editor is not ISpreadsheetLoader spreadsheetLoader)
			{
				Debug.LogError(
					$"[{nameof(LocalizationEditorTools)}] For load localization data {nameof(LocalizationData)} must have Editor script inherited from {nameof(ISpreadsheetLoader)}");
				return;
			}

			spreadsheetLoader.DownloadAndSerialize();
		}

		private static void ProcessAllPrefabsWithLocalizationInjection(Action<PrefabInfo, GameObject> action)
		{
			var assetPath = Application.dataPath;
			var prefabs = Directory.GetFiles(assetPath, "*.prefab", SearchOption.AllDirectories);
			var enumerator = WaitProcessAllPrefabsWithLocalizationInjection(prefabs, action);
			EditorCoroutineUtility.StartCoroutineOwnerless(enumerator);
		}

		private static IEnumerator WaitProcessAllPrefabsWithLocalizationInjection(
			string[] prefabs,
			Action<PrefabInfo, GameObject> action
		)
		{
			for (var i = 0; i < prefabs.Length; i++)
			{
				var prefab = prefabs[i];
				var assetPath = prefab.Replace(Application.dataPath.Replace("Assets", ""), "");
				var prefabInfo = new PrefabInfo(assetPath, AssetDatabase.LoadAssetAtPath<GameObject>(assetPath));
				var instance = PrefabUtility.LoadPrefabContents(prefab);

				try
				{
					if (Filter(instance))
					{
						action(prefabInfo, instance);
						PrefabUtility.SaveAsPrefabAsset(instance, prefab);
					}
				}
				catch (Exception e)
				{
					Debug.LogError($"[{nameof(LocalizationEditorTools)}] {e}");
				}

				PrefabUtility.UnloadPrefabContents(instance);
				EditorUtility.DisplayProgressBar("Process", prefab, i / (float)prefabs.Length);
				yield return null;
			}

			EditorUtility.ClearProgressBar();
		}

		private static bool Filter(GameObject gameObject) => gameObject.TryGetComponent<LocalizationInjection>(out _);

		private class PrefabInfo
		{
			public readonly string AssetPath;
			public readonly GameObject Prefab;

			public PrefabInfo(string assetPath, GameObject prefab)
			{
				AssetPath = assetPath;
				Prefab = prefab;
			}
		}
	}
}