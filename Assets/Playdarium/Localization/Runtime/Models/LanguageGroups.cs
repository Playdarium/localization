using System;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Models
{
	[CreateAssetMenu(menuName = "Localization/LanguageGroups", fileName = "LanguageGroups")]
	public class LanguageGroups : ScriptableObject
	{
		[SerializeField] private LanguageGroup[] groups =
		{
			new()
			{
				Substitution = SystemLanguage.Russian,
				Desire = new[]
				{
					SystemLanguage.Ukrainian,
					SystemLanguage.Belarusian
				}
			},
			new()
			{
				Substitution = SystemLanguage.German,
				Desire = new[]
				{
					SystemLanguage.Danish,
					SystemLanguage.Dutch
				}
			}
		};

		public SystemLanguage FindSubstitution(SystemLanguage[] availableLanguages, SystemLanguage desire)
		{
			if (Array.IndexOf(availableLanguages, desire) != -1)
				return desire;
			var cycle = 10;
			while (cycle > 0)
			{
				desire = FindSubstitution(desire);
				if (Array.IndexOf(availableLanguages, desire) != -1)
					return desire;
				cycle--;
			}

			return SystemLanguage.English;
		}

		public SystemLanguage FindSubstitution(SystemLanguage desire)
		{
			for (int i = 0, j = groups.Length; i < j; i++)
				if (Array.IndexOf(groups[i].Desire, desire) != -1)
					return groups[i].Substitution;

			return SystemLanguage.English;
		}
	}
}