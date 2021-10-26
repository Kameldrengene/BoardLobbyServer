﻿"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

//Disable send button until connection is established

document.getElementById("createButton").disabled = true;

connection.start().then(function () {
    document.getElementById("createButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// TODO: Når man opretter en ny spil så skal serveren returnere spillet med en uniq id og kalderen skal diregeres til
// ny page med spillet. 
connection.on("EnterGame", function (game) {
    const gameId = game.id;  
    window.localStorage.setItem('game', gameId);
    $("#formgamename").val(gameId);
    $("#gamewatchbutton").click();
    
});

//Når knappen trykkes kald funktionen CreateLobby med  værdierne i felterne 
document.getElementById("createButton").addEventListener("click", function (event) {
    var user = window.localStorage.getItem('playername');
    var lobbyName = document.getElementById("formgamename").value;
    connection.invoke("CreateLobby", user, lobbyName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
