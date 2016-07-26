## Kendo UI Charts

In this chapter you'll learn how to add Kendo UI Chart widgets to your application. The Telerik ASP.NET MVC chart, powered by Kendo UI, is a powerful data visualization component, which allows you to graphically represent your data. It is useful when you would like to utilize modern browser technologies such as SVG or Canvas (with a fallback to VML for older IE versions) for interactive data visualizations.

The component offers a variety of chart types such as area, bar, line, scatter, polar, radar, pie and donut, stock (OHLC) and many more.

### Chart API

The Chart HtmlHelper extension is a server-side wrapper for the Kendo UI Chart widget.

Example:

    @(Html.Kendo().Chart(Model) // The chart will be bound to the Model which is the InternetUsers list
        .Name("internetUsersChart") // The name of the chart is mandatory. It specifies the "id" attribute of the widget.
        .Title("Internet Users")
        .Series(series => {
            series.Bar(model => model.Value) // Create a bar chart series bound to the "Value" property
                    .Name("United States");
        })
        .CategoryAxis(axis => axis
            .Categories(model => model.Year)
        )
    )

### Bullet Series Chart

Begin by adding a Bullet chart, a variation of a bar chart. Bullet charts make great dashboard gauges or meters. The bullet graph compares a given quantitative measure against qualitative ranges and a symbol marker.

<h4 class="exercise-start">
    <b>Exercise</b>: Add a bullet series chart.
</h4>

Since changes to the controller are necessary, **stop** the application if it is running.

Use partials to keep the markup tidy. Under 'Views/Home/' **add** a new empty partial view '_QuarterToDateSales.cshtml'

In the new partial view `_QuarterToDateSales.cshtml` **add** a new Kendo UI Chart helper of type `QuarterToDateSalesViewModel`. The `QuarterToDateSalesViewModel` is part of the quick start bolierplate.

	@(Html.Kendo().Chart<KendoQsBoilerplate.QuarterToDateSalesViewModel>()

    )

**Set** the `Name` property to `EmployeeAverageSales`.

    .Name("EmployeeQuarterSales")

Using the `.HtmlAttributes` property **Set** the controls height to `30px`.

    .HtmlAttributes(new { style = "height:30px;" })

Next, **add** and define a `Bullet` chart with the following properties.

- **Set** the current value to the `Current` property on the model.
- **Set** the target value to the `Target` property on the model.


    .Series(series =>
    {
        series.Bullet(model => model.Current, model => m.Target);
    })

Next, **add** and configure the Category Axis. Since the chart will be a spark line visualization, set these properties to `false`


- Visible
- MajorGridLines


     .CategoryAxis(ca => ca.Labels(lab => lab.Visible(false))
         .MajorGridLines(m => m.Visible(false)).Visible(false)
     )

Next, **add** and configure the ValueAxis with a Numeric configuration.

Since the chart will be a spark line visualization, set these `Visible` properties to `false` to disable them.

- Labels
- MajorGridLines
- MajorTicks


     .ValueAxis(va => va.Numeric()
         .Labels(lab => lab.Visible(false))
         .MajorGridLines(m => m.Visible(false))
         .MajorTicks(mT => mT.Visible(false))
     )

Also **set** the `Legend` to `false`    

    .Legend(leg => leg.Visible(false))

**Configure** the `DataSource` by setting `Read` to the action `EmployeeQuarterSales` on the `Home` controller.

Using the `Data` property, **set** the value to `getEmployeeFilter` sending filter data back to the `Read` action.

Since the DataSource will be invoked on manually, **set** `AutoBind` to `false`

    .AutoBind(false)

The resulting code should be:

    @(Html.Kendo().Chart<KendoQsBoilerplate.QuarterToDateSalesViewModel>()
        .Name("EmployeeQuarterSales")
        .HtmlAttributes(new { style = "height:30px;" })
        .Series(series =>
        {
            series.Bullet(m => m.Current, m => m.Target);
        })
        .CategoryAxis(ca => ca.Labels(lab => lab.Visible(false))
            .MajorGridLines(m => m.Visible(false)).Visible(false)
        )
        .ValueAxis(va => va.Numeric()
            .Labels(lab => lab.Visible(false))
            .MajorGridLines(m => m.Visible(false))
        )
        .Legend(leg => leg.Visible(false))
        .DataSource(ds => ds
            .Read(read => read.Action("EmployeeQuarterSales", "Home")
            .Data("getEmployeeFilter"))
        )
        .AutoBind(false)
    )

**Open** `controllers/HomeController.cs` and **create** a controller action named `EmployeeAverageSales` on the `Home` controller. This action will supply the Chart with data.

The boilerplate installed in Chapter 1 has a function named `EmployeeQuarterSales`, this query will select the data required for the chart. **Return** the results of `EmployeeQuarterSalesQuery` as JSON.

	public ActionResult EmployeeQuarterSales(int employeeId, DateTime statsTo)
    {
        DateTime startDate = statsTo.AddMonths(-3);

        var result = EmployeeQuarterSalesQuery(employeeId, statsTo, startDate);

        return Json(result, JsonRequestBehavior.AllowGet);
    }

