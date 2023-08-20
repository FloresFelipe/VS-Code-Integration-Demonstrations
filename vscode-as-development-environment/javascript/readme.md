# Developing and Debugging JavaScript Code with VS Code

## Goal

G Web Development Software, the NI tool for developing HTML 5 based front-panels, allows for integration with JavaScript for integrating custom functionality into the WebVIs. Through the JavaScript Library Integration, you can call JS functions in the form of SubVIs, that can be grabbed from the functions palette.

G Web also lets you add custom Cascade Style Sheet (CSS) files to customize the look of UI elements.

In this tutorial, we're going to use VS code to create and edit both CSS and JavaScript files that are used by a G Web Dev Project. In addition to that, let's use the Live Server to get live updates of your edits to the Javascript code.

## Prerequisites

List any prerequisites or requirements that readers should have before starting the tutorial.

- Install [Visual Studio Code](link_to_installation_page).
- Install [JavaScript] (link_to_extension_page) extension.
- Install [Live Server](lint_to_extension_page) exension.
- ...

> **Note:** Additional notes or information that might be relevant to the prerequisites can be mentioned here.

## Index

Provide a list of sections or topics covered in your document. Include links to the corresponding sections for easy navigation.

- [Section 1: Title](#section-1-title)
- [Section 2: Title](#section-2-title)
- ...

---

### Customizing WebVI Control using CSS and JavaScript

In G Web Dev, you can customize every part of a UI control by using CSS and modifying it at run-time using JavaScript. In this tutorial, we're going to customize a Gauge control to change the needle, line, and fill colors according to a specific thresholds. In VS Code, we're going to create a Javascript function that takes lower limit, upper limit and current value paramenters and set the color of the CSS element dynamically.

1. Open G Web Development Software.
2. Create a new project and name it `customizing_web_control`
2. Open the **index.gviweb** and add a Gauge control to the Front Panel.
3. Open the properties pane of the control and enter `custom-gauge` in the **Custom HTML Component** field, and Select `Fill` for the **Display Type**.
4. Set the **minimum value** to `0` and the **maximum value** to `10`.
5. Create the following block diagram to set the value of the Gauge.

[WebVI block diagram here]

6. Crete two new namespaces in the gcomp: **css** and **js**. Your project should look like this.

[Project Snapshot here.]

7. Open the gcomp folder by right-clicking it and selecting **Locate in Explorer**.

8. Right-Click the folder and select **Open Folder in VS Code**.

You're going to use VS Code from now on to create and edit the CSS and the Javascript code.

9. Create a new file and save it in the **css** folder as `custom-styles.css`

10. Paste the following snippet on the newly created CSS file.

[CSS Snippet]

11. Create a new file and save it in the **js** folder as `change-color.css`

12. Paste the following snippet on the newly created Javascript file.

[Javascript Snippet]

13. Still in VS Code, open index.gviweb. See that it is displayed as an HTML file. Copy and paste the following HTML snippet

> **Note:** with this step, we're linking both the CSS and the Javascript files to the Web VI.

14. Save the file.

15. Back in G Web Dev, notice that it did not include the files we created in VS Code automatically, so make sure you import them (Right-Click >> Import Files). Your project should now look like this.

[project snippet 2]

16. Create a new Java Script Library under gcomp (Right-Click >> New >> Javascript Library). Name it `custom_control_lib`.

17. Open cusotm_control_lib and add a new function called `change_gauge_color`. Set the inputs and outputs as shown in the table below:

[ table of inputs and outputs]

> **Note:** You can define a custom icon if you want.

18. Open the WebVI block diagram, and on the functions palette, select **Software >> custom_control_lib >> change_gauge_color**.

19. Add it to your block diagram. Connect two numeric controls to lower and upper and add get the same input as the Gauge indicator as the current_value. Your Block Diagram and Front Panel should be similar to the screenshots below.

[Block Diagram Screenshot]

[Front Panel Screenshot]

20. Set **lower threshold** to `3` and **upper threshold** to `8`. 

21. Run the Web VI. Notice how the line color changes depending on the current value.

[GIF reproducing the VI running]

### Using VS Code to Experiment Control Customizations

While G Web Dev doesn't feature live updates while customizing css and javascript files, we can use the Live Server Extension to publish the WebVI and update the UI as you modify the Javascript code.

1. Open the project folder in VS Code if you don't have it open already.

2. Open the index.html. Then, click the **Go Live** button on the lower status bar. VS Code will load the live server and open the WebVI in your default brownser.

3. In VS Code, change the color constants and save the file. See how the colors of the gauge are automatically updated. To stop the live server, click on **Live on Port: 5000**.

**Challenge**: If you set the Display Type to Needle, the css element that carry the color attribute will be `.jqx-needle`. Modify the project files to change the color of the needle.

## Troubleshooting

If there are common issues or challenges that readers might encounter, list them here along with possible solutions or workarounds.

- **Issue 1:** Description of the issue.
    - Solution or workaround.

- **Issue 2:** Description of the issue.
    - Solution or workaround.

---

## Conclusion

VS Code can be used to support the stramline the workflow of customizing UI Controls in G Web Dev. Also, the Live Server extension comes in handy when you want to experiment with attributes you're modifying by automatically updating the Web Page as soon as you save the project files. 


## Additional Resources

List any additional resources, references, or links that readers might find useful or interesting.

- [Using JavaScript with a Web Application](https://www.ni.com/docs/en-US/bundle/g-web-development/page/javascript-web-application.html)
- [JavaScript in Visual Studio Code](https://code.visualstudio.com/docs/languages/javascript)
- [Customize WebVIs with CSS](https://ni.github.io/webvi-examples/CustomizeWithCss/Builds/WebApp_Default%20Web%20Server/)
- [Customizing the Appearance of Controls in a WebVI](https://www.ni.com/docs/en-US/bundle/g-web-development/page/customizing-appearance-controls-webvi.html)

---

**Feedback:** Help us improve this tutorial. Please provide feedback, report issues, or suggest enhancements. :smiley:

**Author:** Felipe Flores, Senior Technical Support Engineer at NI.

**Last Updated:** August 15th, 2023.
