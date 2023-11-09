using UnityEngine.Events;
using UnityEngine.UI;

namespace Playdarium.Localization.Runtime.Zenject.Impls.Access
{
	public class UiTextAccessStrategy : ITextAccessStrategy
	{
		public bool CanAccess(object obj) => obj is Text;

		public string GetText(object obj)
		{
			var uiText = obj as Text;
			return uiText.text;
		}

		public void SetText(string text, object obj)
		{
			var uiText = obj as Text;
			uiText.text = text;
		}

		public void SubscribeOnChange(object obj, UnityAction onChanged)
		{
			var uiText = obj as Text;
			uiText.RegisterDirtyVerticesCallback(onChanged);
		}
	}
}