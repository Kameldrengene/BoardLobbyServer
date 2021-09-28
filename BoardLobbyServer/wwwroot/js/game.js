"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.start().then(function () {
    const game = window.localStorage.getItem('game');
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
});