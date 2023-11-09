using UnityEngine;

namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizableObjectFactory
	{
		ILocalizableObject Create(LocalizableField field, Component localizable);
	}
}