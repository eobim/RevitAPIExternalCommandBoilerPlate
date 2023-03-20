////////////////////////////////////////////////////////////

// EOBIM's RevitAPI external command boilerplate tutorial

////////////////////////////////////////////////////////////

// 0. Course brief:

/*

0.1.
The EOBIM's RevitAPI external command boilerplate tutorial
is a sample tutorial of the EOBIM's RevitAPI code series
that guides you through every relevant
aspect of a c# file,
that contains the necessary elements to generate and execute
a "External Command" in Revit 2023.

0.2.
This tutorial reviews one by one, the key elements 
that should apear in every external command 
(at least in some form, not necessarily in this specific manner),
in order that the external command works.

0.3.
This tutotial consist on the following points:
1. Namespace referencing.
2. Class declaration / interface inheritance.
3. Interface primary and required method declaration.
4. Storing of essential classes for Revit API actions.
5. End statement of primary and required interface method.
6. End of primary and required class.
7. End of namespace and code.

0.4.
The inmense majority of this file consist on commments.
Actually the code is pretty short.
The actual code is whatever you don't find commented.
Don't worry about having trouble on identifiying what
is located inside each code block,
I will do my best to clearly state what belongs to what.
Nonetheless, I'll include a the entire boilerplate in 
the bottom of the file,
so that you can visualize it better.

0.5.
Finally, I would like to whish you my best in your learning.
I know how hard it is.
Take heart!
I find strength every day in Jesus, my Savior.
He gives me strenght to go on.
If you have doubts or suggestions, please write to my email,
eobimoffice@gmail.com
Your comments will be very valuable to the improvement of this
tutorial.

0.6.
Let's start!!!

*/

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// EOBIM's RevitAPI external command boilerplate tutorial

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// 1. Support namespaces referencing.

////////////////////////////////////////////////////////////

/*

1.0. 
The first step in our boilerplate is to reference 
the Autodesk.Revit required namespaces.

1.1.
A namespace is a container to organize and control your code.

1.2.
According to Microsoft docs

"Namespaces are heavily used in C# programming in two ways. 
First, .NET uses namespaces to organize its many classes".

"Second, declaring your own namespaces can help you control 
the scope of class and method names in larger programming projects".
(https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/namespaces)

1.3.
"Autodesk.Revit" namespaces are namespaces created by 
Autodesk, that contain the classes that operate in our 
Revit application.

1.4.
(IMPORTANT!!!)
Referencing this namespaces is possible ONLY IF we first add references to 
the RevitAPI, to the RevitAPIUI in the .csproj file that is 
automactically generated when we create a 
class library through visual studio or through .NET CLI.

1.5.
This references looks like this:
<ItemGroup>

	<Reference Include="RevitAPI">
		<HintPath>C:\Program Files\Autodesk\Revit 2023\RevitAPI.dll</HintPath>
		<Private>False</Private>
	</Reference>

	<Reference Include="RevitAPIUI"> 
		<HintPath>C:\Program Files\Autodesk\Revit 2023\RevitAPIUI.dll</HintPath>
		<Private>False</Private>
	</Reference>

</ItemGroup>

1.6.
Only after defining this references in the .csproj file
You can succesfully reference Autodesk's namespaces.

1.7.
The .csproj file is the file that controls the compiling
of the class libraty.
That's why you need to create this references first.

1.8.
If you use Visual Studio, it generates them for you
If you use .NET CLI you have to write them yourself

1.9.
At the end of this file you'll find templates for 
the .csproj file, and for creating this 
class library in .NET CLI

1.10.
This boilerplate doesn't uses the Autodesk.Revit.Creation namespace,
but I just included it in case that you need it for your scripts.

*/


using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Creation;
using Autodesk.Revit.UI;


////////////////////////////////////////////////////////////

// 2. Namespace declaration

////////////////////////////////////////////////////////////

/*

2.0.
Next we specify the namespace that will contain the external 
command class and method.

2.1.
A namespace is a container to organize and control your code.

2.2.
According to Microsoft docs  

"Namespaces are heavily used in C# programming in two ways. 
First, .NET uses namespaces to organize its many classes"

"Second, declaring your own namespaces can help you control 
the scope of class and method names in larger programming projects."

(https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/namespaces)

2.3.
You can name this namespace as you prefer as long you keep
the sintax rules of namespaces in c#.

*/


