package com.example.monitorappehealth;

import android.content.Context;
import android.util.Log;

import org.eclipse.paho.android.service.MqttAndroidClient;
import org.eclipse.paho.client.mqttv3.DisconnectedBufferOptions;
import org.eclipse.paho.client.mqttv3.IMqttActionListener;
import org.eclipse.paho.client.mqttv3.IMqttDeliveryToken;
import org.eclipse.paho.client.mqttv3.IMqttToken;
import org.eclipse.paho.client.mqttv3.MqttCallbackExtended;
import org.eclipse.paho.client.mqttv3.MqttConnectOptions;
import org.eclipse.paho.client.mqttv3.MqttException;
import org.eclipse.paho.client.mqttv3.MqttMessage;

public class MQTTHelper {
    final String serverUri = "tcp://13.229.69.47:1883";
    final String clientID = "hung";
    String subscriptionTopic = "";
    final String username = "hquocdo";
    final String password = "hung11898";

    public MqttAndroidClient mqttAndroidClient;
    public MQTTHelper(Context context,String topic){
        subscriptionTopic = topic;
        mqttAndroidClient = new MqttAndroidClient(context,serverUri,clientID);
        mqttAndroidClient.setCallback(new MqttCallbackExtended() {
            @Override
            public void connectComplete(boolean b, String s) {
                Log.w("mqtt",s);
            }

            @Override
            public void connectionLost(Throwable throwable) {

            }

            @Override
            public void messageArrived(String topic, MqttMessage mqttMessage) throws Exception {
                Log.w("Mqtt",mqttMessage.toString());
            }

            @Override
            public void deliveryComplete(IMqttDeliveryToken iMqttDeliveryToken) {

            }
        });
        connect();
    }

    public void setCallback(MqttCallbackExtended callback){
        mqttAndroidClient.setCallback(callback);
    }
    private void connect(){
        final MqttConnectOptions mqttConnectOptions = new MqttConnectOptions();
        mqttConnectOptions.setAutomaticReconnect(true);
        mqttConnectOptions.setCleanSession(false);
        mqttConnectOptions.setUserName(username);
        mqttConnectOptions.setPassword(password.toCharArray());

        try {
            mqttAndroidClient.connect(mqttConnectOptions, null, new IMqttActionListener() {
                @Override
                public void onSuccess(IMqttToken asyncActionToken) {
                    DisconnectedBufferOptions disconnectedBufferOptions = new DisconnectedBufferOptions();
                    disconnectedBufferOptions.setBufferEnabled(true);
                    disconnectedBufferOptions.setBufferSize(100);
                    disconnectedBufferOptions.setPersistBuffer(false);
                    disconnectedBufferOptions.setDeleteOldestMessages(false);
                    mqttAndroidClient.setBufferOpts(disconnectedBufferOptions);
                    subscribeToTopic();
                }
                @Override
                public void onFailure(IMqttToken asyncActionToken, Throwable exception) {
                    Log.w("Mqtt","Failed to connect to: " + serverUri + exception.toString());
                }
            });
        } catch (MqttException ex) {
            ex.printStackTrace();
        }

    }
    private void subscribeToTopic(){
        try{
            mqttAndroidClient.subscribe(subscriptionTopic, 0, null, new IMqttActionListener() {
                @Override
                public void onSuccess(IMqttToken asyncActionToken) {
                    Log.w("Mqtt","Subcribed");

                }

                @Override
                public void onFailure(IMqttToken asyncActionToken, Throwable exception) {
                    Log.w("Mqtt","Subcribed Failed");

                }
            });
        } catch (MqttException ex) {
            System.err.println("Exceptionist Subscribing");
            ex.printStackTrace();
        }
    }
}
