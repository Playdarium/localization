using System;
using UnityEngine;

namespace Package.Localization.Runtime.Models
{
	[Serializable]
	public class Value
	{
		[SerializeField] private SystemLanguage language;
		[SerializeField] private string text;

		public SystemLanguage Language
		{
			get => language;
			set => language = value;
		}

		public string Text
		{
			get => text;
			set => text = value;
		}

		public Value(SystemLanguage language, string text)
		{
			this.language = language;
			this.text = text;
		}
	}
}