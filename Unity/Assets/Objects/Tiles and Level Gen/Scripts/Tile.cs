using UnityEngine;
using System.Collections;

public class Tile {
	public TileData tileData;
	public Room room;
	public int indexInRoomX;
	public int indexInRoomY;
	
	public Tile(Room room, TileData tileData, int indexInRoomX, int indexInRoomY) {
		this.room = room;
		this.tileData = tileData;
		this.indexInRoomX = indexInRoomX;
		this.indexInRoomY = indexInRoomY;
	}

	public Tile(Room room, int indexInRoomX, int indexInRoomY) : this(room, null, indexInRoomX, indexInRoomY) {

	}

	public Tile() : this(null, null, -1, -1) {

	}
}