namespace ExternalCommandBoilerplate
{


////////////////////////////////////////////////////////////

// 3. Transaction attribute implementation

////////////////////////////////////////////////////////////

/*

3.0.
The next statement represents an Attribute.

3.1.
According to Microsoft docs  

"Attributes can be placed on almost any declaration, 
though a specific attribute might restrict the types of 
declarations on which it's valid". 

"In C#, you specify an attribute by placing the name of 
the attribute enclosed in square brackets ([]) 
above the declaration of the entity to which it applies".

(https://learn.microsoft.com/en-us/dotnet/csharp/advanced-topics/reflection-and-attributes/)

3.2.
The attribute declaration statement ("[Transaction(TransactionMode.Manual)]") 
instantiates the "Transaction" attribute class
created by Autodesk by derivating of "System.Attribute" class.

3.3.
This instanciation is achieved passing as it's argument 
the "Manual" member of the "TransactionMode" enum 
in the "Autodesk.Revit.Attributes" namespace.

3.4.
According to the Revit API documentation, 
the "TransactionMode" enum represents
"All transaction modes supported by Revit external commands".
(https://www.revitapidocs.com/2018/84254a1f-7bba-885a-ce65-e68fc238fddb.htm).

3.5.
This statement indicates that this external command will be able to
write changes in the document.

3.6.
According to the RevitAPI documentation
If we decide to use the "ReadOnly" member instead of "Manual"

"No transaction (nor group) will be created, and 
no transaction may be created for the lifetime of the command."

In this case "The External command may use methods that 
only read from the model, but 
not methods that write anything to it".
(https://www.revitapidocs.com/2018/84254a1f-7bba-885a-ce65-e68fc238fddb.htm).

*/


[Transaction(TransactionMode.Manual)]


////////////////////////////////////////////////////////////

// 4. External Command class declaration and interface
// implementation

////////////////////////////////////////////////////////////

/*

4.0.
Next we see an implementation of the IExternalCommand interface.

4.1.
"IExternalCommand is an interface that should 
be implemented to provide the implementation for a 
Revit add-in External Command".
(https://www.revitapidocs.com/2023/ad99887e-db50-bf8f-e4e6-2fb86082b5fb.htm)

4.2.
This interface guarantees the essential form of the class 
to be executed by Revit and of
it's only menber: A method (called "Execute").

*/


	public class BoilerplateBaseAndTest : IExternalCommand
	{

		
////////////////////////////////////////////////////////////

// 5. External Command class 
// primary and required method declaration

////////////////////////////////////////////////////////////

/*

5.0.
Next comes the Execute method of the IExternalCommand interface.

5.1.
You don't have to pass anything in it's arguments.

5.2.
The only argument to cosider in detail in it is "commandData".

5.3.
CommandData is an instance of the "ExternalCommandData" class.

5.4.
This instance references the application of Revit for the
current external command written in this file.

5.5.
This instance allows the "Execute" method to work with the
Revit application.

5.6.
"ExternalCommandData" is a class from "Autodesk.Revit.UI" namespace.

5.7.
Please notice that finally the command to be executed is a method.
A public method of "Result" type and "Execute" name.

5.8.
Therefore, we need to end the method with a "return" statement that 
Yields a "Result.<emun menber option>" in order to work.

5.9.
The options available in the "Result" enum are:
Failed
Succceeded
Cancelled

*/


		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        	{
		
		
////////////////////////////////////////////////////////////

// 6. Obtain and store reference to the current Revit application

////////////////////////////////////////////////////////////

/*

6.0.
Next we obtain and store a reference to the current Application.

6.1.
"commandData.Application"
"Represents an active session of the Autodesk Revit user interface,
providing access to UI customization methods, 
events, the main window, and the active document".
(https://www.revitapidocs.com/2023/51ca80e2-3e5f-7dd2-9d95-f210950c72ae.htm).

6.2.
"Application" is a property of the ExternalcommandData instanciated 
in the variable "commandData", 
that retrieves an object that "represents the current Application for external command". 
(https://www.revitapidocs.com/2023/4ac271da-80b2-d66e-3824-fa88091c1e79.htm).

6.3.
An instance of ExternalcommandData class seems to be passed 
by default when
executing the Execute metod of the IExternalCommand interface that 
was inherited by the file preeminent class
("BoilerplateBaseAndTest" class in this case).

6.4.
The next line we create a variable called "uiapp" that 
contains an object that represents the current Aplication that 
is in fact an instance of the UIApplication class.

*/


	Autodesk.Revit.UI.UIApplication uiapp = commandData.Application;
	
	
////////////////////////////////////////////////////////////

// 7. Obtain and store reference to the 
// the database level current Revit application

////////////////////////////////////////////////////////////

/*

7.0.
Next, comes the grabbing of the database level current 
Revit application.

7.1.
The uiapp.Application is a property of the UIApplication class 
that "Returns the database level Application represented by 
this UI level Application".
(https://www.revitapidocs.com/2023/ef60b8a9-75b6-a227-f991-55d73ef0c695.htm).

*/

	
	Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
	
	
////////////////////////////////////////////////////////////

// 8. Obtain and store reference to the "ActiveDocument"

////////////////////////////////////////////////////////////

/*

8.0.
The next line obtains the current "ActiveDocument" in the user interface.

8.1.
""ActiveUIDocument" is a get only property of the 
UIDocument class, that Provides access to an object that represents the currently active [Revit] project".
(https://www.revitapidocs.com/2023/3488133d-60c2-aa7c-ab72-0d9360ff122a.htm).

*/


	Autodesk.Revit.UI.UIDocument uidoc = commandData.Application.ActiveUIDocument;
	

////////////////////////////////////////////////////////////

// 9. Obtain and store reference to the database level 
// Revit document.

////////////////////////////////////////////////////////////

/*

9.0.
The next statement retrieves the current instance of the 
UIDocument class, and
obtains its Document property.

9.1.
UIDocument.Document is a property that
"Returns the database level document represented by
this UI-level document".
(https://www.revitapidocs.com/2023/b2ec237a-74c9-d0ed-c192-65186d2d434e.htm).

*/


	Autodesk.Revit.DB.Document doc = commandData.Application.ActiveUIDocument.Document;	


////////////////////////////////////////////////////////////

// 10. Test dialog for boilerplate

////////////////////////////////////////////////////////////
	
/*

10.0.
This next lines are optional.

10.1.
They only tests if the command is working by
generating a window that shows a message to demanstrate that the
external command is working.

*/


TaskDialog testDialog = new TaskDialog("Test dialog");
testDialog.MainInstruction = "I'm working!";
testDialog.Show();


////////////////////////////////////////////////////////////

// 11. Return Statement of the "Execute" method

////////////////////////////////////////////////////////////
	
/*

11.0.
This final line of the Execute method is required.

11.1.
This statement ends the method and the actions of the
external command.

11.2.
The options that can be returned in the 
"Autodesk.Revit.UI.Result" enum are:

-"Failed: The external application was unable to complete its task.

-"Succeeded: The external application completed successfully. 
Autodesk Revit will keep this object during the entire Revit session.

-"Cancelled: Signifies that the external application is cancelled."

(https://www.revitapidocs.com/2023/e6cebb3c-0c3f-7dc4-2063-e5df0a00b2f5.htm).

11.3.
This return statement is necessary because the method has a Result type
specified on it's declaration.

*/


			return Result.Succeeded;

			
////////////////////////////////////////////////////////////

// 12.

////////////////////////////////////////////////////////////

		}
		
// End of the class

	}
	
// End of the namespace

}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// Tutorial end

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// Tutorial Assets

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// 1. EOBIM's RevitAPI external command uncommented boilerplate

