# Analyzing SystemLink Support Report Using Visual Studio Code

## Goal

This tutorial aims to enhance log file analysis using Visual Studio Code's text editing features along with the Log File Highlighter extension.

SystemLink Reports serve as the primary starting point for troubleshooting SystemLink issues. Once you've obtained the report by following the steps in the [Generate a SystemLink Technical Support Report](https://knowledge.ni.com/KnowledgeArticleDetails?id=kA03q000000YGpmCAG&l=en-US) KB article, you can leverage Visual Studio Code (VS Code) to open and analyze its contents.

---
## Prerequisites

- Install the latest version of [Visual Studio Code](https://code.visualstudio.com/download).
- Install the [Log File Highlighter](https://marketplace.visualstudio.com/items?itemName=emilast.LogFileHighlighter) extension.

> **Note:** While the instructions are provided for Windows, the process is similar for Mac and Linux.

---
## Index

- [Setting up VS Code for Log Analysis](#setting-up-vs-code-for-log-analysis)
- [Effective Strategies for Log Analysis](#effective-strategies-for-log-analysis)

---
## Setting up VS Code for Log Analysis

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

---
## Effective Strategies for Log Analysis

Proficiently navigating the realm of SystemLink Technical Support Reports can often prove to be a challenging task. Within these reports lie an array of folders housing log files, each adhering to different standards and containing thousands of lines of intricately detailed technical data. A standard report encompasses logs from various components, including:

- MongoDB
- NI WebServer
- Postgres
- RabbitMQ
- Salt-Stack
- Skyline

This diversity poses a challenge for comprehending the situation at hand, especially when dealing with technologies one might not be well-versed in. However, there are strategic measures that can be taken to extract pertinent information and effectively collaborate with subject matter experts (SMEs) and research and development (R&D) teams.

### 1. Contextual Information Collection

Begin by obtaining relevant context from the user to narrow the focus of your investigation. Despite SystemLink being perceived as a singular application, it is actually an amalgamation of discreet, independently functioning mini-applications within a unified web user interface. Identifying the specific application encountering issues (e.g., Test Insights, Systems Management, Analysis Procedure, Jupyter, etc.) aids in the elimination of potential factors while delving into the logs.

Further refinement can be achieved by pinpointing the precise action leading to the anomaly. Several examples include:

- A state that does not apply or returns an error
- Failure of one or all queued jobs
- Errors during the retrieval of packages from ni.com
- Inability to load test result data
- Non-appearance of a particular app within SystemLink

Requesting the date and time of the error occurrence, provided it is within the user's control, is valuable. Log files often span several months and may encompass issues that have since been resolved. Having this timestamp helps in filtering out unrelated data.

### 2. Examine the `services.txt`

When troubleshooting services that have not properly initialized, a prudent starting point is the `services.txt` file. Identify the services that have encountered difficulties and subsequently focus on the associated folders. This approach sheds light on the underlying technologies responsible for potential disruptions.

### 3. Leverage the Readme

Undoubtedly, the `readme.txt` file holds valuable guidance. It directs your attention to the exact folders housing the relevant logs pertinent to the specific scenario you are investigating.

### 4. Proficiency with the VS Code Search Tool

Harness the capabilities of the VS Code Search Tool `(Ctrl+Shift+F)` to streamline your log analysis efforts. Here are notable functionalities:

- Effortlessly locate occurrences of the keyword "Error" within a designated folder (particularly useful when you have insights into the malfunctioning process).
- Employ Regular Expressions (RegEx) for a more intricate search. For instance, utilize `^2023-05-30 15:08:.*Error.*` to identify instances of the term "Error" within lines bearing the date stamp of 2023-05-30.
- The "files to include" feature allows for precise narrowing of the search scope. Tailor your search by selecting specific folders or files based on extensions or names.

By incorporating these strategic approaches and adhering to a methodical approach, you are primed to unlock the mysteries concealed within these logs.

---
### Conclusion

Extracting pertinent information from the SystemLink Technical Support Report can indeed pose a challenge. It's important to remember that this process is often a vital step in troubleshooting, even though it might not always lead directly to the root cause of the issue. However, employing the appropriate analysis techniques will certainly bring you closer to identifying the underlying factors. This not only simplifies the task for subject matter experts and developers, but also has a positive ripple effect on resolution time and overall customer satisfaction. A powerful tool like VS Code seamlessly enhances this workflow.


---
### Additional Resources

For more information and references, check out these resources:

- [Search across files - VS Code User Guide](https://code.visualstudio.com/docs/editor/codebasics#_search-across-files)
- [SystemLink Log File Locations for Troubleshooting](https://knowledge.ni.com/KnowledgeArticleDetails?id=kA00Z000000kGcSSAU&l=en-US)

---
**Feedback:** Help us improve this tutorial by providing feedback, reporting issues, or suggesting enhancements. :smiley:

**Author:** Felipe Flores, Senior Technical Support Engineer at NI.

**Last Updated:** August 18th, 2023.