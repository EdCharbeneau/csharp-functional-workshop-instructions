## Getting Up and Running

In this chapter you're setting up the basics for unit testing.

### Create a New Class Library Project

Start by creating a new ASP.NET MVC project. You'll use this project throughout the rest of this tutorial to build your application.

<h4 class="exercise-start">
    <b>Exercise</b>: Create a new project    
</h4>

Click **File > New Project > Class Library**

Delete Class1.cs

<div class="exercise-end"></div>

### Install the xUnit, unit testing framework

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

