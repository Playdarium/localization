using Playdarium.Localization.Runtime.Impls;
using Playdarium.Localization.Runtime.Models;
using Playdarium.Localization.Runtime.Zenject.Impls;
using Playdarium.Localization.Runtime.Zenject.Impls.Access;
using UnityEngine;
using Zenject;

namespace Playdarium.Localization.Runtime.Zenject.Installers
{
	[CreateAssetMenu(menuName = "Installers/Localization/" + nameof(LocalizationProjectInstaller),
		fileName = nameof(LocalizationProjectInstaller))]
	public class LocalizationProjectInstaller : ScriptableObjectInstaller
	{
		[SerializeField] private LocalizationData localizationData;
		[SerializeField] private LanguageGroups languageGroups;

		public override void InstallBindings()
		{
			Container.Bind<LocalizationData>().FromInstance(localizationData)
				.WhenInjectedInto(typeof(LocalizationService), typeof(LocalizationProvider));
			Container.Bind<LanguageGroups>().FromInstance(languageGroups)
				.WhenInjectedInto(typeof(LocalizationService), typeof(LocalizationProvider));

			Container.Bind<ILocalizationProvider>().To<LocalizationProvider>().AsSingle()
				.WhenInjectedInto(typeof(DynamicLocalizableObject), typeof(StaticLocalizableObject));
			Container.Bind<ILocalizationService>().To<LocalizationService>().AsSingle();

			Container.BindInterfacesTo<LocalizableObjectFactory>().AsSingle();
			BindLocalizableObjectFactory<DynamicLocalizableObject, DynamicLocalizableObject.Factory>();
			BindLocalizableObjectFactory<StaticLocalizableObject, StaticLocalizableObject.Factory>();

			Container.BindInterfacesTo<LocalizationInjector>().AsSingle();

			Container.BindInterfacesTo<UiTMPTextAccessStrategy>().AsCached();
		}

		private void BindLocalizableObjectFactory<TContract, TFactory>()
			where TFactory : PlaceholderFactory<object, ILocalizationSettings, ITextAccessStrategy, TContract>
			=> Container.BindFactory<object, ILocalizationSettings, ITextAccessStrategy,
				TContract, TFactory>().AsSingle();
	}
}