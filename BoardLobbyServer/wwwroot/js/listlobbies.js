"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/lobbyHub").build();

connection.start().then(function () {
    connection.invoke("getLobbies").catch(function (err) {
        return console.error(err.toString());
    });
}).catch(function (err) {
    return console.error(err.toString());
})

function createBtn(id) {
    var element3 = document.createElement("input");
    element3.id = id;
    element3.type = "button";
    element3.name = "add";
    element3.value = "Join";
    element3.className = "btn btn-primary btn-xs";
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
        newCell5.appendChild(createBtn(i));
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