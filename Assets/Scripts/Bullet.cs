using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private NetworkView view;
	public GameObject owner;

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
			
		if (transform.position.x < -30||transform.position.x > 30||
		    transform.position.z < -30||transform.position.z > 30) {
			Destroy (gameObject);
		}
	}
	
	void OnTriggerEnter(Collider coll)
	{
		Controller c = coll.gameObject.GetComponent<Controller> ();
		if (!c.isMine) {
			Destroy(coll.gameObject);
			Destroy(gameObject);
		}
	}

	[RPC]
	public void MoveBullet(Vector3 position){
		transform.position = position;
	}
}
