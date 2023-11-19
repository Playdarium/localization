using System.Collections.Generic;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizationInjector
	{
		void Localize(Component localizableView, List<ILocalizableObject> localizableObjects);

		ILocalizableObject Localize(object localizable, ILocalizationSettings settings);
	}
}