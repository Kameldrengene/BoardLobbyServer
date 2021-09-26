"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established

document.getElementById("okbutton").disabled = true;

function createPlayer() {
    let playername = document.getElementById("playername").value;
    connection.invoke("CreatePlayer", playername).catch(function (err) {
        return console.error(err.toString());
    });
    return false;
}

connection.on("CreatePlayer", function (player) {
    console.log(player);
    document.getElementById('createbutton').click();
});



connection.start().then(function () {
    
    document.getElementById("okbutton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
    
});
