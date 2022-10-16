using UnityEngine;
using System.Collections;

[AddComponentMenu("Effects/Fracture")]
public class Fracture : MonoBehaviour
{
	[HideInInspector]
	public bool UseGravity = true;
	[HideInInspector]
	public bool DestroyParticles = true;
	[HideInInspector]
	public float DestroyParticlesAfterSeconds = 5;
	[HideInInspector]
	public float Force = 20;
	[HideInInspector]
	public float Radius = 30;
	[HideInInspector]
	public AudioSource Audio;
	[HideInInspector]
	public GameObject Debris;
	[HideInInspector]
	public MeshRenderer Renderer;
	[HideInInspector]
	public MeshFilter Filter;


	public void Trigger()
	{
		SplitMesh();
	}

	private void SplitMesh()
	{
		Mesh originalMesh = Filter.mesh;
		Vector3[] verts = originalMesh.vertices;
		Vector3[] normals = originalMesh.normals;
		Vector2[] uvs = originalMesh.uv;
		bool hasUvs = uvs.Length != 0;
		for (int submesh = 0; submesh < originalMesh.subMeshCount; ++submesh)
		{
			int[] indices = originalMesh.GetTriangles(submesh);
			for (int i = 0; i < indices.Length; i += 3)
			{
				Vector3[] newVerts = new Vector3[3];
				Vector3[] newNormals = new Vector3[3];
				Vector2[] newUvs = new Vector2[3];
				for (int n = 0; n < 3; n++)
				{
					int index = indices[i + n];
					newVerts[n] = verts[index];
					newNormals[n] = normals[index];
					if (hasUvs)
					{
						newUvs[n] = uvs[index];
					}
				}
				this.BuildTriangle(Renderer.materials[submesh], newVerts, newNormals, newUvs, "Triangle" + (i / 3));
			}
		}
		Renderer.enabled = false;
		float audioLength = 0;
		if (Audio != null)
		{
			AudioSource source = Instantiate<AudioSource> (Audio, this.transform.position, Quaternion.identity);
			source.Play();
			Destroy (source, audioLength = source.clip.length);
		}
	}

	private void BuildTriangle(Material Mat, Vector3[] NewVerts, Vector3[] NewNormals, Vector2[] NewUvs, string Name)
	{
		Mesh mesh = new Mesh();
		mesh.vertices = NewVerts;
		mesh.normals = NewNormals;
		mesh.uv = NewUvs;
		mesh.triangles = new int[]
		{
			0, 1, 2,
			2, 1, 0
		};

		GameObject triangleObject = new GameObject(Name);
		if (Debris != null) triangleObject.transform.parent = Debris.transform;
		triangleObject.transform.position = transform.position;
		triangleObject.transform.rotation = transform.rotation;
		triangleObject.transform.localScale = transform.localScale;
		triangleObject.transform.localRotation = transform.localRotation;
		triangleObject.AddComponent<MeshRenderer>().material = Mat;
		triangleObject.AddComponent<MeshFilter>().mesh = mesh;
		triangleObject.AddComponent<BoxCollider>();
		var rigidbody = triangleObject.AddComponent<Rigidbody> ();
		rigidbody.AddExplosionForce(Force, transform.position, Radius);
		rigidbody.useGravity = UseGravity;
		if(DestroyParticles)
			StartCoroutine(DestroyCoroutine(triangleObject));
	}

	private IEnumerator DestroyCoroutine(GameObject TriangleObject)
	{
		yield return new WaitForSeconds(DestroyParticlesAfterSeconds);
		TriangleObject.GetComponent<MeshFilter>().sharedMesh.Clear();
		Destroy(TriangleObject);
	}
}