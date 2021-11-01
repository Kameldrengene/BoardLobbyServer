# BoardLobbyServer :rocket:

## Tasks:
### 1st iteration sumup
- [x] Create lobby and specifying its name
- [x] automatically join the lobby when creating
- [x] Becomming the lobby-leader
- [x] have it listed to other clients
- [ ] leaving the lobby as leader should make it disappear
- [ ] show message to other clients, when the lobby-leader leaves
- [x] Client can join a lobby
- [x] Client is shown info about the lobby
- [x] can see who else is in the lobby
- [ ] can leave the lobby
- [ ] A system that allows multiple clients to connect to the same server. 
  - [ ] Clients can specify a username when connecting
  - [ ] The system should display how many clients that are not in a lobby
- [ ] A client should be able to join a lobby from a listing of multiple lobbies and thereby entering a new screen without other lobby listings
  - [ ] A client should be able to see information about the lobby before and after joining it
  - [ ] A client can see who else is in the lobby
  - [ ] A client should be able to leave the lobby
- [ ] A client should be able to create a new lobby and specify the lobbys name. now the client counts a lobby-leader
  - [ ] The lobby should then be visible to all other clients not currently in a lobby
  - [ ] If the lobby-leader leaves, the lobby disapears sending all clients in the lobby back to the first screen
    - [ ] This will display a message to other clients than the lobby-leader
- [ ] create client Gui
### 2nd iteration sumup
- [ ] a lobby can start a Game when 2 or more players are connected
  - [ ] This disconnects the lobby and all clients from the server
  - [ ] the game is played by P2P connection
  - [ ] the game should be able to send a message
- [ ] When choosing a new hanger, it should be done randomly
  - [ ] each client will broadcast a random byte-array of size 8 (Each index is 0-255)
  - [ ] each client will then sum the arrays to one array and have each position Mod 256
  - [ ] each client will use this summed array as the seed for randowmly choosing a new hanger.
  - [ ] the client will automatically write a message in the chat with their result 
- [ ] The lobby will create a P2P connection with all clients
  - [ ] Messages can be sent from one client and then broadcasted to every client
- [ ] The Game is a simple hangman game
  - [ ] It is a chat system
  - [ ] one player is assigned to be the 'hanger'
  - [ ] the hanger chooses a word or phrase and enters it into the chat as underscores instead of letters
  - [ ] other clients write a letter in the chat and the hanger will substitute any underscores where that letter should be
  - [ ] is the letter not in the word or phrase, the players loose a life
  - [ ] the players have 6 lives total 
### 3rd Iteration sumup
##### Database
- [x] Login database
- [x] Implement audit
- [ ] init database: https://stackoverflow.com/questions/42912755/how-to-create-a-db-for-mongodb-container-on-start-up
##### Admin Login
- [x] Login
- [x] Authentication
- [x] JWT and bcrypt
- [ ] JWT secret
- [ ] database connection
##### Player Client
- [ ] Tokens ?
- [ ] Validation algo
##### Docker
- [ ] Docker-compose med mongo, mongo express og vores server

### Overall
- [ ] Ludo Logic
- [ ] Ludo Gui
- [ ] Lobbies can be multiple games

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


```mermaid 
graph LR

Node1 -->ServiceNode1
Node2 -->ServiceNode2
Node2 -->ServiceNode3

subgraph "Services"
ServiceNode1[SignalR]
ServiceNode2[Razor Pages]
ServiceNode3[Rest]
end

subgraph "Clients"
Node1[Players]
Node2[Admins]

end
  ``` 
