## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)

## General info
This project is software that takes the latest 5 news published on wired.com and creates a quiz about these news. You can enter the question title and question options and choose the correct answer. There is a structure where you can list the exams you have created. You can solve the quiz and find out if your answers are right or wrong.

Project URL: https://wiredproject.azurewebsites.net/
	
## Technologies
Project is created with:
* .NET 6
* N Layer Architecture with MVC
* Microsoft Identity
* Web scrapping with Html Agility Pack
* Bootstrap
* Javascript, jquery, ajax
	
## Setup
The project is currently running on a live database and server.
But if you want to use your database/connection string, change the connection string in appsettings.json

```
"ConnectionStrings": {
    "DefaultConnection": "yourconnectionstring"
  }
```

```
$ mail: test@test.com
$ pass: 123456
```
