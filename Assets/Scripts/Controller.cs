using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public bool isMine;
	public GameObject bullet;

	void Start(){
		bullet = (GameObject)Resources.Load ("Bullet");
	}
	
	// Update is called once per frame
	void Update () {
		if (!NetworkViewManager.connected) {
			return;
		}

		if(isMine){
			float dx = Input.GetAxis ("Horizontal");
			float dy = Input.GetAxis ("Vertical");
			//transform.Translate (dx*0.5f,dy*0.5f, 0);
			transform.RotateAround (transform.position, transform.up, dx*1.5f);
			transform.position += transform.forward*dy*0.5f;

			GetComponent<NetworkView>().RPC (
				"MovePlayer",
				RPCMode.Others,
				transform.position);

			if (Input.GetKeyDown (KeyCode.Space)) {
				GetComponent<NetworkView> ().RPC (
					"Shoot",
					RPCMode.All,
					GetComponent<NetworkView> ().viewID,
					transform.position,
					transform.rotation);
			}
		}
	}

	[RPC]
	public void MovePlayer(Vector3 position){
		transform.position = position;
	}

	[RPC]
	public void Shoot(NetworkViewID viewID, Vector3 position, Quaternion rotation){
		Bullet b = ((GameObject)Instantiate (bullet, position, rotation)).GetComponent<Bullet> ();
		b.ownerID = viewID;
	}
}
