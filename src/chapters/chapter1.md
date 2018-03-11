## Getting Up and Running

In this chapter you're setting up the basics for unit testing.

### Create an xUnit Test Project - VS2017

Start by creating a new Class Library project. You'll use this project throughout the rest of this tutorial to build your application.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a new project    
</h4>

Click **File > New Project > xUnit Test Project (.NET Core)**

Name the project **CsharpPoker**

![](images/chapter1/new-xunit-project.jpg)

Delete UnitTest1.cs

Using the package manager console, run the following commands

    PM> Install-Package FluentAssertions

Alternatively, you can use the package manager GUI

The next steps are for Visual Studio 2015 users. You may advance to step 2.0.

<div class="exercise-end"></div>

### VS2015 ONLY - Create a New Class Library Project

Start by creating a new Class Library project. You'll use this project throughout the rest of this tutorial to build your application.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a new project    
</h4>

Click **File > New Project > Class Library**

Name the project **CsharpPoker**

Delete Class1.cs

<div class="exercise-end"></div>

### VS2015 ONLY - Install the xUnit, unit testing framework

With the new project created, it's time to start building unit tests. For this guide, we are using xUnit. xUnit is a commonly used unit testing framework in .NET.

<h4 class="exercise-start">
    <b>Exercise</b>: Install xUnit
</h4>

Open the package manager console, in quick launch, type: package manager console

Using the package manager console, run the following commands

    PM> Install-Package xunit
    PM> Install-Package xunit.runner.visualstudio
    PM> Install-Package FluentAssertions

Alternatively, you can use the package manager GUI

<div class="exercise-end"></div>

