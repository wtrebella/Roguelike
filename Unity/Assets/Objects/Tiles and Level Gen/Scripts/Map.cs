using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map {
	public const int maxMapHeight = 100;
	public const int mapWidth = 20;
	public const int mapHeight = 4;

	public List<Room> rooms;
	
	protected List<int> roomHashes;
	
	public Map() {
		//if (mapHeight > maxMapHeight) throw new UnityException("too many room columns");

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
				RoomType roomType = RoomType.NONE;
				if (y == 0) roomType = 0;
				else roomType = (RoomType)Random.Range(1, (int)RoomType.NONE);
				Room newRoom = new Room(this, roomType, x, y);
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