**Add** the partial view to the main application page.

In `Views/Home/Index.cshtm` **find** the `<!-- QTD Sales Chart -->` placeholder.

	<!-- QTD Sales Chart -->
	@Html.Placehold(430, 120, "Chart")

**Replace** the placeholder with the `_QuarterToDateSales` partial.

	<!-- QTD Sales Chart -->
    @Html.Partial("_QuarterToDateSales")

**Find** the scripts section.

	<script>
		...
	</script>

**Add** a new function named `refreshEmployeeQuarterSales`, this function will invoke `read` on the chart's DataSource.

The resulting code should be:

    function refreshEmployeeQuarterSales() {
        var employeeQuarterSales = $("#EmployeeQuarterSales").data("kendoChart");
        employeeQuarterSales.dataSource.read();
    }

**Find and modify** the `onCriteriaChanged` function so it calls `refreshGrid` updating the entire dashboard when a filter is changed.

    function onCriteriaChange() {
        updateEmployeeAvatar();
        refreshGrid();
        refreshEmployeeQuarterSales();
    }

**Run** the application to see the chart render on the dashboard. Change the filter criteria to see the chart update along with other UI elements.

![Bullet Chart](images/chapter8/bullet-chart.jpg)

<div class="exercise-end"></div>

### Line Chart

Next, add a Line chart, a Line chart shows data as continuous lines that pass through points defined by their items' values. It can be useful for showing a trend over time and comparing several sets of similar data. For this example, you'll use a Line chart to show trend data.

<h4 class="exercise-start">
    <b>Exercise</b>: Trigger the grid datasource from a DatePicker event.
</h4>

Since changes to the controller are necessary, **stop** the application if it is running.

Use partials to keep the markup tidy. Under 'Views/Home' **add** a new empty partial view '_MonthlySalesByEmployee.cshtml'

In the new partial view `_MonthlySalesByEmployee.cshtml` **add** a new Kendo UI Chart helper.

	@(Html.Kendo().Chart<KendoQsBoilerplate.MonthlySalesByEmployeeViewModel>()

    )

**Set** the `Name` property to `EmployeeAverageSales`.

    .Name("EmployeeAverageSales")

**Set** the controls height to `30px`.

    .HtmlAttributes(new { style = "height:30px;" })

Next, **add** and define a Series chart with the following properties.

 - **Set** `Line` to the `EmployeeSales` property on the model.
 - **Set** the `Width` to `1.5`
 - **Disable** markers by setting the `Markers` visible property to `false`
 - **Set** the tooltip using an inline Kendo UI Template `#=kendo.toString(value, 'c2')#`


    .Series(series =>
    {
        series.Line(model => model.EmployeeSales)
        .Width(1.5)
        .Markers(m => m.Visible(false))
        .Tooltip(t => t.Template("#=kendo.toString(value, 'c2')#"));
    })

Next, **add** and configure the Category Axis with a `Date` configuration

**Set** the Category to the `Date` field of the view model

