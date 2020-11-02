package com.example.monitorappehealth;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import org.eclipse.paho.client.mqttv3.IMqttDeliveryToken;
import org.eclipse.paho.client.mqttv3.MqttCallbackExtended;
import org.eclipse.paho.client.mqttv3.MqttMessage;

public class MainActivity extends AppCompatActivity {
    GraphView graphTemperature;
    MQTTHelper mqttHelper;
    LineGraphSeries<DataPoint> seriesTemp;
    double iT = 3;

    EditText mqttID;
    TextView showField, showAvaliability;
    Button btnConnect;
    String topicMQTT;

    String[] mqttIdArray;
    int count = 0;
    boolean avaliabity = false;

    private void startMQTT(String topic){
        mqttHelper = new MQTTHelper(getApplicationContext(), topic);
        mqttHelper.setCallback(new MqttCallbackExtended() {
            @Override
            public void connectComplete(boolean b, String s) {

            }

            @Override
            public void connectionLost(Throwable throwable) {

            }

            @Override
            public void messageArrived(String topic, MqttMessage mqttMessage) throws Exception {

                    Log.d("MQTT","sub " + topic);

                    if (topic.equals(topicMQTT)) {
                        Log.d("a", "in" + mqttMessage);
                        /*if(count == 50) {
                            count = 0;*/
                            showField.setText(mqttMessage.toString());

                            double data = Double.parseDouble(mqttMessage.toString());

                            setData(seriesTemp, data, iT, graphTemperature);
                            iT++;
                        //}
                        //count++;
                        /*if(count == 50){
                            Log.d("a1", "in loop" + mqttMessage);
                            double data2 = Double.parseDouble(mqttMessage.toString());
                            Log.d("a2", "sau parse data");
                            seriesTemp.appendData(new DataPoint(iT, data), false, 20);
                            Log.d("b","set");
                            showDataOnGraph(seriesTemp, graphTemperature);
                            Log.d("c", "show");
                            iT++;
                            count = 0;
                        }
                        count++;*/
                }
            }

            @Override
            public void deliveryComplete(IMqttDeliveryToken iMqttDeliveryToken) {

            }
        });
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        graphTemperature = findViewById(R.id.graphTemperature);

        graphTemperature.getViewport().setMinY(0);
        graphTemperature.getViewport().setMaxY(200);
        graphTemperature.getViewport().setYAxisBoundsManual(true);

        mqttIdArray = getResources().getStringArray(R.array.mqttId);

        showField = findViewById(R.id.test);
        showAvaliability = findViewById(R.id.Availability);
        mqttID = findViewById(R.id.mqttID);
        btnConnect = findViewById(R.id.BtnConnect);
        btnConnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String mqttIDtest = mqttID.getText().toString();

                for(int i = 0;i < mqttIdArray.length;i++){
                    if(mqttIdArray[i].equals(mqttIDtest)){
                        topicMQTT = "mqtt" + mqttIDtest + "/BPM";
                        showAvaliability.setText("Avaliable");
                        avaliabity = true;
                        break;
                    }
                    else{
                        avaliabity = false;
                        showAvaliability.setText("Not Avaliable");
                    }
                }

                if(avaliabity){
                    startMQTT(topicMQTT);
                }
            }
        });

        seriesTemp = new LineGraphSeries<>(new DataPoint[]{
                new DataPoint(0, 0)
        });
        seriesTemp.appendData(new DataPoint(1, 0), false, 20);
        setData(seriesTemp, 0, 2, graphTemperature);
    }

    public void setData(LineGraphSeries<DataPoint> series, double data, double index, GraphView graph){
        series.appendData(new DataPoint(index, data), false, 20);
        showDataOnGraph(series, graph);
    }

    public void showDataOnGraph(LineGraphSeries<DataPoint> series, GraphView graph) {
        if (graph.getSeries().size() > 0) {
            graph.getSeries().remove(0);
        }
        graph.addSeries(series);
        series.setDrawDataPoints(true);
        series.setDataPointsRadius(10);
    }
}
