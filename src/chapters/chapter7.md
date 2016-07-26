## Kendo UI Datasource

In this chapter you'll learn how to work with Kendo UI datasources.

### Working with the Kendo UI Datasource

The <a href="http://demos.telerik.com/kendo-ui/datasource/index">Kendo UI DataSource component</a> plays a central role in practically all web applications built with Kendo UI. It is an abstraction for using local data—arrays of JavaScript objects—or remote data—web services returning JSON, JSONP, <a href="http://www.odata.org/">OData</a> or XML.

The Kendo UI DataSource has many abilities and responsibilities, among which to:

- <a href="http://docs.telerik.com/kendo-ui/framework/datasource/cors">Retrieve data from a remote endpoint</a>.
- Maintain the <a href="http://docs.telerik.com/kendo-ui/framework/datasource/crud#schema">structure and type of the data (schema)</a>.
- Process serialization formats to and from a remote endpoint.
- <a href="http://docs.telerik.com/kendo-ui/framework/datasource/crud">synchronize updates—create, update, delete</a> to and from a remote endpoint.
- <a href="http://docs.telerik.com/kendo-ui/framework/datasource/offline">Maintain an in-memory cache of data, including changes</a> for updating to a remote endpoint.
- Calculate and maintain <a href="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#methods-aggregate">aggregates</a>, <a href="http://demos.telerik.com/kendo-ui/api/javascript/data/datasource#methods-sort">sorting order</a> and <a href="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#methods-page">paging</a>.
- Provide a query mechanism via <a href="http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#methods-filter">filter expressions</a>.

