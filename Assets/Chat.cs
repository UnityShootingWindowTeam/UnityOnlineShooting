using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chat : MonoBehaviour {
	private List<string> messages = new List<string> ();
	private string inputMessage = "";
	private string name = "Nemoto";
	
	void OnGUI(){
		if (!NetworkViewManager.connected) {
			return;
		}

		GUILayout.Space (150);
		GUILayout.BeginHorizontal (GUILayout.Width (400));

		inputMessage = GUILayout.TextField (inputMessage);

		if (GUILayout.Button ("Send")) {
			NetworkView view = GetComponent<NetworkView>();
			view.RPC ("chatMessage", RPCMode.All, name + ":" + inputMessage);

			//messages.Add(inputMessage);

			//inputMessage = ""と同じ
			inputMessage = string.Empty;
		}
		GUILayout.EndHorizontal ();

//		List<string> mes = new List<string> (messages);
//		mes.Reverse ();

		messages.Reverse ();
		foreach (string s in messages) {
			GUILayout.Label (s);
		}
		//上と同じ処理
		//messages.ForEach (s => GUILayout.Label (s));
		messages.Reverse ();

		if (messages.Count > 10) {
			messages.RemoveAt (0);
		}

	}

	[RPC]
	public void chatMessage(string msg){
		messages.Add (msg);
	}
}