////////////////////////////////////////////////////////////

/*

using Autodesk.Revit.Attributes;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.Creation;
using Autodesk.Revit.UI;

namespace ExternalCommandBoilerplate
{

	[Transaction(TransactionMode.Manual)]
	public class BoilerplateBaseAndTest : IExternalCommand
	{

		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{

			Autodesk.Revit.UI.UIApplication uiapp = commandData.Application;
			Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
			Autodesk.Revit.UI.UIDocument uidoc = commandData.Application.ActiveUIDocument;
			Autodesk.Revit.DB.Document doc = commandData.Application.ActiveUIDocument.Document;	

			TaskDialog testDialog = new TaskDialog("Test dialog");
			testDialog.MainInstruction = "I'm working!";
			testDialog.Show();

			return Result.Succeeded;
			
		}

	}

}

*/

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// 2. Assets for the boilerplate
 
////////////////////////////////////////////////////////////

// 2.0.
// .csproj file template

////////////////////////////////////////////////////////////

/*

<?xml version="1.0" encoding="utf-8"?>

<Project Sdk="Microsoft.NET.Sdk" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">

	<PropertyGroup>
		<TargetFramework>net6.0</TargetFramework>
		<ImplicitUsings>enable</ImplicitUsings>
		<Nullable>enable</Nullable>
	</PropertyGroup>

	<ItemGroup>

		<Reference Include="RevitAPI">
			<HintPath>C:\Program Files\Autodesk\Revit 2023\RevitAPI.dll</HintPath>
			<Private>False</Private>
		</Reference>

		<Reference Include="RevitAPIUI"> 
			<HintPath>C:\Program Files\Autodesk\Revit 2023\RevitAPIUI.dll</HintPath>
			<Private>False</Private>
		</Reference>

		<!-- <Reference Include="System" /> -->
		<!-- <Reference Include="System.Core" /> -->
		<!-- <Reference Include="System.Xml.Linq" /> -->
		<!-- <Reference Include="System.Data.DataSetExtensions" /> -->
		<!-- <Reference Include="Microsoft.CSharp" /> -->
		<!-- <Reference Include="System.Data" /> -->
		<!-- <Reference Include="System.Net.Http" /> -->
		<!-- <Reference Include="System.Xml" /> -->

	</ItemGroup>

</Project>

*/

////////////////////////////////////////////////////////////

// 2.1.
// .NET CLI commands in 
// Windows powershell to generate this classlib
// Steps z.4 and z.5 are made in a text editor before
// Sept z.6.

////////////////////////////////////////////////////////////

/*

z.1
dotnet new classlib -n ExternalCommandBoilerplateRvt2023

z.2
cd ExternalCommandBoilerplateRvt2023

z.3
Rename-Item Class1.cs ECBPtutorial.cs

z.4
// (IMPORTANT) Edit .csproj file, 
// paste the above .csproj template in it!

z.5
// (IMPORTANT) Edit the .cs file,
// paste the above .cs template in it!

z.6
dotnet publish -r win-x64 -f net6.0 -c Release --sc false

*/

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

// File end

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////