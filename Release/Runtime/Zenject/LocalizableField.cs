namespace Playdarium.Localization.Runtime.Zenject
{
	public readonly struct LocalizableField
	{
		public readonly object Value;
		public readonly ILocalizationSettings Settings;

		public LocalizableField(object value, ILocalizationSettings settings)
		{
			Value = value;
			Settings = settings;
		}
	}
}