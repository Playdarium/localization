using System.Reflection;

namespace Package.Localization.Runtime.Zenject
{
    public class LocalizableField
    {
        public readonly LocalizationAttribute Attribute;
        public readonly FieldInfo FieldInfo;

        public LocalizableField(LocalizationAttribute attribute, FieldInfo fieldInfo)
        {
            Attribute = attribute;
            FieldInfo = fieldInfo;
        }
    }
}