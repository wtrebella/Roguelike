using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PolygonTest : MonoBehaviour {
	PolygonCollider2D polygonCollider;
	MeshFilter meshFilter;
	MeshRenderer meshRenderer;
	public Material material;

	// Use this for initialization
	void Start () {
		polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
		meshFilter = gameObject.AddComponent<MeshFilter>();
		meshRenderer = gameObject.AddComponent<MeshRenderer>();

		float radius = 2;
		int resolution = 30;

		CreatePolygonCollider2D(radius, resolution);
		CreatePolygonMesh(radius, resolution);
	}

	void CreatePolygonCollider2D(float radius, float resolution) {
		List<Vector2> points = new List<Vector2>();

		float x, y = 0;
		Vector2 v = Vector2.zero;

		for (int i = 0; i < resolution; i++) {
			x = Mathf.Cos(i * (360f / resolution) * Mathf.Deg2Rad) * radius;
			y = Mathf.Sin(i * (360f / resolution) * Mathf.Deg2Rad) * radius;

			v = new Vector2(x, y);

			points.Add(v);
		}

		polygonCollider.points = points.ToArray();
	}

	void CreatePolygonMesh(float radius, float resolution) {
		Mesh mesh = new Mesh();

		List<Vector3> vertices = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<int> tris = new List<int>();

		for (int i = 0; i < resolution; i++) {
			Vector2 point = polygonCollider.points[i];

			vertices.Add(new Vector3(point.x, point.y, 0));
			vertices.Add(Vector3.zero);
			vertices.Add(Vector3.zero);
			vertices.Add(i < resolution - 1 ? polygonCollider.points[i + 1] : polygonCollider.points[0]);

			uvs.Add(new Vector2(1, 1));
			uvs.Add(new Vector2(1, 0));
			uvs.Add(new Vector2(0, 0));
			uvs.Add(new Vector2(0, 1));

			tris.Add(i * 4 + 0);
			tris.Add(i * 4 + 1);
			tris.Add(i * 4 + 3);
		}

		mesh.name = "Mesh!";
		mesh.vertices = vertices.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = tris.ToArray();

		meshFilter.mesh = null;
		meshFilter.mesh = mesh;

		meshRenderer.material = material;

		mesh.RecalculateBounds();
		mesh.RecalculateNormals();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
