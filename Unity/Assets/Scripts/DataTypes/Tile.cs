using UnityEngine;
using System.Collections;

public class Tile {
	public TileType type;
	public Room room;
	public int indexInRoomX;
	public int indexInRoomY;
	
	public Tile(Room room, TileType type, int indexInRoomX, int indexInRoomY) {
		this.room = room;
		this.type = type;
		this.indexInRoomX = indexInRoomX;
		this.indexInRoomY = indexInRoomY;
	}

	public Tile(Room room, int indexInRoomX, int indexInRoomY) : this(room, TileType.NONE, indexInRoomX, indexInRoomY) {

	}

	public Tile() : this(null, TileType.NONE, -1, -1) {

	}
}
