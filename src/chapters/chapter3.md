## Scaffolding

In this chapter, you'll learn how to add leverage Telerik UI for MVC's scaffolding capabilities. One feature that MVC developers are quite used to is scaffolding. Visual-Studio-powered MVC scaffolding is a code generation framework that allows you to hook up your model to a controller and render views that are strongly typed, among other things. Since the scaffolding is simply a code generation tool, you are free to change any of the code that it generated.

### Upgrade the Database

A copy of the Northwind database is included with the Kendo UI Quick Start Boilerplate. Before you begin scaffolding make sure the Northwind database is upgraded.  Having a working connection to the database is needed for the scaffolding wizard to work properly.

> Note: Upgrading the database is only necessary for this guide because the database supplied must support multiple versions of SQL therefore we chose the lowest database version possible.

<h4 class="exercise-start">
    <b>Exercise</b>: Upgrade the Northwind Database
</h4>

> Note: If you do not have an SQL Server instance installed on your machine, you may need to install SQL Server Express Edition from Microsoft. You can download the free installer [here](http://www.microsoft.com/en-us/server-cloud/products/sql-server-editions/sql-server-express.aspx).

Using Visual Studio's **Server Explorer**, expand **DataConnections** and **right-click NorthwindDB > Modify Connection**.

![](images/chapter3/upgrade-db-1.jpg)

Next, **click OK**.

![](images/chapter3/upgrade-db-2.jpg)

Finally, **click Yes** to complete the upgrade.

![](images/chapter3/upgrade-db-3.jpg)

Once the upgrade is complete, **expand** the Northwind Database Tables to verify connectivity.

![](images/chapter3/upgrade-db-4.jpg)

<div class="exercise-end"></div>

With the database upgraded use the scaffolding wizard to create an interactive grid view.

### UI for MVC Scaffolding Wizard

The scaffolding wizard will aid you in creating the view by providing point a click configuration screen. Use the scaffolding wizard to create an interactive Kendo UI Grid view of invoices for the Team Efficiency Dashboard. By enabling grid features like: sorting, paging and exporting users will be able to analyze and share data in a familiar way.

<h4 class="exercise-start">
    <b>Exercise</b>: Scaffold a grid view of invoices
</h4>

Start the scaffolding wizard by **right-clicking Controllers > Add > New Scaffolded Item**

![](images/chapter3/scaffold-1.jpg)

Choose the **Kendo UI Scaffolder** and click **Add** to continue

![](images/chapter3/scaffold-2.jpg)

Notice the Scaffolder is capable of creating Grid, Chart, and Scheduler views for both C# and JavaScript. For this guide you'll be using the UI for MVC Grid scaffolding option. Choose **UI for MVC Grid** and click **Add** to continue.

![](images/chapter3/scaffold-3.jpg)

From MVC Grid scaffolding dialog, the grid's model options, grid options and events are defined. The Model Options control the following settings:

- *Controller Name* - The name of the controller created by the Scaffolder.
- *View Name* - The name of the view created, which will display the scaffolded grid.
- *Model Class* - The model the Scaffolder will use to build the view.
- *Data Context Class* - The Entity Framework DbContext used to connect the view to the data.

Define the grid's model options using the following values:

- Controller Name: **InvoiceController**
- View Name: **Index**
- Model Class: **Invoice**
- Data Context Class: **NorthwindDBContext**

![](images/chapter3/scaffold-4.jpg)

The Grid Options control what features are scaffolded & enabled on the grid including:

- *DataSource* Type - Ajax, Server or WebApi.
- *Editable* - Enable the editing, configure the edit mode (InLine, InCell or PopUp) and the operations to be included (Create, Update, Destroy).
- *Filterable* - Enable the filtering of the grid and select the filter mode.
- *Column Menu* - Enable the column menu.
- *Navigatable* - Enable the keyboard navigation.
- *Pageable* - Enable the paging of the grid.
- *Reorderable* - Enable the column reordering.
- *Scrollable* - Enable the scrolling of the grid table.
- *Selectable* - Enable the selection and specify the selection mode and type.
- *Sortable* - Enable the sorting and specify the sorting mode.
- *Excel Export* - Enable the Excel export functionality.
- *PDF Export* - Enable the PDF export functionality.

Define the grid's options by setting the following values:

- *unchecked* Scrollable
- *checked* Sortable
- *checked* Pageable
- *checked* Excel Export
- *checked* PDF Export

![](images/chapter3/scaffold-5.jpg)

Click **Add** to continue and create the scaffolded items.

The Scaffolder will create the following files:

- `Controllers/InvoiceController.cs` - This controller has the actions for the features selected in the scaffolding wizard.
    - `Index` returns the view
    - `Invoices_Read` - gets all invoices from the database and returns a JSON formatted *DataSourceRequest* object. The *DataSourceRequest* will contain the current grid request information - page, sort, group and filter.
    - `Excel_Export_Save` - creates an XLS exported File result.
    - `Pdf_Export_Save` - creates a PDF exported File result.
- `Views/Invoice/Index.cshtml` - This view contains the markup and HTML helper responsible for rendering the grid control.

**Run** the application and navigate to `/Invoice/index` to see the generated grid control. You should see the following output:

![](images/chapter3/invoices-grid.jpg)

<div class="exercise-end"></div>

Now that the UI for MVC Scaffolder has generated a starting point for working with the grid, you can modify the scaffolded code to meet your needs. In the next chapter we'll do just that.
