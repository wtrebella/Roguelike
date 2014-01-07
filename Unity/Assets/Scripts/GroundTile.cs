using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {
	public AudioClip explosionSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Destroy() {
		GameObject.Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(explosionSound, Vector3.zero);
	}
}
