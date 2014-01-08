using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public EnemyType enemyType = EnemyType.NONE;
	public AudioClip killSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Kill() {
		GameObject.Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(killSound, Vector3.zero);
	}
}
