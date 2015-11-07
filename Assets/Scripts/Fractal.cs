using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	private Material[] materials;

	public int maxDepth;

	public float childScale;

	private int depth;

	public Mesh mesh;
	public Material material;

	private void InitializeMaterials()
	{
		materials = new Material[maxDepth+1];

		for (int i = 0; i <= maxDepth; i++) {
			materials[i] = new Material(material);
			materials[i].color = Color.Lerp (Color.white, Color.yellow, (float) i / maxDepth);
		}

	}

	// Use this for initialization
	void Start () {
		if (materials == null) {
			InitializeMaterials();
		}

		gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		gameObject.AddComponent<MeshRenderer> ().material = materials [depth];

		if (depth < maxDepth) {
			StartCoroutine(CreateChildren());
		}
	}

	private IEnumerator CreateChildren()
	{
		WaitForSeconds wait = new WaitForSeconds (0.5f);

		yield return wait;

		new GameObject ("P'tit Fractal")
			.AddComponent<Fractal> ()
				.Initialize(this, Vector3.up, Quaternion.identity);

		yield return wait;

		new GameObject ("P'tit Fractal")
			.AddComponent<Fractal> ()
				.Initialize(this, Vector3.right, Quaternion.Euler(0f, 0f, -90f));

		yield return wait;
		
		new GameObject ("P'tit Fractal")
			.AddComponent<Fractal> ()
				.Initialize(this, Vector3.left, Quaternion.Euler(0f, 0f, 90f));
	}

	// Update is called once per frame
	private void Initialize(Fractal parent, Vector3 direction, Quaternion orientation) {
		mesh = parent.mesh;
		materials = parent.materials;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		transform.parent = parent.transform;

		transform.localScale = Vector3.one * childScale;
		transform.localRotation = orientation;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
	}
}
