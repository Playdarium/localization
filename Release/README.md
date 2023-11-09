# Localization

[![openupm](https://img.shields.io/npm/v/com.playdarium.localization?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.playdarium.localization/)
<img alt="GitHub" src="https://img.shields.io/github/license/Playdarium/localization">

## Installing

Using the native Unity Package Manager introduced in 2017.2, you can add this library as a package by modifying your
`manifest.json` file found at `/ProjectName/Packages/manifest.json` to include it as a dependency. See the example below
on how to reference it.

### Install via OpenUPM

The package is available on the [openupm](https://openupm.com/packages/com.playdarium.localization/)
registry.

#### Add registry scope

```
{
  "dependencies": {
    ...
  },
  "scopedRegistries": [
    {
      "name": "Playdarium",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.playdarium"
      ]
    }
  ]
}
```

#### Add package in PackageManager

Open `Window -> Package Manager` choose `Packages: My Regestries` and install package

### Install via GIT URL

```
"com.playdarium.localization": "https://github.com/Playdarium/localization.git#upm"
```

# Usage

## Install localization

Create scriptable installer `Installers/Localization/LocalizationProjectInstaller` and assign it to your
**ProjectContext**

## Add localization component

Add component **LocalizationInjection** on your **GameObject** and assign **Components** what you need localize.
Script **LocalizationInjection** automatically collect all fields with **LocalizationAttribute** from assigned
components.

## Localize fields

### Localize static text:

For localize static text add attribute **LocalizationAttribute** to target field. Target field should be **private**
and **SerializeField** or **public**.

```c#
public class SomeView : MonoBehaviour
{
    [Localization("localization.key")] 
    [SerializeField]
    private Text text;
}
```

### Localize dynamic text:

For dynamic localization you need provide text contained args pattern.
Args pattern you can create using **Localizable.ToArgs(params string[] args)**.

```c#
public class SomeView : MonoBehaviour
{
    [Localization("localization.key", true)] 
    [SerializeField]
    private Text text;
}
```

Localization key can contain formatting arguments.

#### Localization keys:

| Keys        | Values     |
|-------------|------------|
| text.format | "{0}: {1}" |
| text.score  | "Score"    |

```c#
public class SomeView : MonoBehaviour
{
    [Localization("text.format", true)] 
    [SerializeField]
    private Text textField;
}

...

// Assign text to field in your controller script
textField.text = Localizable.ToArgs("text.score", "100");

// Output: "Score: 100"
```
