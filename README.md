====================================================================
                    Bookstore Management Application
====================================================================

This document provides instructions on configuring the Bookstore 
Management Application, developed as an home test project by Roy Dovrat. 

The application allows users to manage bookstore records stored in 
an XML file and generate HTML reports from these records.

====================================================================
                            Configuration
====================================================================

Before running the application, please ensure that the configuration 
settings are properly set up in the `appsettings.json` file and launchSettings.json. 
The important configurations include:

1. XML File Path (source file)
2. HTML Directory Path (Products directory)
3. applicationUrl


--------------------------------------------------------------------
Configuration Example in appsettings.json
--------------------------------------------------------------------
{
  "XmlFilePath": "C:\\Users\\roydo\\source\\repos\\BookStore_RoyDovrat\\BookStore_RoyDovrat\\bookstore.xml",
  "htmlDirectoryPath": "C:\\Users\\roydo\\source\\repos\\BookStore_RoyDovrat\\BookStore_RoyDovrat"
}
