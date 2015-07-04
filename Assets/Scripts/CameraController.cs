using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	private GameObject floor;

	// Use this for initialization
	void Start () {
		floor = GameObject.Find ("Floor");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = floor.transform.position + new Vector3 (0, 20, 0);
		transform.LookAt (floor.transform);
	}
}
