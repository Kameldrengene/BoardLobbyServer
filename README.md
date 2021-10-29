# BoardLobbyServer :rocket:

Prerequisite:
* .NET 5.0

Publish project with following command in project source folder:
### ` dotnet publish -c Release -o published `

Run application from in project source folder:
### ` dotnet published\BoardLobbyServer.dll `

Docker image building (Dockerfile can be found at top level folder):
### ` docker build -t <imagename> . `

Run MongoDB from docker:
### ` docker run -d --name mongo -p 27017:27017 mongo `

Run Below commands from MongoDb client:

### ` use BoardServerDB `
### ` db.createCollection('Admins') `
### ` db.Admins.insertMany([{'Name':'Frederik','Password':'asdfasdf'}, {'Name':'Mark','Password':'asdfasdf'}]) `

Run Docker container:
### ` docker run -it --rm -p 5000:80 --name <containername> <imagename> `
