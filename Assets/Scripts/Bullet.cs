using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public GameObject myobj;
	private NetworkView view;

	void Start () {
		view = GetComponent<NetworkView> ();
		myobj = GetComponent<GameObject> ();
	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * 0.5f;
		view.RPC (
			"MoveBullet",
			RPCMode.Others,
			transform.position);

		view.RPC (
			"OnCollisionEnter",
			RPCMode.All,
			gameObject.GetComponent<SphereCollider> ());
			
		if (transform.position.x < -30||transform.position.x > 30||
		    transform.position.z < -30||transform.position.z > 30) {
			Destroy (gameObject);
		}
	}

	[RPC]
	public void MoveBullet(Vector3 position){
		transform.position = position;
	}

	[RPC]
	public void OnCollisionEnter(Collision coll)
	{
		Controller c = coll.gameObject.GetComponent<Controller> ();
		if (!c.isMine) {
			Destroy(coll.gameObject);
		}
	}
}
