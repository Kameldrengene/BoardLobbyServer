"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established

document.getElementById("createButton").disabled = true;


connection.on("ReceiveLobbies", function (lobbies) {
    var li = document.createElement("li");
    document.getElementById("LobbyList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${lobbies}`;
});


connection.on("ReceiveLobby", function (user, lobbyName) {
    var li = document.createElement("li");
    document.getElementById("LobbyList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${user} created ${lobbyName}`;
});

connection.start().then(function () {
    
    connection.invoke("getLobbies").catch(function (err) {
        return console.error(err.toString());
    });
    
    document.getElementById("createButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

//Når knappen trykkes kald funktionen CreateLobby med  værdierne i felterne 
document.getElementById("createButton").addEventListener("click", function (event) {
    var user = document.getElementById("playerName").value;
    var lobbyName = document.getElementById("lobbyName").value;
    connection.invoke("CreateLobby", user, lobbyName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
