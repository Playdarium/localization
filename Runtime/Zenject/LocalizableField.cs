namespace Playdarium.Localization.Runtime.Zenject
{
	public readonly struct LocalizableField
	{
		public readonly LocalizationAttribute Attribute;
		public readonly object Value;

		public LocalizableField(LocalizationAttribute attribute, object value)
		{
			Attribute = attribute;
			Value = value;
		}
	}
}