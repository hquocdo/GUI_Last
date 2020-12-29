document.getElementById("BPM").innerHTML = "BPM";
document.getElementById("SPO2").innerHTML = "SPO2";
document.getElementById("temp").innerHTML = "Body Temperature";


var dpsBPM = [], dpsSPO2 = [];
var chartBPM = new CanvasJS.Chart("BPMchartContainer", {
	title :{
		text: "BPM"
	},
	data: [{
		type: "line",
		dataPoints: dpsBPM
	}]
});
var chartSPO2 = new CanvasJS.Chart("SPO2chartContainer", {
	title :{
		text: "SPO2"
	},
	data: [{
		type: "line",
		dataPoints: dpsSPO2
	}]
});

var xValBPM = 1, xValSPO2 = 1;
var dataLength = 20; // number of dataPoints visible at any point

var updateChart = function (count, xvalue, value, array, chart) {

	count = count || 1;

	for (var j = 0; j < count; j++) {
		//yVal = yVal +  Math.round(5 + Math.random() *(-5-5));
		array.push({
			x: xvalue,
			y: value
		});
	}

	if (array.length > dataLength) {
		array.shift();
	}

	chart.render();
};

updateChart(1, 0, 0, dpsBPM, chartBPM);
updateChart(1, 0, 0, dpsSPO2, chartSPO2);

function startConnect() {
    // Generate a random client ID
    clientID = "clientID-" + parseInt(Math.random() * 100);

    // Initialize new Paho client connection
    client = new Paho.MQTT.Client("13.229.80.211", Number(9001), clientID);

    // Set callback handlers
    client.onConnectionLost = onConnectionLost;
    client.onMessageArrived = onMessageArrived;

    // Connect the client, if successful, call onConnect function
    client.connect({ 
        onSuccess: onConnect,
    });
}

// Called when the client connects
function onConnect() {
    // Fetch the MQTT topic from the form
    topic = "mqttraw" + document.getElementById("deviceID").value;

    // Subscribe to the requested topic
    client.subscribe(topic);
}

// Called when the client loses its connection
function onConnectionLost(responseObject) {
    console.log("onConnectionLost: Connection Lost");
    if (responseObject.errorCode !== 0) {
        console.log("onConnectionLost: " + responseObject.errorMessage);
    }
}

// Called when a message arrives
function onMessageArrived(message) {
    console.log("onMessageArrived: " + message.payloadString);
    var patt = /^([0-9]*)-([0-9]*)-([0-9.]*)/i;
    var result = message.payloadString.match(patt);

    document.getElementById("BPM").innerHTML = result[1];
    document.getElementById("SPO2").innerHTML = result[2];
    document.getElementById("temp").innerHTML = result[3];
    updateChart(1, xValBPM, parseInt(result[1]), dpsBPM, chartBPM);
    updateChart(1, xValSPO2, parseInt(result[2]), dpsSPO2, chartSPO2);
    xValBPM++;
    xValSPO2++;
}

// Called when the disconnection button is pressed
function startDisconnect() {
    client.disconnect();
    document.getElementById("messages").innerHTML += '<span>Disconnected</span><br/>';
    updateScroll(); // Scroll to bottom of window
}