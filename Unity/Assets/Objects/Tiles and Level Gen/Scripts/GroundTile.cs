using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour {
	public AudioClip explosionSound;
	public float health = 3;

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

	public void HitWithBullet(Bullet bullet) {
		health = Mathf.Max(health - bullet.power, 0);

		if (health == 0) Destroy();
	}
}
