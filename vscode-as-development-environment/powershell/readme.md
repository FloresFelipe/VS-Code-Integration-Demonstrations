# Developing and Debugging PowerShell Scripts with VS Code

## Goal

PowerShell scripts can be incredibly useful for automating tasks on your computer. If you often find yourself following a sequence of steps, creating a script to automate the process not only saves time but also ensures consistency in your workflow.

## Prerequisites

- Install [Visual Studio Code](https://code.visualstudio.com/download).
- Install the [PowerShell](https://marketplace.visualstudio.com/items?itemName=ms-vscode.PowerShell) extension.


## Index

- [Automating NI Package Manager Troubleshooting Tasks with Power Shell.](#automating-ni-package-manager-troubleshooting-tasks-with-power-shell)
- [Create a launch.json to Debug the Script When Passing Arguments.](#create-a-launchjson-to-debug-the-script-when-passing-arguments)
- [ISE Mode](#ise-mode)



### Automating NI Package Manager Troubleshooting Tasks with Power Shell.

Automation simplifies troubleshooting tasks, ensuring that they are executed consistently and quickly. One vital troubleshooting action for NI Package Manager is capturing logs. If you need to assist a user with limited command-line skills, ensuring all logs are generated without affecting future installations, consider creating a script to streamline the process.

> **Note:**  Please be aware that some computers may either block script execution or require elevated privileges to run scripts successfully.

Follow these steps to automate the NI Package Manager log-capturing process:

1. Review the knowledge base article on [Generating and Locating NI Package Manager Error Logs](https://knowledge.ni.com/KnowledgeArticleDetails?id=kA03q000000YHe6CAG&l=en-US) to understand the main tasks involved:
    
    - Identifying the log folder location
    - Enabling specific flags in the NIPM global configuration file to generate additional error logs
    - Steps to generate these additional logs
    - Any recommended cleanup procedures

    > **Note:** Consider whether you'll be analyzing the logs locally or extracting them from another machine.

2. create a folder and name it `capture-all-logs-nipm`.
3. Open the folder, right-click, and select **Open Folder in VS Code**.
    - The name of this menu item might vary depending on your VS Code version.

4. Open the Command Palette (Ctrl+Shift+P) and type `New File`.
5. Choose Python File and save it as `capture-all-logs-nipm.ps1`.
6. Paste the following code snippet into the newly created PoweShell script file.

```PowerShell
# Clear the console to start with a clean output.
Clear-Host

# Enable MSI and Curl logs for NIPM.
Write-Host "Enabling MSI ba Curl Logs..."
& "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-set nipkg.plugin.wininst.msilogs-enabled=true
& "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-set nipkg.curldebugfile-enable=true

# TODO: Force an error to happen using NIPM CLI.
Write-Host "Forcing the error from NIPM CLI..."
Start-Sleep 5  # Sleep for 5 seconds simulate the error reproduction process.

# Capture NIPM logs and store them in a specified directory as a Zip file that can be easly shared.
Write-Host "Capturing Logs..."
& mkdir "C:\NIPM_LOGS" -Force # Create a directory to store the logs.
& Compress-Archive -Path "~\AppData\Local\National Instruments\NI Package Manager\Logs" -Destination "C:\NIPM_LOGs\NIPM_All_Logs.zip" -Force

# Disable MSI and cURL logs for NIPM.
Write-Host "Disabling MSI and cURL Logs..."
& "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-set nipkg.curldebugfile-enable=false
& "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-set nipkg.plugin.wininst.msilogs-enabled=false

# Display a completion message.
Write-Host "Script Finished Successfully!"
```
Overall, this script automates the process of enabling specific logs, simulating an error, capturing logs into a zip file, disabling logs, and providing feedback messages in the PowerShell console along the way. The "TODO" section is a placeholder where you can customize the script to force specific errors and handle generated logs as needed.

Optionally, you can run the `config-get` command to check if the log flags were correctly enabled and disabled.

```PowerShell
    # Check and display the current global settings for cURL and MSI logs again.
    Write-Host "Checking the Global Settings..."
    & "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-get *curldebugfile-enable
    & "C:\Program Files\National Instruments\NI Package Manager\nipkg.exe" config-get *msilogs-enabled
```
You could also replace the command that saves the log folder in a zip file by a command that launches VS Code from the Logs folder.

```PowerShell
    # Opening Log folder in VS Code
    Write-Host "Opening VS Code for Log Analysis"
    & Set-Location "~\AppData\Local\National Instruments\NI Package Manager\Logs\"
    & code .
```

7. Hover your mouse over the commandlets to learn about their structure and parameters. 
8. Put the cursor in one of the commands (e.g., **Write-Host**), then Press `Ctrl-F1` to open the complete on-line help for it.

>> Note: The Command Explorer offers a list of all available commandlets in alphabetical order. Click the PowerShell button on the **Activity Bar** (vertical bar to the left) to access it.

9. Click the Run Button. See that the script runs in the embedded PowerShell terminal at the bottom of VS Code UI.

10. Check that the logs were saved to *C:\NIPM_LOGS\NIPM_All_logs.zip*

### Create a launch.json to Debug the Script When Passing Arguments.

VS Code allows you to create a specific debug configuration that calls your script passing specific arguments. The following steps describe how could you use an input argument to define whether to zip the logs or to launch VS code for local analysis.

1. With **enable-all-nipm-logs.ps1** open, click the Run and Debug `(Ctrl+Shift+D)` button and click on the **create a launch.json file** link.

2. Select the **Launch Current File** configuration. See that a **.vscode** folder and a **launch.json** file are created within the workspace.

3. Open **launch.json** and add the `local_analysis` in the args array. The configuration sould look like this:

```JSON
    {
            "name": "PowerShell: Launch Current File",
            "type": "PowerShell",
            "request": "launch",
            "script": "${file}",
            "args": ["local_analysis"]
        }
```
4. back to the script file, modify it to conditionally choose what to do with the logs based on the input argument.

```PowerShell
if ($($args[0]) -eq "local_analysis") {
    # Opening Log folder in VS Code
    Write-Host "Opening VS Code for Log Analysis"
    & Set-Location "~\AppData\Local\National Instruments\NI Package Manager\Logs\"
    & code .
} else{
    # Capture NIPM logs and store them in a specified directory as a Zip file that can be easly shared.
    Write-Host "Capturing Logs..."
    & mkdir "C:\NIPM_LOGS" -Force # Create a directory to store the logs.
} 
```

5. Click the Start Debugging `(F5)` to start debugging. See that now a new instance of VS Code is launched with the NIPM **Logs** folder as the current workspace.

6. Delete the `local_analysis` from **launch.json** and restart debugging. See that now the .zip file is generated.

### ISE Mode

For those familiar with the Power ISE (Integrated Scripting Environment), VS Code offers an ISE Mode that makes the transition smoother and provides a more familiar experience.

Explore the [How to replicate the ISE experience in Visual Studio Code](https://learn.microsoft.com/en-us/powershell/scripting/dev-cross-plat/vscode/how-to-replicate-the-ise-experience-in-vscode?view=powershell-7.3) article for detailed information on replicating the ISE experience within Visual Studio Code.

### Troubleshooting

If there are common issues or challenges that readers might encounter, list them here along with possible solutions or workarounds.

- **Issue 1:** Description of the issue.
    - Solution or workaround.

- **Issue 2:** Description of the issue.
    - Solution or workaround.



## Conclusion

In this guide, we've explored the power of PowerShell scripting in combination with Visual Studio Code for both development and debugging. Automation not only saves time but also ensures consistency in your tasks. By creating scripts to streamline processes like log capturing in NI Package Manager troubleshooting, you can assist others efficiently. Moreover, we've learned how to enhance the debugging process by creating a launch.json configuration to pass arguments to our scripts. Lastly, for users familiar with Power ISE, we've touched on how Visual Studio Code offers an ISE Mode for a more comfortable transition. With these skills, you're well-equipped to harness the potential of PowerShell for your automation needs.

## Additional Resources

List any additional resources, references, or links that readers might find useful or interesting.

- [Automate administrative tasks by using PowerShell
](https://learn.microsoft.com/en-us/training/paths/powershell/)
- [PowerShell in Visual Studio Code](https://code.visualstudio.com/docs/languages/powershell) - VS Code Docs
- [Accessing the Command Line Interface for Package Manager](https://www.ni.com/docs/en-US/bundle/package-manager/page/cli-package-manager.html) - NI Package Manager Help


**Feedback:** Help us improve this tutorial. Please provide feedback, report issues, or suggest enhancements. :smiley:

**Author:** Felipe Flores, Senior Technical Support Engineer at NI.

**Last Updated:** August 21st, 2023.

