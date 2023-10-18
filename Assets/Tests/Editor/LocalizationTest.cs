using System.Collections.Generic;
using NUnit.Framework;
using Package.Localization.Runtime;
using Package.Localization.Runtime.Zenject;
using UnityEngine;

namespace Tests.Editor
{
	public class LocalizationTest
	{
		[Test]
		public void Test()
		{
			var provider = new LocalizationProvider(new Dictionary<string, string>
			{
				{"arg.one", "{0}"},
				{"draw.value", "value: {0}"},
			});

			var argsInner = Localizable.ToArgs("draw.value", "457");
			var args = Localizable.ToArgs(argsInner);
			var localizationValue = LocalizationValue.Build(args, 0, args.Length);
			var localize = localizationValue.Localize("arg.one", args, SystemLanguage.Afrikaans, provider);
			Debug.Log($"[{nameof(LocalizationTest)}] {localize}");
		}
		
		private class LocalizationProvider : ILocalizationProvider
		{
			private readonly Dictionary<string, string> _dictionary;

			public LocalizationProvider(Dictionary<string, string> dictionary)
			{
				_dictionary = dictionary;
			}

			public string Find(SystemLanguage language, string domainKey) 
				=> Find(language, domainKey, out var value) ? value : domainKey;

			public bool Find(SystemLanguage language, string domainKey, out string value) 
				=> _dictionary.TryGetValue(domainKey, out value);
		}
	}
}