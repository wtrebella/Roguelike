using UnityEngine;
using System.Collections;

public class TileManager : MonoBehaviour {
	public GameObject baseTilePrefab;

	protected Transform tileMap;
	protected SpriteManager spriteManager;

	void Awake() {
		spriteManager = GameObject.Find("Sprite Manager").GetComponent<SpriteManager>();
		tileMap = GameObject.Find("Tile Map").transform;
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
		newTile.transform.parent = tileMap;
		return newTile;
	}
}
