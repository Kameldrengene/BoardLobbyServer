"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.start().then(function () {
    let game = window.localStorage.getItem('game');
     console.log(game);
    game = $("#gameidfromlist").text();
    console.log(game);
    connection.invoke("MonitorGame", game).catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("MonitorGame", function (game) {
    console.log(game);
    document.getElementById("gamename").innerHTML = game.gameName;
    document.getElementById("gameid").innerHTML = game.id;
    document.getElementById("leadername").innerHTML = game.leader.name;
    Array.from(game.participants).forEach(function (participant, index) {
        console.log(participant);
        var ul = document.getElementById("Participants");
        var li = document.createElement("li");
        li.appendChild(document.createTextNode(participant.name));
        ul.appendChild(li);
    })
});

connection.on("RecieveParticipants", function (participants) {
    console.log(participants)
    const gamepars = JSON.parse(participants);
    Array.from(gamepars).forEach(function (participant, index) {
        console.log(participant);
        var ul = document.getElementById("Participants");
        var li = document.createElement("li");
        li.appendChild(document.createTextNode(participant.Name));
        ul.appendChild(li);
    })
});