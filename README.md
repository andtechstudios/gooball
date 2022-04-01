[![GitHub tag](https://img.shields.io/nuget/v/Andtech.Gooball)](https://www.nuget.org/packages/Andtech.Gooball/)

## Setup

### Prerequisites
* [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools)
* Make sure [NuGet Gallery](https://nuget.org) is registered as a source in your NuGet configuration (it is by default).

```shell
$ dotnet nuget list source
Registered Sources:
  1.  nuget.org [Enabled]
      https://api.nuget.org/v3/index.json
```

### Installation
1. Use `dotnet tool install`.
```shell
$ dotnet tool install --global Andtech.Gooball
```

### Uninstallation
1. Use `dotnet tool uninstall`.
```shell
$ dotnet tool uninstall --global Andtech.Gooball
```

---

## Usage

### Unity Project Commands
```
goo <command> [<projectPath>] [--follow] [-- <args>]
Gooball commands:
	open	Open a Unity project with a unity editor
	test	Run tests on a Unity project
	build	Build a unity project
	run		Run arbitrary Unity commands
```

| Parameter | Description | Remarks |
| --- | --- | --- |
| `projectPath` | The path to the Unity project. | Default: `./` |
| `--follow` | (Experimental) Output appended data to console as the Unity log grows. |  |
| `args` | Arguments to pass to the Unity process (e.g. `Unity.exe`). |  |

### Miscellaneous Commands

#### List installed Unity editors
```
goo list
```

#### Hide assets
```
goo hide [--in-package <packagePath>] <assetPath>
```

| Parameter | Description | Remarks |
| --- | --- | --- |
| `--in-package` | The path to the Unity project. | This folder should contain your `package.json`. |
| `assetPath` | The path to the asset. | This will hide the asset and delete the associated `meta` file. |

---

## Examples

### Open a Unity project with a Unity editor
```
$ goo MyProject
```

### Build a Unity project
```
$ goo build MyProject -- -executeMethod BuildAll
```

### Run tests on Unity project
```
$ goo test MyProject -- -testResults results.xml
```

### Run arbitrary Unity commands
```
$ goo run -- -batchMode -quit -executeMethod ExportPackages
```

### Hide assets from the Unity asset database

*This hides assets from Unity during the import process (see [Unity - Manual: Special folder names](https://docs.unity3d.com/Manual/SpecialFolders.html) for more details)*

```
$ goo hide MyProject/Assets/README.md
```

```
$ goo hide --in-package MyProject/Assets/MyPackage MyProject/Assets/MyPackage/Samples
```

### List installed Unity editors
```
$ goo list
2021.1.24f1	[/mnt/c/Program Files/Unity/Hub/Editor/2021.1.24f1/Editor/Unity.exe]
2021.2.0f1	[/mnt/c/Program Files/Unity/Hub/Editor/2021.2.0f1/Editor/Unity.exe]
```
