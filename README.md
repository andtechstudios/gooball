# Gooball (Preview)
[![GitHub tag](https://img.shields.io/github/tag/AndrewMJordan/gooball.svg)](https://github.com/AndrewMJordan/gooball/tags)

Build, test, and manage Unity projects from the command line. Develop custom Unity packages, manipulate Unity files/folders, and more.

## Setup
### Prerequisites
Gooball is a [.NET tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) and requires at least [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/core).

### Installation
To install Gooball, use `dotnet tool install`.
```
> dotnet tool install --global Gooball

You can invoke the tool using the following command: goo
Tool 'gooball' was successfully installed.
```

To uninstall, use `dotnet tool uninstall`.
```
> dotnet tool uninstall --global Gooball

Tool 'gooball' was successfully uninstalled.
```

## Usage
All Gooball commands use the following grammar:
```
goo <CATEGORY> <COMMAND> [<OPTIONS>] <ARGUMENTS>
```

| Category | Syntax | Description |
| -: | :-: | :- |
| Project | `project` | Manage a Unity project. |
| Package Development |`package` | Manage a custom Unity package. |
| Unity | `unity` | Project-independent unity operations. |
| File Transformation | `transform` | Properly manipulate a Unity file/folder. |

### I. Project Commands
#### Build a Unity Project
```
goo project build [--editor <EDITOR_PATH>] [<PROJECT_PATH>] [-- <ARGS...>]
```
- If `PROJECT_PATH` is not provided, the current directory is used as the project path
- Use the `editor` option to use a specific version of the Unity editor
- `ARGS...` will be passed as arguments to `unity.exe`

#### Run Tests on a Unity Project
```
goo project test [--editor <EDITOR_PATH>] [<PROJECT_PATH>] [-- <ARGS...>]
```
- If `PROJECT_PATH` is not provided, the current directory is used as the project path
- Use the `editor` option to use a specific version of the Unity editor
- `ARGS...` will be passed as arguments to `unity.exe`

#### Open a Unity Project
```
goo project open [--editor <EDITOR_PATH>] [<PROJECT_PATH>] [-- <ARGS...>]
```
- If `PROJECT_PATH` is not provided, the current directory is used as the project path
- Use the `editor` option to use a specific version of the Unity editor
- `ARGS...` will be passed as arguments to `unity.exe`

Examples
```
Open Unity project in background
> goo project open "My Game" &
```
#### Print the Version of a Unity Project
```
goo project get-version [<PROJECT_PATH>]
```

#### Print the Editor Version of a Unity Project
```
goo project get-editor-version [<PROJECT_PATH>]
```

### II. Package Commands
#### Print the Version of a Package
```
goo package get-version [<PACKAGE_PATH>]
```

#### Ignore a Folder in a Package Manifest
```
goo package ignore-folder <PACKAGE_PATH> <FOLDER_PATH>
```

#### Bump the Version of a Package
```
goo package bump (--major | --minor | --patch) [<PACKAGE_PATH>]
```

### III. Unity Commands
#### Print All Installed Unity Editors
```
goo unity list-installs [<EDITOR_INSTALL_PATH>]
```

### IV. Transformation Commands
#### Hide a Folder from Unity Importer
```
goo transform hide-folder <FOLDER_PATH>
```

#### Inject Text into Unity Source Code File
```
goo transform inject <FILE_PATH> [<HEADERFILE_PATH>]
```
- If `HEADERFILE_PATH` is not provided, the header is read from `stdin`.

Examples
```
File argument example
> goo transform inject Program.cs licenseHeader.txt

Stream redirection example
> cat licenseHeader.txt | goo transform inject Program.cs
```

## Links
- [NuGet Gallery](https://www.nuget.org/packages/Gooball/)
- [GitHub](https://github.com/AndrewMJordan/gooball)
