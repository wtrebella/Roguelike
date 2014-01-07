using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject baseTilePrefab;

	protected GameObject tileHolder;
	protected SpriteManager spriteManager;

	void Awake() {
		spriteManager = GameObject.Find("Sprite Manager").GetComponent<SpriteManager>();
		tileHolder = GameObject.Find("Tile Holder");
		if (!tileHolder) tileHolder = new GameObject("Tile Holder");
	}

	void Start() {
	
	}
	
	void Update() {
	
	}

	public Transform GetNewTile(TileTheme tileTheme, GroundTileType tileType) {
		Transform newTile = ((GameObject)Instantiate(baseTilePrefab)).transform;
		//newTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileTheme, tileType);
		newTile.gameObject.AddComponent<BoxCollider2D>();
		newTile.gameObject.layer = LayerMask.NameToLayer("Ground");
		newTile.transform.parent = tileHolder.transform;
		return newTile;
	}
}
