using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room {
	public const int roomWidth = 10;
	public const int roomHeight = 10;
	
	public int roomIndexX;
	public int roomIndexY;

	protected List<Tile> tiles;
	protected Map map;
	protected List<int> tileHashes;
	
	public Room(Map map, RoomType roomType, int roomIndexX, int roomIndexY) {
		this.roomIndexX = roomIndexX;
		this.roomIndexY = roomIndexY;
		this.map = map;
		
		tiles = new List<Tile>();
		tileHashes = new List<int>();
		
		GenerateTiles(roomType);
	}
	
	public Tile GetTile(int x, int y) {
		int tileIndex = GetTileIndex(x, y);
		if (tileIndex == -1) throw new UnityException("invalid tile");
		return tiles[tileIndex];
	}

	void AddTile(Tile tile) {
		tiles.Add(tile);
		tileHashes.Add(GetTileHash(tile.indexInRoomX, tile.indexInRoomY));
	}
	
	void GenerateTiles(RoomType roomType) {
		char[,] template = RoomTemplates.GetRoomTemplate(roomType);

		for (int x = 0; x < roomWidth; x++) {
			for (int y = 0; y < roomHeight; y++) {
				Tile tile = new Tile(this, x, y);

				tile.tileData = TileData.CharToTileData(template[x, y]);

				AddTile(tile);
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
