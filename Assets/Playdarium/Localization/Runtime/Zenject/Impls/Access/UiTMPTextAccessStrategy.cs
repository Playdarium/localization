using TMPro;
using UnityEngine.Events;

namespace Playdarium.Localization.Runtime.Zenject.Impls.Access
{
	public class UiTMPTextAccessStrategy : ITextAccessStrategy
	{
		public bool CanAccess(object obj) => obj is TMP_Text;

		public string GetText(object obj)
		{
			var uiText = obj as TMP_Text;
			return uiText.text;
		}

		public void SetText(string text, object obj)
		{
			var uiText = obj as TMP_Text;
			uiText.text = text;
		}

		public void SubscribeOnChange(object obj, UnityAction onChanged)
		{
			var uiText = obj as TMP_Text;
			uiText.RegisterDirtyVerticesCallback(onChanged);
		}
	}
}