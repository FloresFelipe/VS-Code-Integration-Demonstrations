# Analyzing SystemLink Support Report Using Visual Studio Code

## Goal

This tutorial aims to enhance log file analysis using Visual Studio Code's text editing features along with the Log File Highlighter extension.

SystemLink Reports serve as the primary starting point for troubleshooting SystemLink issues. Once you've obtained the report by following the steps in the [Generate a SystemLink Technical Support Report](https://knowledge.ni.com/KnowledgeArticleDetails?id=kA03q000000YGpmCAG&l=en-US) KB article, you can leverage Visual Studio Code (VS Code) to open and analyze its contents.

## Prerequisites

- Install the latest version of Visual Studio Code.
- Install the [Log File Highlighter](https://marketplace.visualstudio.com/items?itemName=emilast.LogFileHighlighter) extension.

> **Note:** While the instructions are provided for Windows, the process is similar for Mac and Linux.

## Steps

1. Extract the `SystemLink_support_mm-dd-yy.zip` file to your preferred location.

2. Open the extracted folder. Right-click and select "Open with VS Code".
    - *The name of this option might slightly vary depending on your VS Code version.*
    
3. VS Code will open, and the SystemLink TechSupport Folder will be your active workspace. Refer to [VS Code Workspace Documentation](https://code.visualstudio.com/docs/editor/workspaces) to learn more about workspaces in VS Code.

4. Define Workspace Settings for the Log File Highlighter extension and file associations. These settings are confined to the current workspace to prevent configuration mix-ups when handling multiple log workspaces.

5. Press `Ctrl+Shift+P` to open the command palette. Type "Preferences: Open Workspace Settings (JSON)".
    - This will create a `.vscode` directory in your workspace root, housing a `settings.json` file to store workspace-specific settings.

6. Paste the following JSON string into the `settings.json` file:

```json
{
    "files.associations": {
        "*": "log",
        "*.json": "json"
    },
    "logFileHighlighter.customPatterns": [
        // Custom patterns for services.txt
        {
            "pattern": "Live|Go Live",
            "foreground": "#04fa04aa",
            "textDecoration": "underline"
        },
        {
            "pattern": "^(\\w+\\s*)*:\\s",
            "foreground": "#1586e8ee",
            "textDecoration": "bold"
        },
        {
            "pattern": "Running",
            "foreground": "#62659e"
        },
        // NI Web Server Logs
        {
            "pattern": "\\[[a-zA-Z_]+:warn\\]",
            "background": "#ff7700fb"
        },
        {
            "pattern": "\\[[a-zA-Z_]+:error\\]",
            "background": "#e40202ee"
        },
        {
            "pattern": "\\[[a-zA-Z_]+:notice\\]",
            "background": "#350038ee"
        },
        // Custom pattern for sysinfo.txt
        {
            "pattern": "\\t[[a-zA-Z\\s]+",
            "foreground": "#00f37a"
        }
    ]
}
```
These preset Log File Highlighter settings are designed to color custom log files in the SystemLink Support logs. File associations are configured to detect log types, enabling the Log File Highlighter to parse log files (except JSON files in the Logs folder). Feel free to customize these settings as desired. Refer to the Log File Highlighter documentation on the extension's page for more details on creating custom settings.

7. Save the settings file.
8. Open one of the log files; you'll notice that the text is now highlighted with distinct colors, making relevant information stand out.
