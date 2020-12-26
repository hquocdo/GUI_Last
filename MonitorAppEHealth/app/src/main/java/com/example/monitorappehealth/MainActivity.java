package com.example.monitorappehealth;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.NotificationCompat;

import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.jjoe64.graphview.GraphView;
import com.jjoe64.graphview.GridLabelRenderer;
import com.jjoe64.graphview.series.DataPoint;
import com.jjoe64.graphview.series.LineGraphSeries;

import org.eclipse.paho.client.mqttv3.IMqttDeliveryToken;
import org.eclipse.paho.client.mqttv3.MqttCallbackExtended;
import org.eclipse.paho.client.mqttv3.MqttMessage;
import org.apache.commons.codec.binary.Base64;

import java.nio.channels.Channel;
import java.util.Timer;
import java.util.TimerTask;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;
import java.security.Key;

public class MainActivity extends AppCompatActivity {
	GraphView graphBPM, graphSPO2;
	MQTTHelper mqttHelper;
	LineGraphSeries<DataPoint> seriesBPM, seriesSPO2;

	LinearLayout BPMshowLayout, SPO2showLayout, TemptshowLayout;
	EditText mqttID;
	TextView showFieldBPM, showFieldSPO2, showFieldTempt, showAvaliability;
	Button btnConnect;

	double iBPM = 3, iSPO2 = 3;
	String topicMQTT, CHANNEL_ID = "1";
	String[] mqttIdArray;
	boolean avaliabity = false;

	public static String DeKey = "1111111111111111";
	public static byte[] Dekey_Array = Base64.decodeBase64(DeKey.getBytes());

	String test = "";

