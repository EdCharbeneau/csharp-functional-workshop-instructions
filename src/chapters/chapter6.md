## Client Side

The client side is where Kendo UI really shines. Kendo UI uses a common JavaScript language and standards so that it’s easy for any JavaScript developer to get started. In this chapter you'll learn about client-side events and how to take control of Kendo UI using JavaScript.

### Working with Client-Side Events

Telerik UI for MVC helpers provide an Events method that is part of the HTML Helper's property chain. The Events method is used to set event handlers for the Kendo UI widget. Each widget has a variety of events that can be handled including: cancel, change, dataBound, dataBinding, edit, remove, and save.

Example:

    @(Html.Kendo().ListView<ProductViewModel>()
            .Name("listView")
            .TagName("div")
            .ClientTemplateId("template")
            .DataSource(dataSource => {
                dataSource.Read(read => read.Action("Products_Read", "ListView"));
            })
            .Events(e => e
                .DataBound("productListView_dataBound")
                .Change("productListView_change")
            )
    )

Let's continue to work with the EmployeesList that was created in the previous chapter. The list is selectable, but when the application starts the first item should be selected by default giving the user a starting point to begin interacting with the dashboard.

<h4 class="exercise-start">
    <b>Exercise</b>: Select the first list item by default.
</h4>

**Find** the `EmployeeList`

	<!-- Employee List View -->
	@(Html.Kendo().ListView<Employee>()
            .Name("EmployeesList")
			...
        	.Selectable(s => s.Mode(ListViewSelectionMode.Single))
	)

**Add** an event handler named `onListDataBound` for the `DataBound` event for the EmployeeList.

	@(Html.Kendo().ListView<KendoQsBoilerplate.Employee>()
		...
		.Selectable(s => s.Mode(ListViewSelectionMode.Single))
		.Events(e => e.DataBound("onListDataBound"))
	)

The resulting code should be:

	<!-- Employee List View -->
	@(Html.Kendo().ListView<KendoQsBoilerplate.Employee>()
    	.Name("EmployeesList")
        .ClientTemplateId("EmployeeItemTemplate")
        .TagName("ul")
        .DataSource(dataSource =>
        {
        	dataSource.Read(read => read.Action("EmployeesList_Read", "Home"));
        	dataSource.PageSize(9);
		})
        	.Selectable(s => s.Mode(ListViewSelectionMode.Single))
            .Events(e => e.DataBound("onListDataBound"))
	)

In the same view **find** the `Scripts` section.

	@section Scripts {
	    <script>
	        //Custom Scripts
	    </script>
	}

In the `<script>` element and **add** a function `onListDataBound`.

**Select the first element** by calling the `.select` function on the ListView object `this` and pass in the element first employee element using the jQuery selector `$(".employee:first")`.

	@section Scripts {
	    <script>
	        //Custom Scripts
			function onListDataBound(e) {
		        this.select($(".employee:first"));
		    }
	    </script>
	}

**Refresh** the page to see that the first item in the list is selected by default.

![employee list selected](images\chapter6\employee-list-selected.jpg)

<div class="exercise-end"></div>

Selecting the first item using the `DataBound` event was a good start. Next we'll take it a step further by using the selected item to populate a Kendo UI template showing the selected employee on the dashboard.

<h4 class="exercise-start">
    <b>Exercise</b>: Use the Change event to populate a template.
</h4>

**Add** an event handler named `onCriteriaChange` for the `Change` event for the EmployeeList.

	@(Html.Kendo().ListView<Employee>()
			...
        	.Selectable(s => s.Mode(ListViewSelectionMode.Single))
            .Events(e => e.DataBound("onListDataBound")
   					      .Change("onCriteriaChange"))
	)

**Find** the `<!-- Kendo Templates -->` placeholder.

	<!-- Kendo Templates -->
		...
	<!-- /Kendo Templates -->

**Add** a new template that will display the selected employee's image and full name.

	<!-- Kendo Templates -->
	<script type="text/x-kendo-tmpl" id="employeeAvatarTemplate">
	    <img src="@(Url.Content("~/content/employees/"))#:EmployeeId#.png" />
	    <span>#:FullName#</span>
	</script>

**Find** the `<script>` section.

	<script>
		...
    </script>

**Add** a function named `getSelectedEmployee` that returns the selected employee from the EmployeeList.

	function getSelectedEmployee() {
    	var employeeList = $("#EmployeesList").data("kendoListView"),
		employee = employeeList.dataSource.getByUid(employeeList.select().attr("data-uid"));
		return employee;
	}

**Add** a function named `updateEmployeeAvatar` that binds the selected employee data to the `employeeAvatarTemplate` and places the template's  content in the `employee-about` element.

	function updateEmployeeAvatar() {
        var employee = getSelectedEmployee(),
            template = kendo.template($("#employeeAvatarTemplate").html());

        //apply template
        $("#employee-about").html(template(employee));
    }

**Add** a function named `onCriteriaChange`, this function will handle the `Change` event and call `updateEmployeeAvatar`.

	function onCriteriaChange() {
        updateEmployeeAvatar();
	}

**Refresh** the page and select an employee from the EmployeeList. Selecting an item should update the dashboard with the selected employee's data.

![selected item to template](images/chapter6/selected-item-to-template.jpg)

**Find and remove** the `<!-- Employee Avatar -->` placeholder code, it is no longer needed because the element is created dynamically.

Remove:

     <!-- Employee Avatar -->
     @Html.Placehold(90, 90, "Face")
     <span>Full Name </span>


<div class="exercise-end"></div>

Now that you know how to work with client-side APIs, let's enhance the Team Efficiency Dashboard by working with datasources.
