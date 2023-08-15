# Developing and Debugging a C++ DLL Called by LabVIEW or TestStand

## Goal

Learn the process of setting up VSCode for C/C++ development and debugging a Dynamic Link Library (DLL) that is called by LabVIEW or TestStand.

## Prerequisites

- Install Visual Studio Code
- Install the [C/C++ Extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode.cpptools)

> **Note:** The C/C++ extension for Visual Studio Code is provided by Microsoft to enable cross-platform C and C++ development on Windows, Linux, and macOS. When you create a \*.cpp file, the extension adds features such as syntax highlighting, smart completions, hovers (IntelliSense), and error checking.

## Table of Contents

- [Setting up the VS Code Environment](#setting-up-the-vs-code-environment)
- [Creating the Code](#creating-the-code)
- [Building the DLL](#building-the-dll)
- [Attaching to LabVIEW](#attaching-to-labview)
- [Attaching to TestStand](#attaching-to-teststand)
- [Troubleshooting](#troubleshooting)
- [Conclusion](#conclusion)
- [Additional Resources](#additional-resources)

---

## Setting up the VS Code Environment

In this tutorial, we will configure VS Code for Microsoft C++, though other options like GCC and Clang are available.

### Microsoft Visual C++ (MSVC) Compiler Toolset

If you have Visual Studio installed, open the Visual Studio Installer and ensure that "Desktop development with C++" is selected. If not, you can check it and then click "Modify."

If you don't have Visual Studio, you can install "Desktop development with C++" only. From the Visual Studio Downloads page, locate "Tools for Visual Studio" under the "All Downloads" section and select the download for "Build Tools for Visual Studio 2022."

### Verifying Microsoft Visual C++ Installation

To use MSVC from a command line or VS Code, you must run it from a Developer Command Prompt for Visual Studio. Regular shells like PowerShell, Bash, or the Windows command prompt lack the necessary path environment variables.

To open the Developer Command Prompt for VS, start typing 'developer' in the Windows Start menu, and you should see it in the suggestions list. The exact name depends on your Visual Studio version. Select the appropriate item to open the prompt.

> **Note:** You can utilize the C++ toolset from Visual Studio Build Tools alongside Visual Studio Code to compile, build, and verify C++ codebases as long as you have a valid Visual Studio license (Community, Pro, or Enterprise) actively used for C++ development.

You can test if you have the C++ compiler (cl.exe) correctly installed by typing 'cl'; you should see a copyright message displaying the version and basic usage description.

> **Note:** If the Developer Command Prompt starts at the BuildTools location (not suitable for projects), navigate to your user folder (e.g., C:\users\{your username}\) before creating new projects.

## Creating the Code

Create a Project for a Simple DLL.

1. From the Developer Command Prompt, create an empty folder named "projects" to store your VS Code projects. Inside it, create a subfolder called "simple_dll." Navigate into it and open VS Code by entering the command `code .`

```bash
mkdir projects
cd projects
mkdir simple_dll
cd simple_dll
code .
```

> **Note:** The `code .` command opens VS Code in the current working folder, becoming your "workspace."

2. In the File Explorer title bar, click the New File button and name the file `simple_dll.cpp`.

3. Paste the following source code into `simple_dll.cpp` and save it. This code returns the sum of inputs `a` and `b`, both of integer type.

```C++

#include <iostream>

// The function that adds two integers
extern "C" __declspec(dllexport) int AddIntegers(int a, int b) {
    return a + b;
}

```

4. At the workspace root, create two folders: "bin" and "build." These will be used as destinations for binaries in a few steps.

5. Create a folder named `.vscode` at the workspace root, and within it, create the following two files:

`launch.json` - configuration file used to define how you want to launch and debug your code in Visual Studio Code.

```JSON
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Attach to DLL Client",
            "type": "cppvsdbg",
            "request": "attach",
            "processId": "${command:pickProcess}",
            "symbolSearchPath": "${workspaceFolder}\\bin",
            "stopAtEntry": false,
            "justMyCode": false,
            "sourceFileMap": {
                "/mnt/c": "C:/"
            }
        }
    ]
}

```

`tasks.json` - configuration file used to define tasks that automate build processes, code execution, or other custom actions.

```JSON
{
    "tasks": [
        {
            "type": "cppbuild",
            "label": "C/C++: Build DLL",
            "command": "cl.exe",
            "args": [
                "/LD",
                "/EHsc",
                "/Zi",
                "/Fo${workspaceFolder}\\build\\",
                "/Fe${workspaceFolder}\\bin\\Add.dll",
                "${file}"
            ],
            "group": {
                "kind": "build",
                "isDefault": true
            },
            "detail": "Task generated by Debugger."
        }
    ],
    "version": "2.0.0"
}

```

## Building the DLL

Now, let's use VS Code to build the C++ DLL.

1. Select the `simple_dll.cpp` file.
2. Open the Command Palette (Ctrl+Shift+P), type "Tasks: Run Build Task," and press Enter.
3. After the build process completes, you'll find the generated binaries in both the "bin" and "build" folders.

## Attaching to LabVIEW

With our DLL built, we can now use the VS Code debugger and attach it to LabVIEW.

1. Launch your 32-bit version of LabVIEW.
2. Create a blank VI, add a [Call Library Function Node](https://www.ni.com/docs/en-US/bundle/labview-api-ref/page/functions/call-library-function-node.html#:~:text=The%20Call%20Library%20Function%20Node%20consists%20of%20pairs%20of%20input,top%2Dto%2Dbottom%20order.), and configure it to call the `simple_dll.dll` we created.
3. Run the VI and observe the successful execution of the Add operation.
4. Back in VS Code, click the "Run and Debug" button (Ctrl+Shift+X).
5. Choose "Attach to DLL Client" from the dropdown menu and click the green play button.
6. Select the "LabVIEW.exe" process from the list after filtering results by typing "LabVIEW." VS Code is now attached to LabVIEW.
7. Set a breakpoint on line 5 of `simple_dll.cpp`.

> **Note:** Hover over the line numbers to reveal breakpoints. Clicking once sets a breakpoint, clicking again removes it.

8. In LabVIEW, run the VI; the code will break in VS Code at line five. Use VS Code's debugging tools to analyze the code.
9. After analyzing, click "Continue" in the VS Code debugger, and observe the VI completing execution.

## Attaching to TestStand

With the DLL built, we can attach the VS Code debugger to TestStand.

1. Launch the active version of TestStand.
2. Create a new sequence.
3. Add an Action Step using the [C/C++ DLL Adapter](https://www.ni.com/docs/en-US/bundle/teststand/page/tsref/infotopics/dll.htm), and provide a meaningful name.
4. In the step properties pane, specify the `simple_dll.dll`.
5. Click "Verify Prototype" and point to `simple_dll.cpp`.

> **Note:** The Verify Prototype button checks for conflicts between source code and parameter information on the Module tab. Refer to [Parsing Parameters from Source Code](https://www.ni.com/docs/en-US/bundle/teststand/page/tsref/infotopics/parsing_parameters_from_source_code.htm) for how the adapter interprets parameter declarations.

6. Fill the VALUE EXPRESSION column for Return Value, `a`, and `b`.
7. Back in VS Code, click "Run and Debug" (Ctrl+Shift+X).
8. Choose "Attach to DLL Client" from the dropdown and click the green play button.
9. Select the "SeqEdit.exe" process by filtering results with "SeqEdit." VS Code is now attached to TestStand.
10. Set a breakpoint on line 5 of `simple_dll.cpp`.

> **Note:** Hover over line numbers to manage breakpoints.

11. In TestStand, run the sequence (using Run MainSequence, for instance). The code will break

### Troubleshooting

If there are common issues or challenges that readers might encounter, list them here along with possible solutions or workarounds.

- **Issue 1:** Can't run VS Code from a Developer Command Prompt.

  - Follow the steps of [Running VS Code Outside a Developer Command Prompt](https://code.visualstudio.com/docs/cpp/config-msvc#_run-vs-code-outside-the-developer-command-prompt).

---

### Conclusion

In this tutorial, you learned how to setup VS Code to develope and debug a C++ DLL that can be called from LabVIEW or TestStand. This is the base knowledge you need to either support a customer who has problems with their DLLs, or even create example codes that can be properly tested for integrtion with LabvIEW or TestStand.

---

### Additional Resources

- [FAQ for C/C++ Development in VS Code](https://code.visualstudio.com/docs/cpp/faq-cpp)

---

**Feedback:** Feel free to suggest improvements to this article.

**Author:** Felipe Flores, a Senior Technical Support Engineer at NI.

**Last Updated:** August 15th, 2023
