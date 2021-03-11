const crypto = require('crypto');
const express = require('express');
const { createServer } = require('http');
const WebSocket = require('ws');

const app = express();

const server = createServer(app);
const wss = new WebSocket.Server({ server });

wss.on('connection', function(ws) {
  console.log("client joined.");

//首次连接，主动推消息给客户端
  ws.send("you have connected on websocket server!") 
  
  ws.on('message', function(data) {
    if (typeof(data) === "string") {
      // client sent a string
      console.log("string received from client -> '" + data + "'");
      ws.send("websocket server received you message " + data) 
    } else {
      console.log("binary received from client -> " + Array.from(data).join(", ") + "");
      ws.send("websocket server received you message  " + Array.from(data).join(", ") + "") 
    }
  });

  ws.on('close', function() {
    console.log("client left.");
  });
});

server.listen(8099, function() {
	
  console.log('Listening on http://localhost:8080');
});
