using System;

namespace Package.Localization.Runtime.Zenject
{
	public interface ITextAccessStrategy
	{
		bool CanAccess(object obj);

		string GetText(object obj);

		void SetText(string text, object obj);

		void SubscribeOnChange(object obj, Action onChanged);
	}
}