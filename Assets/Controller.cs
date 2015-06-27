using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public bool isMine;
	
	// Update is called once per frame
	void Update () {
		if (!NetworkViewManager.connected) {
			return;
		}

		if(isMine){
			float dx = Input.GetAxis ("Horizontal");
			float dy = Input.GetAxis ("Vertical");
			transform.Translate (dx*0.5f,dy*0.5f, 0);

			GetComponent<NetworkView>().RPC (
				"MovePlayer",
				RPCMode.Others,
				transform.position);
		}
	}

	[RPC]
	public void MovePlayer(Vector3 position){
		transform.position = position;
	}
}