For detailed information on the capabilities of the [DataSource](http://docs.telerik.com/kendo-ui/framework/datasource/overview), refer to its <a href="http://demos.telerik.com/kendo-ui/api/javascript/data/datasource">configuration API, methods, and events</a>, and <a href="http://demos.telerik.com/kendo-ui/datasource/index">demos</a>.

At this point the dashboard is showing all invoice data. Let's use the EmployeeList list view and StatsFrom/StatsTo date pickers to filter the invoice grid by invoking the grid's datasource.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a filter.
</h4>

In the view `/Views/Home/Index.cshtml` **find** the scripts section.

	<script>
		...
    </script>

**Add** a function named `getEmployeeFilter` that gets the `employeeId`, `salesPerson`, `statsFrom` and `statsTo` values and returns a JSON object.

The resulting code should be:

    function getEmployeeFilter() {
        var employee = getSelectedEmployee(),
            statsFrom = $("#StatsFrom").data("kendoDatePicker"),
            statsTo = $("#StatsTo").data("kendoDatePicker");

        var filter = {
            employeeId: employee.EmployeeId,
            salesPerson: employee.FullName,
            statsFrom: statsFrom.value(),
            statsTo: statsTo.value()
        }
        return filter;
    }

In the view `/Views/Invoice/Index.cshtml` **find** the EmployeeSales grid.    

	@(Html.Kendo().Grid<KendoQsBoilerplate.Invoice>()
	      .Name("EmployeeSales")
		  ...
	      .Scrollable(scrollable => scrollable.Enabled(false))
	      .DataSource(dataSource => dataSource
	          .Ajax()
	          .Read(read => read.Action("Invoices_Read", "Invoice"))
	      )
	)

On the grid's `DataSource` property, **set** the `Data` property to `getEmployeeFilter`. The `Data` property supplies additional data to the server, in this case the data is our filter parameters.

    .DataSource(dataSource => dataSource
                .Ajax()
                .Read(read => read.Action("Invoices_Read", "Invoice")
                .Data("getEmployeeFilter"))
    )

**Add** the property `AutoBind` to the end of the property chain and set the value to `false`. Setting AutoBind to false tells UI for MVC that the datasource's `Read` action is invoked on manually on the client.

The resulting code should be:

	@(Html.Kendo().Grid<KendoQsBoilerplate.Invoice>()
	      .Name("EmployeeSales")
	      ...
	      .Scrollable(scrollable => scrollable.Enabled(false))
	      .DataSource(dataSource => dataSource
	          .Ajax()
	          .Read(read => read.Action("Invoices_Read", "Invoice")
	          .Data("getEmployeeFilter"))
	      )
		  .AutoBind(false)
	)

In the view `/Views/Home/Index.cshtml` **add** a function named `refreshGrid`, this function will invoke the grid's `Read` action.

	function refreshGrid() {
        var employeeSales = $("#EmployeeSales").data("kendoGrid");
        employeeSales.dataSource.read();
    }

**Find** the function `onCriteriaChange` and **add** a call to the `refreshGrid` function. This will cause the Grid's data to refresh whenever the employee selection changes.

	function onCriteriaChange() {
        updateEmployeeAvatar();
        refreshGrid();
	}

Next, we'll need to update the grid's `Read` action to apply the filter using Entity Framework.

**Open** `Controllers/InvoiceController.cs` and find the `Invoices_Read` action.

    public ActionResult Invoices_Read([DataSourceRequest]DataSourceRequest request)
    {
        IQueryable<Invoice> invoices = db.Invoices;
        DataSourceResult result = invoices.ToDataSourceResult(request, invoice => new {
            OrderID = invoice.OrderID,
            CustomerName = invoice.CustomerName,
            OrderDate = invoice.OrderDate,
            ProductName = invoice.ProductName,
            UnitPrice = invoice.UnitPrice,
            Quantity = invoice.Quantity,
            Salesperson = invoice.Salesperson
        });

        return Json(result);
    }

**Add** the parameters `salesPerson`, `statsFrom` and `statsTo` to the action.

    public ActionResult Invoices_Read([DataSourceRequest]DataSourceRequest request,
        string salesPerson,
        DateTime statsFrom,
        DateTime statsTo)

Using the parameter values filter the invoices using a `Where` LINQ query.

The resulting code should be:

    public ActionResult Invoices_Read([DataSourceRequest]DataSourceRequest request,
        string salesPerson,
        DateTime statsFrom,
        DateTime statsTo)
    {
        var invoices = db.Invoices.Where(inv => inv.Salesperson == salesPerson)
            .Where(inv => inv.OrderDate >= statsFrom && inv.OrderDate <= statsTo);
        DataSourceResult result = invoices.ToDataSourceResult(request, invoice => new {
            OrderID = invoice.OrderID,
            CustomerName = invoice.CustomerName,
            OrderDate = invoice.OrderDate,
            ProductName = invoice.ProductName,
            UnitPrice = invoice.UnitPrice,
            Quantity = invoice.Quantity,
            Salesperson = invoice.Salesperson
        });

        return Json(result);
    }

**Run** the project to see the behavior. Now the EmployeeList and EmployeeSales grid are in sync. When an employee is selected, only that employees data will show in the grid.

![](images/chapter7/datasource-filter.jpg)

<div class="exercise-end"></div>

At this point EmployeeList is acting as a filter for EmployeeSales, however the data shown does not reflect the StatsFrom/StatsTo date range. With the filtering code in place, additional controls are wired up with relative ease. Let's wire up the StatsFrom/StatsTo DatePickers to EmployeeSales.

<h4 class="exercise-start">
    <b>Exercise</b>: Trigger the grid datasource from a DatePicker event.
</h4>

In the view `/Views/Home/Index.cshtml` **find** the StatsFrom DatePicker.

    @(Html.Kendo().DatePicker()
                    .Name("StatsFrom")
                    .Value(new DateTime(1996, 1, 1))
	)

**Add** the `Events` property and **set** the `Change` event to `onCriteriaChange`.

    @(Html.Kendo().DatePicker()
                    .Name("StatsFrom")
                    .Value(new DateTime(1996, 1, 1))
                    .Events(e => e.Change("onCriteriaChange"))
	)

**Find** the StatsTo DatePicker and **set** the `Events` property and set the `Change` event to `onCriteriaChange`.

    @(Html.Kendo().DatePicker()
			        .Name("StatsTo")
			        .Value(new DateTime(1998, 8, 1))
			        .Events(e => e.Change("onCriteriaChange"))
	)

**Save** the changes and **refresh** the browser. StatsFrom/StatsTo and EmployeeList will update EmployeeSales with data based on the selected dates and employee.

![](images/chapter7/datasource-filter2.jpg)

<div class="exercise-end"></div>

Your Team Efficiency Dashboard is now interactive, users can filter data using dates and employees. Next, you'll enhance the application by adding some data visualizations.
