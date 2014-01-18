using UnityEngine;
using System.Collections;

public class RoomTemplates {

	public static char[,] GetRoomTemplate(int type) {
		string template = null;
		
		if (type == 1) {
			int rand = Random.Range(0, 5);

			if (rand == 0) {
				template = "" +
						"0 0 2 2 0 0 0 0 0 0" +
						"0 0 0 0 0 0 2 2 0 0" +
						"0 0 0 0 0 2 2 0 0 0" +
						"0 0 0 0 3 1 2 0 0 0" +
						"0 0 0 3 1 1 2 0 0 0" +
						"0 0 4 4 4 5 5 5 0 0" +
						"0 0 4 4 4 5 5 5 0 0" +
						"1 1 1 1 4 4 5 5 5 1" +
						"1 1 1 1 1 4 4 5 1 1" +
						"1 1 1 1 1 1 1 1 1 1";
			}
			else if (rand == 1) {
				template = "" +
						"0 4 0 0 0 0 4 4 0 0" +
						"0 4 t 0 5 5 4 4 0 0" +
						"0 4 3 3 2 2 1 1 0 0" +
						"0 4 3 3 2 2 1 1 0 0" +
						"0 0 0 0 6 6 0 0 0 0" +
						"0 0 0 0 0 0 0 0 0 0" +
						"0 0 0 0 t 0 0 0 0 0" +
						"1 1 0 0 2 2 0 0 0 0" +
						"1 1 1 1 1 1 1 1 0 0" +
						"1 1 1 1 1 1 1 1 1 1";
			}
			else if (rand == 2) {
				template = "" +
						"0 0 0 2 2 2 1 1 1 1" +
						"0 0 0 0 0 0 1 1 1 1" +
						"0 0 0 0 0 0 2 2 2 2" +
						"2 2 0 0 0 0 0 0 0 0" +
						"2 2 1 1 t 0 0 0 0 0" +
						"1 1 1 1 1 1 0 0 0 0" +
						"0 0 0 0 0 0 0 0 2 2" +
						"0 0 0 r 0 0 0 0 1 1" +
						"0 1 1 1 1 1 1 1 1 1" +
						"1 1 1 1 1 1 1 1 1 1";
			}
			else if (rand == 3) {
				template = "" +
						"0 0 0 0 0 0 0 0 0 0" +
						"0 2 6 0 0 0 2 2 0 0" +
						"0 2 6 4 3 2 1 1 2 0" +
						"0 0 0 4 3 2 1 1 2 0" +
						"0 0 0 0 0 0 0 2 2 0" +
						"1 1 0 0 0 0 0 0 0 0" +
						"1 1 0 0 1 1 0 t 0 1" +
						"1 1 1 1 1 1 1 1 1 1" +
						"1 1 1 1 1 1 1 1 1 1" +
						"1 1 1 1 1 1 1 1 1 1";
			}
			else if (rand == 4) {
				template = "" +
						"0 0 0 0 0 0 0 0 0 0" +
						"0 0 2 2 0 0 5 5 0 0" +
						"0 2 1 1 2 6 1 1 2 0" +
						"0 2 1 1 2 6 1 1 2 0" +
						"0 0 3 0 0 0 0 4 0 0" +
						"0 0 0 0 0 0 0 0 0 0" +
						"0 0 0 0 1 1 0 0 0 0" +
						"0 0 r 0 1 1 0 r 0 0" +
						"2 2 1 1 1 1 1 1 1 1" +
						"1 1 1 1 1 1 1 1 1 1";
			}
		}

		return TranslateStringTemplate(template);
	}

	static char[,] TranslateStringTemplate(string s) {
		s = s.Replace(" ", string.Empty);
		
		char[,] c = new char[Room.roomHeight, Room.roomWidth];
		char[] sc = s.ToCharArray();
		float switcher = Random.value;
		
		for (int x = 0; x < Room.roomWidth; x++) {
			for (int y = 0; y < Room.roomHeight; y++) {
				int xNew = switcher < 0.5f ? x : Room.roomWidth - 1 - x;
				int yNew = Room.roomHeight - 1 - y;
				
				c[xNew, yNew] = sc[y * Room.roomWidth + x];
			}
		}

		return c;
	}
}
