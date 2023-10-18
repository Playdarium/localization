using Package.Localization.Runtime.Zenject.Impls;
using UnityEngine;
using Zenject;

namespace Package.Localization.Runtime.Zenject.Installers
{
	[CreateAssetMenu(menuName = "Installers/Localization/" + nameof(LocalizationSceneInstaller),
		fileName = nameof(LocalizationSceneInstaller))]
	public class LocalizationSceneInstaller : ScriptableObjectInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInitializableExecutionOrder<LocalizationInjector>(10000);
			Container.BindInterfacesTo<LocalizationInjector>().AsSingle();
		}
	}
}