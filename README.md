# Installation

Using the native Unity Package Manager introduced in 2017.2, you can add this library as a package by modifying your
`manifest.json` file found at `/ProjectName/Packages/manifest.json` to include it as a dependency. See the example below
on how to reference it.

```
"com.playdarium.localization": "https://gitlab.com/pd-packages/localization.git#upm-1.0.0"
```

# <a id="localizationGuide">Localization guide</a>

### Localize static text:

For localize static text localizable **MonoBehaviour** need implement interface **ILocalizable**
and to localizable field add attribute LocalizationAttribute. Localizable field should be **private** and *
*SerializeField** or **public**.

```c#
public class SomeView : UiView, ILocalizable
{
    [Localization("localization.key")] 
    [SerializeField]
    private Text text;
}
```

### Localize dynamic text:

For dynamic localization used localization pattern. Text arguments should be converted to patter by **
Localizable.ToDynamicArgs(params string[] strings)**.

```c#
public class SomeView : UiView, ILocalizable
{
    [Localization("localization.key", true)] 
    [SerializeField]
    private Text text;
}
```

Localization key can be pattern arguments.

#### Localization keys:

| Keys        | Values     |
|-------------|------------|
| text.format | "{0}: {1}" |
| text.score  | "Score"    |

```c#
public class SomeView : UiView, ILocalizable
{
    [Localization("text.format", true)] 
    [SerializeField]
    private Text textField;
}

...

textField.text = Localizable.ToDynamicArgs("text.score", 100.ToString());

// Output: "Score: 100"
```

### Dynamic localizable object injection:

Add to prefab text object add components **LocalizationDynamicInjection**.

## Multi inner keys support

| Keys              | Values               |
|-------------------|----------------------|
| text.for_join     | "Form join {0}"      |
| text.press_button | "press button '{0}'" |

```c#
var buttonPattern = Localizable.ToDynamicArgs("text.press_button", "Tab");
textField.text = Localizable.ToDynamicArgs("text.for_join", buttonPattern);

// Output: "Form join press button 'Tab'"
```

## Dynamic localizable object injection:

Add to prefab text object add components **UiTextLocalization** or **TextProLocalization** and *
*LocalizationDynamicInjection**.

