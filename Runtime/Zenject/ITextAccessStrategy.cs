using UnityEngine.Events;

namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ITextAccessStrategy
	{
		bool CanAccess(object obj);

		string GetText(object obj);

		void SetText(string text, object obj);

		void SubscribeOnChange(object obj, UnityAction onChanged);
	}
}