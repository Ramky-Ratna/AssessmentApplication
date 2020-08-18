# ValuelabsAssessment
This is the repository for Valuelabs task completion
# Console Appliaction: AssessmentClientApp

It is console appliaction which runs on client mechine to Process the text file from the current Directory.

This applicatin will take 2 command line Arguments FileName and minimumSalesAmount

After processing the text file it will consume the WEB API service to insert the customer details in AZure SQL DB.


# WebAPI Application : AssementAPI

This application is Hosted in Azure portal.
This service will takes the Data input from above client console application.

This azure web API url : https://assessmentapiservice.azurewebsites.net/api/Customers

This is using Azure SQL SQL as backend to store the data.


# Class Library : DataService

This is .net core class library which will hanle all the data operations.
Here I have created DBContext in Database First Approach

# Azure SQL : AssessmentDB

This Database will contains two tables 
1. Customers
2.CustomerType

This customer Type table is added for the feature enhancement introducing new CustomerType 3 as Government. To resolve this issue I have added this new table.


# Unit Test : XUnitTestProject
I have created the unit test methods using XUnit and Moq.
I have created the moq methods to mock the Controller



