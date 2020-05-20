"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/measurementHub").build();

connection.start().catch(funtion(e)){});

connection.on("NewMeasurements", function (time, name, lat, lon, temperature, humidity, airPressure) {
    var measurements = "Time :" + time + ", " +
        "Name: " + name + ", " +
        "Lat: " + lat + ", " +
        "Lon: " + lon + ", " +
        "Temperature: " + temperature + ", " +
        "Humidity: " + humidity + ", " +
        "Pressure: " + airPressure;
    var li = document.createElement("li");
    li.textContent = m;
    document.getElementById("measurementList").appendChild(li);
});