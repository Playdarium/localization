namespace Package.Localization.Runtime
{
	public interface ILanguageChangeListener
	{
		void OnLanguageChanged(ILocalizationProvider localizationProvider);
	}
}