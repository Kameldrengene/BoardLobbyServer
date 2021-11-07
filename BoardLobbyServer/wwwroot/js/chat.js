"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var mess = document.createElement("div");
    mess.className = "d-flex flex-row justify-content-start justify-content-end";
  
    mess.innerHTML = '<div class="card text-white bg-secondary w-50 m-1" ><div class="card-header">' + user + ' says:</div><div class="card-body"><p class="card-text">' + message + '</p></div></div>';

    document.getElementById("messagesList").appendChild(mess);
  

});


connection.on("ReceiveOwnMessage", function (user, message) {
    var mess = document.createElement("div");
    mess.className = "d-flex flex-row justify-content-start";

    mess.innerHTML = '<div class="card text-white bg-primary w-50 m-1"><div class="card-header">' + user + ' says:</div><div class="card-body"><p class="card-text">' + message + '</p></div></div>';

    document.getElementById("messagesList").appendChild(mess);


});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});