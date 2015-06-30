using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private NetworkView view;

	void Start () {
		view = GetComponent<NetworkView> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * 0.5f;
		view.RPC (
			"MoveBullet",
			RPCMode.Others,
			transform.position);
	}

	[RPC]
	public void MoveBullet(Vector3 position){
		transform.position = position;
	}
}
