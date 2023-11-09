using System.Collections.Generic;
using UnityEngine;

namespace Playdarium.Localization.Runtime.Zenject
{
	public interface ILocalizationInjector
	{
		void Localize(Component localizable, List<ILocalizableObject> localizableObjects);
	}
}