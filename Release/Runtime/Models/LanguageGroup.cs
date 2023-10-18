using System;
using UnityEngine;

namespace Package.Localization.Runtime.Models
{
	[Serializable]
	public class LanguageGroup
	{
		[SerializeField] private SystemLanguage[] desire;
		[SerializeField] private SystemLanguage substitution;

		public SystemLanguage[] Desire
		{
			get => desire;
			set => desire = value;
		}

		public SystemLanguage Substitution
		{
			get => substitution;
			set => substitution = value;
		}
	}
}