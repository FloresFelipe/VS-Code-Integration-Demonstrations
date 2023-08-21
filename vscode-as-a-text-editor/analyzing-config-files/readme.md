# Analyzing Configuration Files Using Visual Studio Code

## Goal

This tutorial aims to guide you through the process of analyzing configuration files from LabVIEW and TestStand using Visual Studio Code. You'll learn how to set up and utilize VS Code for this purpose, focusing on specific file types.

## Prerequisites

- Install [Visual Studio Code](https://code.visualstudio.com/download).
- Install the [XML Tools Extension](https://marketplace.visualstudio.com/items?itemName=DotJoshJohnson.xml).
- Install the [Ini for VSCode Extension](https://marketplace.visualstudio.com/items?itemName=DavidWang.ini-for-vscode).

## Index

- [Analyzing the TestStand Configuration Files](#analyzing-the-teststand-configuration-files)
- [Analyzing LabVIEW.ini](#analyzing-labviewini)



### Analyzing the TestStand Configuration Files

TestStand stores configuration filesin the <TestStand Application Data>\Cfg and <TestStand Application Data>\Cfg\ModelPlugins directories. Click on [this link](https://www.ni.com/docs/en-US/bundle/teststand/page/tsdeploysystem/infotopics/configurationfiles.htm) for a list of commonly modified configuration files, with the information TestStand stores in those files, and whether to include those configuration files in a deployment.

Now, let's open one of these folders and use VS code to analyze the files.

1. Navigate to the `<TestStandAppData>\Cfg` folder.
2. Open the extracted folder. Right-click and select "Open with VS Code".
    - *The name of this option might slightly vary depending on your VS Code version.*

> **Note:** You might encounter a "Do you trust the authors of the files in this folder?" prompt. If prompted, ensure you trust the authors of all files in the parent folder and click "Yes, I trust the authors."

3. The TestStand `Cfg` folder becomes your active workspace. Now, you need to establish the workspace settings.

4. Press `Ctrl+Shift+P` to open the command palette. Type "Preferences: Open Workspace Settings (JSON)".
    - This action creates a `.vscode` directory in your workspace root, containing a `settings.json` file to store workspace-specific settings.

> **Note:** This step is necessary because some TestStand config files have custom extensions but adhere to standard formatting. For instance, `SearchDirectories.cfg` and `FileDiffer.ini` contain XML strings as their content.

5. Paste the following JSON string into the `settings.json` file:

```json
{
    "files.associations": {
        // Parse as XML
        "*.cfg": "xml",
        "SequenceEditor.isl": "xml",
        "FileDiffer.ini": "xml",
        "Analyzer.ini": "xml",
        "CustomRules.tsarules": "xml",
        // Parse as INI
        "TeststandPersistedOptions.opt": "ini"
    }
}
```

6. Notice that the config files are now parsed correctly, enabling easy navigation. XML files display the XML Document section in the Explorer pane, while INI files show the Outline section. In both cases, you can navigate the file by clicking the navigation bar at the top of the file display pane.

> **Note:** VS Code won't display binary files. If you encounter one, VS Code will notify you that it's unable to display a binary file.



### Analyzing LabVIEW.ini

In contrast to TestStand, creating a workspace for the INI file isn't necessary. Here, you'll learn how to set the file association for a single file.

1. Right-click and open the `LabVIEW.ini` file located at `<Program Files>\National Instruments\LabVIEW 20XX`. Choose the option "Edit with VS Code".

2. The file should be parsed by the Ini for VSCode Extension. If it isn't, you can click the current associated file type on the status bar at the bottom of the UI, then type "ini" and press enter. The file should now be parsed as an INI file.

3. Utilize the Outline section or the navigation bar to explore the file's sections and keys.

## Troubleshooting

If there are common issues or challenges that readers might encounter, list them here along with possible solutions or workarounds.

- **Issue 1:** Description of the issue.
    - Solution or workaround.

- **Issue 2:** Description of the issue.
    - Solution or workaround.



## Conclusion

In this tutorial, you've learned how to effectively analyze configuration files from LabVIEW and TestStand using Visual Studio Code. By setting up the appropriate extensions and workspace settings, you can easily navigate and understand the content of these configuration files. Visual Studio Code provides a versatile and user-friendly environment for exploring these files, helping you gain insights into your LabVIEW and TestStand setups. Armed with this knowledge, you'll be better equipped to manage and fine-tune your system configurations efficiently.

### Additional Resources

- [Configuration Files](https://www.ni.com/docs/en-US/bundle/teststand/page/tsdeploysystem/infotopics/configurationfiles.htm)
- [LabVIEW configuration file](https://labviewwiki.org/wiki/LabVIEW_configuration_file) - Not NI Official
- [File Associations](https://code.visualstudio.com/docs/getstarted/tips-and-tricks#_file-associations) - VS Code Docs


**Feedback:** Help us improve this tutorial. Please provide feedback, report issues, or suggest enhancements. :smiley:

**Author:** Felipe Flores, Senior Technical Support Engineer at NI.

**Last Updated:** August 15th, 2023.