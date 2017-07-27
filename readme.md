# returnNULL Each Voice

Each Voice is a repository for the CS461 and CS462 group project at
Western Oregon University. These courses are part of a three-course
series, beginning with CS460, that are the capstone courses 
required for a BS in Computer Science.

Guidelines to Contributing

Setup: To start contributing you will need to fork our repo at www.bitbucket.org/RyanRothweiler/ReturnNULL and get your local repository working and setup. 

Workflow:

# Each Voice Vision Statement

Our web application, Each Voice, is designed to help bring together information
on local and state government matters to help the public more efficiently
acquire this information to better understand and vote in these matters.
Our goal is that in being able to locate this information easily the American people
will participate more in local and state matters. Each Voice will include details on
proposed local bill, laws, and representatives. The public will be able to discuss
these proposed laws and bill with others. This combination of information
aggregation and user discussion is something that does not
currently exist. We hope to provide a place where the public can start to
become more active in and better educated about their local
and state governments. 

# Team Members

* Mattherin Langely
* Ryan Rothweiler
* Zhendong Ma
* Nikolas Beltran

# Status

We are currently just starting the project.

# Building

This project makes use of Visual Studio Community Edition and Git.
Please see below for version requirements.

# Guidelines to Contributing

## Software Requirements

* Visual Studio Community Edition version 14.0.25431.01 Update 3 or above
	* Entity Framework version 6.1.3
	* boostrap version 3.3.7
	* jQuery version 3.3.1
* Git version 2.11.1.windows.1 or above
	* master and develop branches

## Make

Windows operating system is required for this project due to Visual
Studio being exclusive to Windows. 

Install the lastest version of Git. Leave all checkboxes as they are.
Do not select or deselect any of the options. The installation will
also install Git Bash. This is the recommended command line for this
project.

## Setup

To start contributing you will need to fork our repo at www.bitbucket.org/RyanRothweiler/ReturnNULL and get your local repository working and setup. 

## Workflow

We will be following the basic git workflow for forked repositories. All development changes should occur in a new branch seperate from the Master and Develop branch.
Before starting development your local repository and forked repository should be freshly up to date. Any code contributed that is using classes that are out of date will
be denied. For each feature that you work on a new branch should be made that contains changes ONLY to related to that feature. Each pull request should contain the changes 
needed for only one feature. Issue pull requests after testing locally. Pull requests will be rejected if they do not follow the patterns below or have compile errors. Every 
pull request that is denied will have a simple message explaining why. This will generally happen when these basic guidelines are not followed. 

Programming Patterns:
The following are coding standards that must be adhered to. The reason for this is to keep code uniform and clean whilst increasing readibility. If patterns are not followed all code
submitted in the pull request will be denied. All code should be written in C# or using the appropriate web development language. 

Brackets- All brackets (or curly braces) should be on their own line like below.

## Programming Patterns

The following are coding standards that must be adhered to. The reason for this is to keep code uniform and clean whilst increasing readibility. If patterns are not followed all code
submitted in the pull request will be denied. All code should be written in C# or using the appropriate web development language. 

### Brackets

All brackets (or curly braces) should be on their own line like below.

	if(true)
	{
		//do something
	}

Logical Operators- All logical operatores should have a space on either side. Do not squish them together like below.

	if(ranNum!=10)
	{
	
### Logical Operators

All logical operatores should have a space on either side. Do not squish them together like below.

	if(ranNum!=10)
	{
		//do something
	}
	
Instead logical operators should look like the following:
	
	if(ranNum != 10)
	{
	
		//do something
	}
	
This makes it much easier to read the code when quickly going through it. Because this project is being actively developed these small things save a large amount of time.
	
Naming- We will follow the standard naming procedure for C# classes and methods as the example below shows. All classes, methods, and interfaces should be capitilized on each new word.
### Naming

We will follow the standard naming procedure for C# classes and methods as the example below shows. All classes, methods, and interfaces should be capitilized on each new word.

When naming variables please follow the camel-case standard. Meaning the variable should start with a lower case letter and then be capitilized on each new word thereafter. Any new interface
should be named starting with a capital I as the first letter of the name.
	
	public class DoSomething() : IMakingSomething
	{
		private Random myVariable;
		
		public DoSomething()
		{
			//doing something
		}
	}
	
Commenting- All declarations in this program should be commented. Comments should not appear on every line but should be defaulted to when code is complex or a new method, class, constructor, etc. is made. They do not need to be long comments explaining obvious code but ambiguous names should be labeled clearly. New methods

## Commenting
All declarations in this program should be commented. Comments should not appear on every line but should be defaulted to when code is complex or a new method, class, constructor, etc. is made. They do not need to be long comments explaining obvious code but ambiguous names should be labeled clearly. New methods
should be well defined and easy to understand the overall concept and approach taken. All comments should be in XML format.