"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.start().then(function () {
    connection.invoke("getLobbies").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
})

function createBtn(game) {
    var element3 = document.createElement("input");
    element3.id = game;
    element3.type = "button";
    element3.name = "add";
    element3.value = "Join";
    element3.className = "btn btn-primary btn-xs";
    element3.addEventListener("click", function () {
        let player = window.localStorage.getItem("playername");
        alert(player);
        connection.invoke("addParticipant", game, player).catch (function (err) {
            return console.error(err.toString());
        });
    })

    return element3;
}

connection.on("ReceiveLobbies", function (lobbies) {
    console.log(lobbies);
    const lobbiesObj = JSON.parse(lobbies);
    var tbody = document.getElementById('lobbyTable').getElementsByTagName('tbody')[0];
    for (let i = 0; i < lobbiesObj.length; i++) {
        var newRow = tbody.insertRow();
        var newCell1 = newRow.insertCell();
        var newCell2 = newRow.insertCell();
        var newCell3 = newRow.insertCell();
        var newCell4 = newRow.insertCell();
        var newCell5 = newRow.insertCell();
        var text1 = document.createTextNode(`${lobbiesObj[i].Id}`);
        var text2 = document.createTextNode(`${lobbiesObj[i].Leader.Name}`);
        var text3 = document.createTextNode(`${lobbiesObj[i].GameName}`);
        if (lobbiesObj[i].Participants == null) {
            var text4 = document.createTextNode("0");
        } else {
            var text4 = document.createTextNode(`${lobbiesObj[i].Participants.length}`);
        }
        newCell1.appendChild(text1);
        newCell2.appendChild(text2);
        newCell3.appendChild(text3);
        newCell4.appendChild(text4);
        newCell5.appendChild(createBtn(`${lobbiesObj[i].Id}`));
    }   

});

connection.on("ReceiveGame", function (game) {
    console.log(game);
    console.log(game.gameName);
    var tbody = document.getElementById('lobbyTable').getElementsByTagName('tbody')[0];
        var newRow = tbody.insertRow();
        var newCell1 = newRow.insertCell();
        var newCell2 = newRow.insertCell();
        var newCell3 = newRow.insertCell();
        var newCell4 = newRow.insertCell();
        var newCell5 = newRow.insertCell();
    var text1 = document.createTextNode(game.id);
    var text2 = document.createTextNode(`${game.leader.name}`);
    var text3 = document.createTextNode(`${game.gameName}`);
    if (game.participants == null) {
            var text4 = document.createTextNode("0");
        } else {
        var text4 = document.createTextNode(`${game.participants.length}`);
        }
        newCell1.appendChild(text1);
        newCell2.appendChild(text2);
        newCell3.appendChild(text3);
        newCell4.appendChild(text4);
        newCell5.appendChild(createBtn(game.Id));
});