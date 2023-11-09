using System;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Models
{
	[Serializable]
	public class LanguageName
	{
		public static readonly LanguageName[] All =
		{
			new(SystemLanguage.Belarusian, "Belarusian", "be"),
			new(SystemLanguage.Bulgarian, "Bulgarian", "bg"),
			new(SystemLanguage.Catalan, "Catalan", "ca"),
			new(SystemLanguage.Czech, "Czech", "cs"),
			new(SystemLanguage.Danish, "Danish", "da"),
			new(SystemLanguage.Dutch, "Dutch", "nl"),
			new(SystemLanguage.English, "English", "en"),
			new(SystemLanguage.Estonian, "Estonian", "et"),
			new(SystemLanguage.Finnish, "Finnish", "fi"),
			new(SystemLanguage.French, "French", "fr"),
			new(SystemLanguage.German, "German", "de"),
			new(SystemLanguage.Greek, "Greek", "el"),
			new(SystemLanguage.Italian, "Italian", "it"),
			new(SystemLanguage.Japanese, "Japanese", "ja"),
			new(SystemLanguage.Korean, "Korean", "ko"),
			new(SystemLanguage.Latvian, "Latvian", "lv"),
			new(SystemLanguage.Norwegian, "Norwegian", "nb"),
			new(SystemLanguage.Polish, "Polish", "pl"),
			new(SystemLanguage.Portuguese, "Portuguese", "pt"),
			new(SystemLanguage.Russian, "Russian", "ru"),
			new(SystemLanguage.SerboCroatian, "Serbian", "sr"),
			new(SystemLanguage.Slovak, "Slovak", "sk"),
			new(SystemLanguage.Slovenian, "Slovenian", "sl"),
			new(SystemLanguage.Spanish, "Spanish", "es"),
			new(SystemLanguage.Swedish, "Swedish", "sv"),
			new(SystemLanguage.Thai, "Thai", "th"),
			new(SystemLanguage.Turkish, "Turkish", "tr"),
			new(SystemLanguage.Ukrainian, "Ukrainian", "uk"),
			new(SystemLanguage.Vietnamese, "Vietnamese", "vi")
		};

		public readonly string Code;
		public readonly SystemLanguage Id;
		public readonly string Name;

		public LanguageName(SystemLanguage id, string name, string code)
		{
			Id = id;
			Name = name;
			Code = code;
		}

		public static LanguageName FindById(SystemLanguage id)
		{
			return Array.Find(All, x => x.Id == id);
		}

		public static LanguageName FindByCode(string code)
		{
			return Array.Find(All, x => x.Code == code);
		}

		public static LanguageName FindByName(string name)
		{
			return Array.Find(All, x => x.Name == name);
		}
	}
}