using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
	public int levelNum = 0;
	public GameObject pistolPrefab;

	protected Map map;
	protected TileManager tileManager;
	protected GameObject tileMap;
	protected Manager manager;
	protected Player player;

	void Awake() {
		tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
		tileMap = GameObject.Find("Tile Map");
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	void Start() {
		GenerateMapData();
		GenerateLevel();

		Room topLeftRoom = map.GetRoom(0, map.mapHeight - 1);

		List<Tile> emptyTiles = new List<Tile>();
		for (int y = 0; y < Room.roomHeight; y++) {
			for (int x = 0; x < Room.roomWidth; x++) {
				Tile tile = topLeftRoom.GetTile(x, y);
				if (tile.tileData.tileType == TileType.Empty) emptyTiles.Add(tile);
			}
			if (emptyTiles.Count > 0) break;
		}

		Tile randomEmptyTile = emptyTiles[Random.Range(0, emptyTiles.Count)];

		player.transform.position = GetTileOrigin(randomEmptyTile);
	}
	
	void Update() {
	
	}

	void GenerateMapData() {
		map = new Map(10, 4);
	}

	void GenerateLevel() {
		for (int x = 0; x < map.mapWidth; x++) {
			for (int y = 0; y < map.mapHeight; y++) {
				Room room = map.GetRoom(x, y);

				for (int tileX = 0; tileX < Room.roomWidth; tileX++) {
					for (int tileY = 0; tileY < Room.roomHeight; tileY++) {
						Tile tile = room.GetTile(tileX, tileY);
						Vector3 tileOrigin = GetTileOrigin(tile);

						if (tile.tileData.tileType == TileType.Ground) {
							Transform newTile = tileManager.GetNewTile(TileTheme.Grass, GroundTileType.Singular);
							newTile.name = string.Format("Room: (" + x + ", " + y + ") - Tile: (" + tileX + ", " + tileY + ")");
							newTile.position = tileOrigin;
							newTile.parent = tileMap.transform;
						}
						else if (tile.tileData.tileType == TileType.Gun) {
							TileDataGun tdg = (TileDataGun)tile.tileData;
							if (tdg.gunType != GunType.NONE) {
								Gun newGun = null;
								if (tdg.gunType == GunType.Pistol) newGun = ((GameObject)Instantiate(pistolPrefab)).GetComponent<Gun>();
								newGun.transform.parent = tileMap.transform;
								newGun.name = newGun.gunType.ToString();
								newGun.transform.position = tileOrigin;
							}
						}
					}
				}
			}
		}
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