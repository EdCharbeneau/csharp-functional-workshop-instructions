## Getting Up and Running

In this chapter you're going to start with the basics, including starting a new project, adding Telerik UI for MVC to your project, and installing the quick start boilerplate.

### Create a New MVC Project

UI for ASP.NET MVC can easily be added to an existing ASP.NET MVC project in just a few clicks using VisualStudio.

Start by creating a new ASP.NET MVC project. You'll use this project throughout the rest of this tutorial to build your application.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a new MVC project    
</h4>

Click **File > New Project**


In the New Project dialog choose the <b>ASP.NET Web Application</b> template by expanding the "Templates" tree to <b>Templates > Visual C# > Web</b>

![](images/chapter1/file-new-mvc-project.jpg)

Give the application a name (ex: MyQuickStartApp)

Click <b>OK</b> to continue

In the <b>New ASP.NET Project</b> dialog, choose MVC from the 4.6 template selection

![](images/chapter1/file-new-mvc-project2.jpg)

Click <b>OK</b> to finish

<div class="exercise-end"></div>

### Install the Quick Start Boilerplate

With the new project created, it's time to start building your app. For this guide, we've scaffolded out a boilerplate project to act as a starting point for the Team Efficiency Dashboard.

The boilerplate has an HTML page, a layout, the Northwind database and some server-side code you may find in a typical MVC project.

<h4 class="exercise-start">
    <b>Exercise</b>: Install the quick start boilerplate    
</h4>

Using the package manager console, run the following command

    PM> Install-Package KendoQsBoilerplate

Alternatively, you can use the package manager GUI

From the Solution Explorer **right-click References**, then choose **Manage NuGet Packages**

![](images/chapter1/nuget-gui.jpg)

Search for **KendoQsBoilerplate**

![](images/chapter1/nuget-gui2.jpg)

**Click Install** to continue

When the package installs you may be prompted to accept a license agreement for the NortwindDB, click **I Accept** to continue

It is normal for the quick start boilerplate to overwrite existing files, when prompted with a file conflict choose **Yes to All**

![](images/chapter1/file-conflict.jpg)

<div class="exercise-end"></div>

With the boilerplate installed, take a moment to run the application. If all went well, you should see something like this:

![](images/chapter1/wire-frame.jpg)

### Convert to Telerik Application

At this point, you have the wire frame for a basic MVC application. Next you will be adding the UI for ASP.NET MVC to the application by using the **Convert to Telerik Application** tooling. When an application is converted to a Telerik application, all required HTML, CSS, JavaScript and .DLL libraries are added. This is the first step you would take to upgrade a new or existing MVC project to use Telerik UI for ASP.NET MVC.

<h4 class="exercise-start">
    <b>Exercise</b>: Convert to a Telerik Application
</h4>

**Stop the application** if it is already running.

In the Solution Explorer **right-click** the project name and select **Telerik UI for ASP.NET MVC > Convert to Telerik Application**. This will launch the Project Configuration Wizard, from here you can choose settings for your Telerik project.

![](images/chapter1/convert-to-telerik1.jpg)

For this tutorial your project will use CDN support. This means all Kendo UI resources are served from Telerik's content delivery network (CDN) versus relying on your server for the assets. Mark the box **Use CDN support** and click **Next** to continue.

![](images/chapter1/convert-to-telerik2.jpg)

Since the boilerplate is designed with [Bootstrap](http://getbootstrap.com), **choose Bootstrap** from themes select box so the theme matches the current look of the boilerplate. You'll change the theme later when you're ready to customize the look of the application.

![](images/chapter1/convert-to-telerik3.jpg)

Open **\Views\Shared\_Layout.cshtml** find and remove the following script bundle `@Scripts.Render("~/bundles/modernizr")`. This script is included with the Kendo UI assets.

Next, find the CSS bundle `@Styles.Render("~/Content/css")` and **move** it just above the closing head tag `</head>` this will make sure that any custom styles are applied when you customize the application.

The final code of the head section should look like this:

        <meta charset="utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0">

        <title>@ViewBag.Title - UI for MVC / Kendo Quick Start Guide</title>

        <link href="http://cdn.kendostatic.com/2015.3.1111/styles/kendo.common-bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="http://cdn.kendostatic.com/2015.3.1111/styles/kendo.mobile.all.min.css" rel="stylesheet" type="text/css" />
        <link href="http://cdn.kendostatic.com/2015.3.1111/styles/kendo.dataviz.min.css" rel="stylesheet" type="text/css" />
        <link href="http://cdn.kendostatic.com/2015.3.1111/styles/kendo.bootstrap.min.css" rel="stylesheet" type="text/css" />
        <link href="http://cdn.kendostatic.com/2015.3.1111/styles/kendo.dataviz.bootstrap.min.css" rel="stylesheet" type="text/css" />
        <script src="http://cdn.kendostatic.com/2015.3.1111/js/jquery.min.js"></script>
        <script src="http://cdn.kendostatic.com/2015.3.1111/js/jszip.min.js"></script>
        <script src="http://cdn.kendostatic.com/2015.3.1111/js/kendo.all.min.js"></script>
        <script src="http://cdn.kendostatic.com/2015.3.1111/js/kendo.aspnetmvc.min.js"></script>
        <script src="@Url.Content("~/Scripts/kendo.modernizr.custom.js")"></script>
        @Styles.Render("~/Content/css")

> **Tip**: Because the **Convert to Telerik application**, **Upgrade Project**, or **Configure Project** wizards modify the `_Layout.cshtml` file, be sure to check position of any custom CSS declarations afterward.        

<div class="exercise-end"></div>

Now that your app is ready for development, let's add some simple input components to create a nice user experience.
