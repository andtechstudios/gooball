# Gooball
*Build, test, and manage Unity projects from the command line. Develop custom Unity packages, manipulate Unity files/folders, and more.*

[![GitHub tag](https://img.shields.io/nuget/v/Andtech.Gooball)](https://www.nuget.org/packages/Andtech.Gooball/)

# Setup
## Prerequisites
* Gooball is a [.NET tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) and requires at least [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/core).

## Installation
1. Install with `dotnet tool install`.
```
$ dotnet tool install --global Gooball
```

## Uninstallation
1. Use `dotnet tool uninstall`.
```
$ dotnet tool uninstall --global Gooball
```

# Usage

## Open a Unity project with a Unity editor
```
$ goo <PROJECT_PATH> [-- [args..]]
```

> Everything after `--` will be passed to the Unity process (e.g. `Unity.exe`).

## Build a Unity project
```
$ goo build <PROJECT_PATH> [-- [args..]]
```

> Everything after `--` will be passed to the Unity process (e.g. `Unity.exe`).

## Run tests on Unity project
```
$ goo test <PROJECT_PATH> [-- [args..]]
```

> Everything after `--` will be passed to the Unity process (e.g. `Unity.exe`).

## Run arbitrary Unity commands
```
$ goo run [-- [args..]]
```

> Everything after `--` will be passed to the Unity process (e.g. `Unity.exe`).

## Hide assets from the Unity asset database

*This hides assets from Unity during the import process (see [Unity - Manual: Special folder names](https://docs.unity3d.com/Manual/SpecialFolders.html) for more details)*

```
$ goo hide <PATH_TO_ASSET>
$ goo hide [--in-package <PATH_TO_PACKAGE>] <PATH_TO_ASSET>
```

## List installed editors
```
$ goo list
```
