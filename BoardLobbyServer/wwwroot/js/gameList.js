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
    var element3 = document.createElement("button");
    element3.id = game;
    element3.type = "button";
    element3.innerText = "Watch";
    element3.name = "watch";

    //element3.href = "/Game";
    element3.className = "btn btn-info";
    
    element3.addEventListener("click", function () {
        
        connection.invoke("MonitorGame", game).catch(function (err) {
            return console.error(err.toString());
        });



       // <button type="button" class="btn btn-lg btn-danger" data-bs-toggle="popover" title="Popover title" data-bs-content="And here's some amazing content. It's very engaging. Right?">Click to toggle popover</button>
      // <a hidden id="entergame" class="btn btn-link btn-lg text-dark" asp-area="" asp-page="/Game">Enter Game</a>
    })
    
    return element3;
}

function createDeleteBtn(game,row) {
    var delButton = document.createElement("input");
    delButton.id = "del"+game;
    delButton.type = "button";
    delButton.name = "add";
    delButton.value = "Delete";
    delButton.className = "btn btn-danger btn-xs";
    delButton.style.marginLeft = "10px"
    delButton.addEventListener("click", function () {
        alert("Game with id: " + game + "will be deleted!");
        connection.invoke("deleteGame", game).catch(function (err) {
            return console.error(err.toString());
        });
    })

    return delButton;
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
        newCell5.appendChild(createDeleteBtn(`${lobbiesObj[i].Id}`, newRow));
    }

});

connection.on("ReceiveGame", function (game) {
    console.log(game);
    console.log(game.gameName);
    console.log(game.id);
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
    newCell5.appendChild(createBtn(game.id));
    newCell5.appendChild(createDeleteBtn(game.id, newRow));
});

connection.on("RemoveGame", function (game) {
    console.log(game);
    var i = document.getElementById(game).parentNode.parentNode.rowIndex;
    console.log(i);
    var table = document.getElementById('lobbyTable');
    table.deleteRow(i);
});

connection.on("MonitorGame", function (game) {
    console.log(game);

    let gameinfo = "Leader: " + game.leader.name + "\n";
    gameinfo += "Participants: \n"
    for (let participant of game.participants) {
        gameinfo += participant.name + "\n";
    }
    gameinfo += "\n"
    gameinfo += "Watch the game lobby?"

    if (confirm("Game id:" + game.id +"\n"+ gameinfo)) {
        $(document).ready(function () {
            $("#formgameid").val(game.id);
            $("#gamewatchbutton").click();
            
        });
    } else {
        
    }
    
    
});