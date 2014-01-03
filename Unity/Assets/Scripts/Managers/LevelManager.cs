using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile {
	public char val;
	public Room room;
	public int indexInRoomX;
	public int indexInRoomY;

	public Tile(Room room, char val, int indexInRoomX, int indexInRoomY) {
		this.room = room;
		this.val = val;
		this.indexInRoomX = indexInRoomX;
		this.indexInRoomY = indexInRoomY;
	}
}

public class Room {
	public const int roomWidth = 10;
	public const int roomHeight = 6;

	public int roomIndexX;
	public int roomIndexY;
	public List<Tile> tiles;

	protected Map map;
	protected List<int> tileHashes;

	public Room(Map map, int roomIndexX, int roomIndexY) {
		this.roomIndexX = roomIndexX;
		this.roomIndexY = roomIndexY;
		this.map = map;

		tiles = new List<Tile>();
		tileHashes = new List<int>();

		GenerateTiles();
	}

	public Tile GetTile(int x, int y) {
		int tileIndex = GetTileIndex(x, y);
		if (tileIndex == -1) throw new UnityException("invalid tile");
		return tiles[tileIndex];
	}

	void GenerateTiles() {
		for (int x = 0; x < roomWidth; x++) {
			for (int y = 0; y < roomHeight; y++) {
				tileHashes.Add(GetTileHash(x, y));

				if (x == 0 || y == 0 || (roomIndexX == map.mapWidth - 1 && x == roomWidth - 1) || (roomIndexY == map.mapHeight - 1 && y == roomHeight - 1)) {
					tiles.Add(new Tile(this, '1', x, y));
				}
				else tiles.Add(new Tile(this, '0', x, y));
			}
		}
	}

	public int GetTileHash(int x, int y) {
		return (y + roomHeight / 2) + (x + roomHeight / 2) * roomHeight;
	}
	
	public int GetTileIndex(int x, int y) {
		return tileHashes.IndexOf(GetTileHash(x, y));
	}
}

public class Map {
	public const int maxMapHeight = 50;

	public int mapWidth;
	public int mapHeight;
	public List<Room> rooms;

	protected List<int> roomHashes;

	public Map(int mapWidth, int mapHeight) {
		if (mapHeight > maxMapHeight) throw new UnityException("too many room columns");

		this.mapWidth = mapWidth;
		this.mapHeight = mapHeight;

		rooms = new List<Room>();
		roomHashes = new List<int>();

		GenerateRooms();
	}

	public Room GetRoom(int x, int y) {
		int roomIndex = GetRoomIndex(x, y);
		if (roomIndex == -1) throw new UnityException("invalid room");
		return rooms[roomIndex];
	}

	void GenerateRooms() {
		for (int x = 0; x < mapWidth; x++) {
			for (int y = 0; y < mapHeight; y++) {
				Room newRoom = new Room(this, x, y);
				rooms.Add(newRoom);
				roomHashes.Add(GetRoomHash(x, y));
			}
		}
	}

	public int GetRoomHash(int x, int y) {
		return (y + maxMapHeight / 2) + (x + maxMapHeight / 2) * maxMapHeight;
	}
	
	public int GetRoomIndex(int x, int y) {
		return roomHashes.IndexOf(GetRoomHash(x, y));
	}
}

public class LevelManager : MonoBehaviour {
	public int levelNum = 0;

	protected Map map;
	protected TileManager tileManager;
	protected Manager manager;
	protected Player player;

	void Awake() {
		tileManager = GameObject.Find("Tile Manager").GetComponent<TileManager>();
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
				if (tile.val == '0') emptyTiles.Add(tile);
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

						if (tile.val == '1') {
							Transform newTile = tileManager.GetNewTile(TileTheme.Grass, TileType.Singular);
							newTile.name = string.Format("Room: (" + x + ", " + y + ") - Tile: (" + tileX + ", " + tileY + ")");
							Vector3 tileOrigin = GetTileOrigin(tile);
							newTile.position = tileOrigin;
							
							newTile.parent = GameObject.Find("Tile Map").transform;
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
