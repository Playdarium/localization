using System.Collections.Generic;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Models
{
	[CreateAssetMenu(menuName = "Localization/LocalizationData", fileName = "LocalizationData")]
	public class LocalizationData : ScriptableObject
	{
		[SerializeField] private SystemLanguage[] languagesMap;
		[SerializeField] private SystemLanguage[] languages;
		[SerializeField] private List<Key> keys;

		public SystemLanguage[] LanguagesMap => languagesMap;

		public SystemLanguage[] Languages => languages;

		public IEnumerable<Key> Keys => keys;

		public Key Find(string search)
		{
			for (var i = 0; i < keys.Count; i++)
			{
				var key = keys[i];
				if (key.Name != search)
					continue;

				return key;
			}

			return null;
		}
	}
}