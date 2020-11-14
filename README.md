# Gooball (Preview)
CLI tools for Unity projects

### Unity Commands
`goo (build | test) <project_path> [(-x | --editor) <editor_path>] [-- args...]`
- Use `editor_path` to use a specific version of the Unity editor
- `args...` will be passed as arguments to `unity.exe`

### Project Commands
`goo project (get-version | get-editor-version) <project_path>`

### Custom Package Commands
`goo package get-version <package_path>`
`goo package ignore-folder <package_path> <folder_path>`
`goo package bump (--major | --minor | --patch) <package_path>`

### Editor Commands
`goo editor list [<editor_install_path>]`

### Other Commands
`goo inject <targetfile_path> [<headerfile_path>]`
- If `headerfile_path` is not provided, the header is read from `stdin`.

`goo hide-folder <folder_path>`
