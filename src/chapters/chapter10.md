## Kendo UI Themes

Kendo UI widgets include a number of predefined themes. In this chapter you'll learn how to make your app look amazing using Kendo UI themes.

### Theme Change

<h4 class="exercise-start">
    <b>Exercise</b>: Theme the application.
</h4>

If the project is running, **stop** the project.

In Visual Studio's Project Explorer, **Right click** on the project and choose **Telerik UI For MVC > Configure Project** from the menu.

From the Project Configuration Wizard, **choose** the Nova theme.

**Open** `Views/Shared/_Layout.cshtml` and move `@Styles.Render("~/Content/css")` just above the closing head tag `</head>`.

**Run** the application to see the theme applied to the Kendo UI widgets.

Next, you'll be finishing the theme by adding styles to non-Kendo UI elements creating a completely custom look.

A style sheet was installed with the boilerplate to give you a jump-start. **Add** it to the application by opening `Views/Shared/_Layout.cshtml` and adding a reference to `~/Content/site-nova.css` just above the closing head tag `</head>`.

Note: This is CSS, so the order in which the style sheets are added is very important.

    <link href="~/Content/site-nova.css" rel="stylesheet" />
	</head>

**Refresh** the application and notice the look is starting to come together. There's just a few items that could use some fine-tuning. Let's add some additional styles to `site-nova.css` to complete the theme.

**Open** site-nova.css and **find** `/* Side Panel - Employee List */`. **Add** a style that sets the date picker widgets inside the menuPanel to %100 width of the container.

The resulting code should be:

	/* Side Panel - Employee List */
	#menuPanel .k-widget.k-datepicker.k-header {
	    width: 100%;
	}

![Date Picker Width](images/chapter10/datepicker-width.jpg)

**Add** a style to offset the employee list so its content lines up with the left edge of its container.

	#employee-list > ul {
    	margin: 0 -10px;
	}

![Date Picker Width](images/chapter10/list-view-container.jpg)

**Find** `/* Small Devices, Tablets, and Up */`. Here you'll find a media query that will hold some styles that are only applied to scree sizes above `768px`.

	@media only screen and (min-width : 768px) {

	}

Inside the media query **add** a selector for `.app-wrapper` and **set** a left margin of `-15` and **set** the `position` to `relative`. This style will align the app with the left hand edge of the screen.

	/* Small Devices, Tablets, and Up */
	@media only screen and (min-width : 768px) {
	    .app-wrapper {
	        position: relative;
	        margin-left: -15px;
	    }
	}

![App Wrapper margin](images/chapter10/app-wrapper.jpg)

Finally, set the Kendo UI Chart themes.

**Open** `_MontlySalesByEmployee.cshtml` and **set** the `Theme` property to `nova` on the `EmployeeAverageSales` chart.

	@(Html.Kendo().Chart<KendoQsBoilerplate.MonthlySalesByEmployeeViewModel>()
        .Name("EmployeeAverageSales")
        ...
        .AutoBind(false)
       	.Events(e => e.DataBound("onAverageSalesDataBound"))
        .Theme("nova")
	)

**Open** `_QuarterToDateSales.cshtml` and **set** the `Theme` property to `nova` on the `EmployeeQuarterSales` chart.

    @(Html.Kendo().Chart<KendoQsBoilerplate.QuarterToDateSalesViewModel>()
        .Name("EmployeeQuarterSales")
        ...
	    .AutoBind(false)
        .Events(e => e.DataBound("onQuarterSalesDataBound"))
        .Theme("nova")
	)

<div class="exercise-end"></div>

And... that's it! You've created an interactive dashboard application using Telerik UI for MVC and Kendo UI. In the process you've mastered scaffolding, Kendo UI templates, charts, server and client-side APIs, responsive web design and themes.

Congratulations!
