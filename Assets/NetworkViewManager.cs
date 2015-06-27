using UnityEngine;
using System.Collections;

public class NetworkViewManager : MonoBehaviour {

	//接続状況
	public static bool connected = false;

	//サーバーのIPアドレス
	public string connectionIP = "10.25.33.218";

	//接続に使うポート番号
	public int portNumber = 8080;
	
	void OnGUI(){
		//サーバー用
		GUILayout.Label ("Connections:" + Network.connections.Length.ToString ());

		if (connected) {
			if (GUILayout.Button ("Disconnect")) {
				Network.Disconnect ();
			}
		} else {
			if (GUILayout.Button ("Connect")) {
				Network.Connect (connectionIP, portNumber);
			}

			//サーバー用
			if(GUILayout.Button("Server")){
				Network.InitializeServer (20, portNumber);
			}
		}
	}

	//プレイヤーが接続されたときサーバー側で呼び出される
	void OnPlayerConnected(NetworkPlayer player){
		Debug.Log ("Connected from" + player.ipAddress + ":" + player.port);
		connected = true;
	}

	void OnServerInitialized(){
		Debug.Log ("Server initialized and ready");
		connected = true;
	}

	//サーバーに接続したときクライアント側で呼び出される
	void OnConnectedToServer(){
		Debug.Log ("Connected to server");
		connected = true;
	}

	//サーバーから切断されたときにクライアントで呼び出される
	void OnDisconnectedFromServer(NetworkDisconnection info){
		Debug.Log ("Disconnected from the server");
		connected = false;
	}
}
