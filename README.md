# Gooball (Preview)
Build, test, and manage Unity projects from the command line. Develop custom Unity packages, manipulate Unity files/folders, and more.

## Setup
### Prerequisites
Gooball is a [.NET tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) and requires at least [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/core).

### Installation
To install Gooball, use `dotnet tool install`.
```shell
> dotnet tool install --global Gooball

You can invoke the tool using the following command: goo
Tool 'gooball' was successfully installed.
```

To uninstall, use `dotnet tool uninstall`.
```shell
> dotnet tool uninstall --global Gooball

Tool 'gooball' was successfully uninstalled.
```

## Usage
All Gooball commands use the following grammar:
```shell
goo <CATEGORY> <COMMAND> [<OPTIONS>] <ARGUMENTS>
```

| Category | Syntax | Description |
| -: | :-: | :- |
| Project | `project` | Manage a Unity project. |
| Package Development |`package` | Manage a custom Unity package. |
| Unity | `unity` | Access information about installed Unity editors. |
| File Transformation | `transform` | Properly manipulate a Unity file/folder. |

### I. Project Commands
#### Build a Unity Project
```shell
goo project build <PROJECT_PATH> [(-x | --editor) <EDITOR_PATH>] [-- <ARGS...>]
```
- Use the `editor` option to use a specific version of the Unity editor
-  `ARGS...` will be passed as arguments to `unity.exe`

#### Run Tests on a Unity Project
```shell
goo project test <PROJECT_PATH> [(-x | --editor) <EDITOR_PATH>] [-- <ARGS...>]
```
- Use the `editor` option to use a specific version of the Unity editor
-  `ARGS...` will be passed as arguments to `unity.exe`

#### Print the Version of a Unity Project
```shell
goo project get-version <PROJECT_PATH>
```

#### Print the Editor Version of a Unity Project
```shell
goo project get-editor-version <PROJECT_PATH>
```

### II. Package Commands
#### Print the Version of a Package
```shell
goo package get-version <PROJECT_PATH>
```

#### Ignore a Folder in a Package Manifest
```shell
goo package ignore-folder <PROJECT_PATH> <FOLDER_PATH>
```

#### Bump the Version of a Package
```shell
goo package bump (--major | --minor | --patch) <PROJECT_PATH>
```

### III. Unity Commands
#### Print All Installed Unity Editors
```shell
goo unity list-editors [<EDITOR_INSTALL_PATH>]
```

### IV. Transformation Commands
#### Hide a Folder from Unity Importer
```shell
goo transform hide-folder <FOLDER_PATH>
```

#### Inject Text into Unity Source Code File
```shell
goo transform inject <FILE_PATH> [<HEADERFILE_PATH>]
```
- If `HEADERFILE_PATH` is not provided, the header is read from `stdin`.

Examples
```shell
File argument example
> goo transform inject Program.cs licenseHeader.txt

I/O redirection example
> cat licenseHeader.txt | goo transform inject Program.cs
```