	private void startMQTT(String topic){
		mqttHelper = new MQTTHelper(getApplicationContext(), topic);
		mqttHelper.connect();
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
				//Log.d("mes", mqttMessage.toString());
				if (topic.equals(topicMQTT)) {
					String decrypt = decrypt(mqttMessage.toString());
					Log.d("a", "in" + mqttMessage);
					Pattern pattern = Pattern.compile("^([0-9]*)-([0-9]*)-([0-9.]*)$");
					Log.d("AES", decrypt);
					Matcher m = pattern.matcher(decrypt);

					if(m.find()){
						if(graphSPO2.getSeries().size() > 2 || graphBPM.getSeries().size() > 2){
							graphBPM.removeAllSeries();
							graphSPO2.removeAllSeries();
							initialSeries();
						}

						showFieldBPM.setText(m.group(1));
						showFieldSPO2.setText(m.group(2));
						showFieldTempt.setText(m.group(3));

						double dataBPM = Double.parseDouble(m.group(1));
						Log.d("group1", "group1: " + m.group(1));
						setData(seriesBPM, dataBPM, iBPM, graphBPM);
						iBPM++;

						double dataSPO2 = Double.parseDouble(m.group(2));
						Log.d("group2", "group2: " + m.group(2) + " " + iSPO2 + " " + iBPM);
						setData(seriesSPO2, dataSPO2, iSPO2, graphSPO2);
						iSPO2++;
					}
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

		BPMshowLayout = findViewById(R.id.BPMshow);
		SPO2showLayout = findViewById(R.id.SPO2show);
		TemptshowLayout = findViewById(R.id.Temptshow);

		showFieldBPM = findViewById(R.id.testBPM);
		showFieldSPO2 = findViewById(R.id.testSPO2);
		showFieldTempt = findViewById(R.id.testTempt);

		graphBPM = findViewById(R.id.graphBPM);

		graphBPM.getViewport().setMinY(0);
		graphBPM.getViewport().setMaxY(200);
		graphBPM.getViewport().setYAxisBoundsManual(true);
		GridLabelRenderer gridLabelBPM = graphBPM.getGridLabelRenderer();
		gridLabelBPM.setHorizontalAxisTitleColor(Color.WHITE);
		gridLabelBPM.setHorizontalAxisTitleTextSize(35);
		gridLabelBPM.setHorizontalAxisTitle("BPM");

		graphSPO2 = findViewById(R.id.graphSPO2);
		graphSPO2.getViewport().setMinY(0);
		graphSPO2.getViewport().setMaxY(100);
		graphSPO2.getViewport().setYAxisBoundsManual(true);
		GridLabelRenderer gridLabelSPO2 = graphSPO2.getGridLabelRenderer();
		gridLabelSPO2.setHorizontalAxisTitleColor(Color.WHITE);
		gridLabelSPO2.setHorizontalAxisTitleTextSize(35);
		gridLabelSPO2.setHorizontalAxisTitle("SPO2");

		mqttIdArray = getResources().getStringArray(R.array.mqttId);

		showAvaliability = findViewById(R.id.Availability);
		mqttID = findViewById(R.id.mqttID);
		btnConnect = findViewById(R.id.BtnConnect);
		btnConnect.setOnClickListener(new View.OnClickListener() {
			@Override
			public void onClick(View v) {
				String mqttIDtest = mqttID.getText().toString();

				for(int i = 0;i < mqttIdArray.length;i++){
					if(mqttIdArray[i].equals(mqttIDtest)){
						topicMQTT = "mqtt" + mqttIDtest;
						showAvaliability.setText("Avaliable");
						avaliabity = true;
						break;
					}
					else{
						avaliabity = false;
						showAvaliability.setText("Not Avaliable");
						BPMshowLayout.setVisibility(View.GONE);
						graphBPM.setVisibility(View.GONE);
						SPO2showLayout.setVisibility(View.GONE);
						graphSPO2.setVisibility(View.GONE);
						TemptshowLayout.setVisibility(View.GONE);
					}
				}

				if(avaliabity){
					BPMshowLayout.setVisibility(View.VISIBLE);
					graphBPM.setVisibility(View.VISIBLE);
					SPO2showLayout.setVisibility(View.VISIBLE);
					graphSPO2.setVisibility(View.VISIBLE);
					TemptshowLayout.setVisibility(View.VISIBLE);
					startMQTT(topicMQTT);
				}
			}
		});
		initialSeries();

		NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this, CHANNEL_ID).setSmallIcon(R.drawable.ic_notification)
				.setContentTitle("My notification")
				.setContentText("Much longer text that cannot fit one line...")
				.setStyle(new NotificationCompat.BigTextStyle()
						.bigText("Much longer text that cannot fit one line..."))
				.setPriority(NotificationCompat.PRIORITY_DEFAULT);
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

	private void initialSeries(){
		seriesBPM = new LineGraphSeries<>(new DataPoint[]{
				new DataPoint(0, 0)
		});
		seriesBPM.appendData(new DataPoint(1, 0), false, 20);
		setData(seriesBPM, 0, 2, graphBPM);

		seriesSPO2 = new LineGraphSeries<>(new DataPoint[]{
				new DataPoint(0, 0)
		});
		seriesSPO2.appendData(new DataPoint(1, 0), false, 20);
		setData(seriesSPO2, 0, 2, graphSPO2);
	}

	private void createNotificationChannel(){
		if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.O){

		}
	}

	public static String decrypt(String EncryptedMessage){
		try{
			Cipher _Cipher = Cipher.getInstance("AES");

			byte[] iv = { 1, 2, 3, 4, 5, 6, 6, 5, 4, 3, 2, 1, 7, 7, 7, 7 };
			IvParameterSpec ivspec = new IvParameterSpec(iv);

			Key SercetKey = new SecretKeySpec(DeKey.getBytes(), "AES");
			_Cipher.init(Cipher.DECRYPT_MODE, SercetKey, ivspec);
			byte DecodedMessage[] = Base64.decodeBase64(EncryptedMessage.getBytes());

			return new String(_Cipher.doFinal(DecodedMessage));
		}
		catch(Exception exp){
			Log.d("AES", "[Exception]:" + exp.getMessage());
		}
		return null;
	}
}