Since the chart will be a formatted like a [sparkline](https://en.wikipedia.org/wiki/Sparkline), **set** these `Visible` properties to `false` to disable them.

- *Axis* Visible
- MajorGridLines Visible


     .CategoryAxis(ca => ca
         .Date()
         .Categories(model => model.Date)
         .Visible(false)
         .MajorGridLines(m => m.Visible(false))
     )

Next, **add** and configure the ValueAxis with a Numeric configuration.

**Set** the following `Visible` properties to `false` to disable them.

- *Axis* Visible
- Labels Visible
- MajorGridLines Visible


     .ValueAxis(va => va.Numeric()
         .Visible(false)
         .Labels(lab => lab.Visible(false))
         .MajorGridLines(m => m.Visible(false))
      )

Also **set** the `Legend` to `false`    

    .Legend(leg => leg.Visible(false))

**Configure** the `DataSource` by setting `Read` to the action `EmployeeAverageSales` on the `Home` controller.

Using the `Data` property, **set** the value to `getEmployeeFilter` sending filter data back to the `Read` action.

**Add** an `Aggregates` on the DataSource to `Average` the `EmployeeSales`.      

    .DataSource(ds => ds
        .Read(read => read.Action("EmployeeAverageSales", "Home")
        .Data("getEmployeeFilter"))
        .Aggregates(a => a.Add(model => model.EmployeeSales).Average())
     )

Since the DataSource will be invoked on manually, **set** `AutoBind` to `false`

    .AutoBind(false)

The resulting code should be:

    @(Html.Kendo().Chart<KendoQsBoilerplate.MonthlySalesByEmployeeViewModel>()
        .Name("EmployeeAverageSales")
        .HtmlAttributes(new { style = "height:30px;" })
        .Series(series =>
        {
            series.Line(model => model.EmployeeSales)
            .Width(1.5)
            .Markers(m => m.Visible(false))
            .Tooltip(t => t.Template("#=kendo.toString(value,'c2')#"));
        })

        .CategoryAxis(ca => ca
            .Date()
            .Categories(model => model.Date)
            .Visible(false)
            .MajorGridLines(m => m.Visible(false))
        )

        .ValueAxis(va => va.Numeric()
            .Visible(false)
            .Labels(lab => lab.Visible(false))
            .MajorGridLines(m => m.Visible(false))
        )
        .Legend(leg => leg.Visible(false))
        .DataSource(ds => ds
            .Read(read => read.Action("EmployeeAverageSales", "Home")
            .Data("getEmployeeFilter"))
            .Aggregates(a => a.Add(model => model.EmployeeSales).Average())
            )
        .AutoBind(false)
    )

**Open** `controllers/HomeController.cs` and **create** a controller action named `EmployeeAverageSales` on the `Home` controller. This action will supply the Chart with data.

The boilerplate installed in Chapter 1 has a function named `EmployeeAverageSalesQuery`, this query will select the data required for the chart. **Return** the results of `EmployeeAverageSalesQuery` as JSON.

	public ActionResult EmployeeAverageSales(
        int employeeId,
        DateTime statsFrom,
        DateTime statsTo)
    {
        var result = EmployeeAverageSalesQuery(employeeId, statsFrom, statsTo);

        return Json(result, JsonRequestBehavior.AllowGet);
    }

Add the partial view to the main application page.

In `Views/Home/Index.cshtm` **find** the `<!-- Montly Sales Chart -->` placeholder.

	<!-- Montly Sales Chart -->
	@Html.Placehold(430, 120, "Chart")

**Replace** the placeholder with the `_MonthlySalesByEmployee` partial.

	<!-- Montly Sales Chart -->
	@Html.Partial("_MonthlySalesByEmployee")

**Find** the scripts section.

	<script>
		...
	</script>

**Add** a new function named `refreshEmployeeAverageSales`, this function will invoke `read` on the chart's data source.

The resulting code should be:

	function refreshEmployeeAverageSales() {
        var employeeAverageSales = $("#EmployeeAverageSales").data("kendoChart");
        employeeAverageSales.dataSource.read();
    }

**Find and modify** the `onCriteriaChanged` function so it calls `refreshGrid` updating the entire dashboard when a filter is changed.

	function onCriteriaChange() {
        updateEmployeeAvatar();
        refreshGrid();
        refreshEmployeeQuarterSales();
        refreshEmployeeAverageSales();
    }

**Run** the application to see the chart render on the dashboard. Change the filter criteria to see the chart update along with other UI elements.

![Spark Line Chart](images/chapter8/spark-line-chart.jpg)

<div class="exercise-end"></div>

### Client-Side API

Charts, like other Kendo UI widgets are easy to interact with on the client side. By handling the chart's events additional functionality can be added to the application. Use the DataBound event and the DataSource to populate values on labels within the Team Efficiency Dashboard.

<h4 class="exercise-start">
    <b>Exercise</b>: Display chart values using client APIs.
</h4>

In `Views/Home/Index.cshtm`, **find** the scripts section.

	<script>
		...
	</script>

**Add** a function named `onQuarterSalesDataBound`, find the first element of the datasource and displays the Current value in `EmployeeQuarterSalesLabel`.

    function onQuarterSalesDataBound(e) {
        var data = this.dataSource.at(0);
        $("#EmployeeQuarterSalesLabel").text(kendo.toString(data.Current, "c2"));
    }

**Add** a function named `onAverageSalesDataBound` find the dataSource aggregates and display the average of `EmployeeSales` in the `EmployeeAverageSalesLabel`.

	function onAverageSalesDataBound(e) {
        var label = $("#EmployeeAverageSalesLabel"),
            data = this.dataSource.aggregates()

        if (data.EmployeeSales) {
            label.text(kendo.toString(data.EmployeeSales.average, "c2"));
        } else {
            label.text(kendo.toString(0, "c2"));
        }
    }

**Open** the partial view `_MonthlySalesByEmployee.cshtml` and **add** a `DataBound` event handler to the chart, set the event handler to `onQuarterSalesDataBound`.

    @(Html.Kendo().Chart<KendoQsBoilerplate.MonthlySalesByEmployeeViewModel>()
        ...
	    .AutoBind(false)
        .Events(e => e.DataBound("onAverageSalesDataBound"))
	)

**Open** the partial view `_QuarterToDateSales.cshtml` and add a `DataBound` event handler to the chart, set the event handler to `onQuarterSalesDataBound`.

    @(Html.Kendo().Chart<KendoQsBoilerplate.QuarterToDateSalesViewModel>()
        ...
        .AutoBind(false)
        .Events(e => e.DataBound("onQuarterSalesDataBound"))       
    )

![Chart Client API](images/chapter8/chart-client-api.jpg)

<div class="exercise-end"></div>

The Team Efficiency Dashboard is starting to look complete, but it hasn't been tested for devices like mobile phones or tablets yet. In the next chapter you'll use responsive web design techniques to support devices beyond the desktop.
