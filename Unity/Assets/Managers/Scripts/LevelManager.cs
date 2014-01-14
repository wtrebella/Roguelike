using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public int levelNum = 0;
	public GameObject pistolPrefab;
	public GameObject turtlePrefab;
	public GameObject ratPrefab;
	public GameObject borderPiecePrefab;

	protected Map map;
	protected TileManager tileManager;
	protected GameObject tileHolder;
	protected GameObject enemyHolder;
	protected Manager manager;
	protected Player player;

	void Awake() {
		tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();

		tileHolder = GameObject.Find("Tile Holder");
		if (!tileHolder) tileHolder = new GameObject("Tile Holder");

		enemyHolder = GameObject.Find("Enemy Holder");
		if (!enemyHolder) enemyHolder = new GameObject("Enemy Holder");

		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	void Start() {
		GenerateMapData();
		GenerateLevel();
		CreateBorder();

		// place player
		Room topLeftRoom = map.GetRoom(0, Map.mapHeight - 1);

		List<Tile> emptyTiles = new List<Tile>();
		for (int y = 0; y < Room.roomHeight; y++) {
			for (int x = 0; x < Room.roomWidth; x++) {
				Tile tile = topLeftRoom.GetTile(x, y);
				if (tile.tileData.tileType == TileType.Empty) emptyTiles.Add(tile);
			}
			if (emptyTiles.Count > 0) break;
		}

		Tile randomEmptyTile = emptyTiles[Random.Range(0, emptyTiles.Count)];

		Vector3 newPos = GetTileOrigin(randomEmptyTile);
		newPos.y += manager.tileSize / 2f;
		player.transform.position = newPos;
	}
	
	void Update() {
	
	}

	void GenerateMapData() {
		map = new Map();
	}
	
	void GenerateLevel() {
		for (int x = 0; x < Map.mapWidth; x++) {
			for (int y = 0; y < Map.mapHeight; y++) {
				Room room = map.GetRoom(x, y);

				for (int tileX = 0; tileX < Room.roomWidth; tileX++) {
					for (int tileY = 0; tileY < Room.roomHeight; tileY++) {
						Tile tile = room.GetTile(tileX, tileY);
						Vector3 tileOrigin = GetTileOrigin(tile);

						if (tile.tileData.tileType == TileType.Ground) {
							Transform newTile = tileManager.GetNewTile(TileTheme.Grass, GroundTileType.Singular);
							float rVal = Random.value;
							float cVal = 1.0f - rVal * 0.3f;
							newTile.GetComponent<SpriteRenderer>().color = new Color(cVal, cVal, cVal);
							newTile.name = string.Format("Room: (" + x + ", " + y + ") - Tile: (" + tileX + ", " + tileY + ")");
							newTile.position = tileOrigin;
							newTile.parent = tileHolder.transform;
						}
						else if (tile.tileData.tileType == TileType.Weapon) {
							TileDataWeapon tdw = (TileDataWeapon)tile.tileData;
							if (tdw.weaponType != WeaponType.NONE) {
								Weapon newWeapon = null;
								if (tdw.weaponType == WeaponType.Pistol) newWeapon = ((GameObject)Instantiate(pistolPrefab)).GetComponent<Weapon>();
								newWeapon.transform.rotation = Quaternion.identity;
								newWeapon.transform.parent = tileHolder.transform;
								newWeapon.name = newWeapon.weaponType.ToString();
								newWeapon.transform.position = tileOrigin;
							}
						}
						else if (tile.tileData.tileType == TileType.Enemy) {
							TileDataEnemy tde = (TileDataEnemy)tile.tileData;
							if (tde.enemyType != EnemyType.NONE) {
								AbstractEnemy newEnemy = null;

								if (tde.enemyType == EnemyType.Turtle) newEnemy = ((GameObject)Instantiate(turtlePrefab)).GetComponent<AbstractEnemy>();
								else if (tde.enemyType == EnemyType.Rat) newEnemy = ((GameObject)Instantiate(ratPrefab)).GetComponent<AbstractEnemy>();

								newEnemy.transform.rotation = Quaternion.identity;
								newEnemy.transform.parent = enemyHolder.transform;
								newEnemy.transform.position = tileOrigin;
								newEnemy.name = newEnemy.enemyType.ToString();
							}
						}
					}
				}
			}
		}
	}

	void CreateBorder() {
		float mapWidthInWorldUnits = manager.tileSize * Room.roomWidth * Map.mapWidth;
		float mapHeightInWorldUnits = manager.tileSize * Room.roomHeight * Map.mapHeight;

		GameObject bottomBorderPiece = (GameObject)Instantiate(borderPiecePrefab);
		GameObject topBorderPiece = (GameObject)Instantiate(borderPiecePrefab);
		GameObject rightBorderPiece = (GameObject)Instantiate(borderPiecePrefab);
		GameObject leftBorderPiece = (GameObject)Instantiate(borderPiecePrefab);

		float mult = 16;
		float padding = 10;

		float bottomAndTopPieceWidth = mult * (mapWidthInWorldUnits + padding * 2);
		float bottomAndTopPieceHeight = mult * padding;
		float leftAndRightPieceWidth = mult * padding;
		float leftAndRightPieceHeight = mult * mapHeightInWorldUnits;

		bottomBorderPiece.transform.localScale = topBorderPiece.transform.localScale = new Vector3(bottomAndTopPieceWidth, bottomAndTopPieceHeight, 1);
		rightBorderPiece.transform.localScale = leftBorderPiece.transform.localScale = new Vector3(leftAndRightPieceWidth, leftAndRightPieceHeight, 1);

		leftBorderPiece.transform.position = new Vector3(-padding / 2f, mapHeightInWorldUnits / 2f, 0);
		rightBorderPiece.transform.position = new Vector3(mapWidthInWorldUnits + padding / 2f, mapHeightInWorldUnits / 2f, 0);
		topBorderPiece.transform.position = new Vector3(mapWidthInWorldUnits / 2f, mapHeightInWorldUnits + padding / 2f, 0);
		bottomBorderPiece.transform.position = new Vector3(mapWidthInWorldUnits / 2f, -padding / 2f, 0);
	}

	Vector3 GetTileOrigin(Room room, int tileX, int tileY) {
		return new Vector3((Room.roomWidth * room.roomIndexX + tileX + 0.5f) * manager.tileSize, (Room.roomHeight * room.roomIndexY + tileY + 0.5f) * manager.tileSize, 0);
	}

	Vector3 GetTileOrigin(Tile tile) {
		return GetTileOrigin(tile.room, tile.indexInRoomX, tile.indexInRoomY);
	}
	
	// ====1====
	// |       |
	// 8       2
	// |       |
	// ====4====

//	TileType GetTileTypeForData(int xIndex, int yIndex) {
//		int bitmask = 0;
//
//		if (xIndex - 1 >= 0) {
//			if (map[xIndex, yIndex] != 0) bitmask |= 8;
//		}
//
//		if (xIndex + 1 < map.GetLength(0)) {
//			if (map[xIndex, yIndex] != 0) bitmask |= 2;
//		}
//
//		if (yIndex - 1 >= 0) {
//			if (map[xIndex, yIndex] != 0) bitmask |= 4;
//		}
//		
//		if (yIndex + 1 < map.GetLength(1)) {
//			if (map[xIndex, yIndex] != 0) bitmask |= 1;
//		}
//
//		return TileType.Singular;
//
////		if (bitmask == 0) return TileType.NONE;
////		if (bitmask == 1) return TileType.CenterRounded;
////		if (bitmask == 2) return (TileType)RXRandom.Select(TileType.Left, TileType.CliffLeft);
////		if (bitmask == 3) return TileType.Center;
////		if (bitmask == 4) return 
//	}
}