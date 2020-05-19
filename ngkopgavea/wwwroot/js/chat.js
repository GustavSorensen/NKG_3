"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().catch(function (e) {
});

connection.on("ReceiveMeasurement", function (time, name, lat, lon, temp, humid, pressure) {
    var m = "Time :" + time + ", " +
        "Name: " + name + ", " +
        "Lat: " + lat + ", " +
        "Lon: " + lon + ", " +
        "Temperature: " + temp + ", " +
        "Humidity: " + humid + ", " +
        "Pressure: " + pressure;
    var li = document.createElement("li");
    li.textContent = m;
    document.getElementById("measurementList").appendChild(li);
});

/*//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
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
});*/