"use strict";

var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/collegehub")
    .configureLogging(signalR.LogLevel.Information)
    .withAutomaticReconnect()
    .build();

// var hubConnection = new signalR.HubConnectionBuilder().withUrl("/collegehub").build();

$(document).ready(function () {
    collegeHub.Init();
});

var collegeHub =
{
    Init: async () => {

        await hubConnection.start().then(() => { console.log("SignalR connected."); })
        .catch(function (err) {
            // return console.log(err.toString());
            console.log(err.toString());
            setTimeout(( () => {
                collegeHub.Init();
            }), 5000);
        });

        // try {
        //     await hubConnection.start();
        //     console.log("SignalR connected.");
        // }
        // catch (err) {
        //     console.log(err);
        //     setTimeout(( () => {
        //         collegeHub.Init();
        //     }), 5000);
        // }

        hubConnection.on("ShowKafkaMessage", (messageTopic) => {
           console.log(messageTopic);
           let container = document.getElementById('mainContainer');
           container.innerHTML += `<li>${messageTopic}</li>`;
        });
    }
}