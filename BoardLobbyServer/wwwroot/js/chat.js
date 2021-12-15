"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message,img) {
    var mess = document.createElement("div");
    mess.className = "d-flex flex-row justify-content-start justify-content-end";
  
    mess.innerHTML = '<div class="card text-white bg-secondary w-50 m-1" ><div class="card-header">' +
                     '<img src="'+img+'" class="rounded" style="width: 50px" alt="...">' +
                      user + ' says:</div><div class="card-body"><p class="card-text">' +
                      message + '</p></div></div>';

    document.getElementById("messagesList").appendChild(mess);
  

});


connection.on("ReceiveOwnMessage", function (user, message,img) {
    var mess = document.createElement("div");
    mess.className = "d-flex flex-row justify-content-start";

    mess.innerHTML = '<div class="card text-white bg-primary w-50 m-1"><div class="card-header">' +
                     '<img src="' + img + '" class="rounded" style="width: 50px" alt="...">' +
                     user + ' says:</div><div class="card-body"><p class="card-text">' +
                     message + '</p></div></div>';

    document.getElementById("messagesList").appendChild(mess);


});

connection.on("Connected", function (id) {

    const adminname = $('#userInput').text();
    const img = $('#avatar').attr('src')
    connection.invoke("BroadcastStatus", id, adminname, img).catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("Disconnected", function (id) {
    $("#" + id).remove();
});

connection.on("ReceiveOnlineAdmins", function (AdminList) {
    
    for (let admin of AdminList) {
        console.log(admin);
        let item =  '<div id="'+ admin.value._id +'" class="d-flex justify-content-center align-items-center">' +
            '<div><img src="' + admin.value._img +'" class="rounded" style="width: 50px" alt="..."> </div>'+
            '<div id="userInput" class=" badge rounded-pill bg-primary" style="font-size:15px;">' + admin.value._adminname+'</div>'+
                    '</div>';
        $('#adminList').append(item);
    }
    
    
});

connection.on("UpdateOnlineAdmins", function (admin) {
    let item = '<div id="'+admin._id+'" class="d-flex justify-content-center align-items-center">' +
        '<div><img src="' + admin._img + '" class="rounded" style="width: 50px" alt="..."> </div>' +
        '<div id="userInput" class=" badge rounded-pill bg-primary" style="font-size:15px;">' + admin._adminname + '</div>' +
        '</div>';
    $('#adminList').append(item);
});



connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var node = document.getElementById("userInput");
    var user = node.textContent || node.innerText;
    var message = document.getElementById("messageInput").value;
    const img = $('#avatar').attr('src')
    connection.invoke("SendMessage", user, message, img).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});