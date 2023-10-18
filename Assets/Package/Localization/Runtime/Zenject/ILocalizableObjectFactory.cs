using Zenject;

namespace Package.Localization.Runtime.Zenject
{
	public interface ILocalizableObjectFactory : IFactory<LocalizableField, ILocalizable, ILocalizableObject>
	{
	}
}