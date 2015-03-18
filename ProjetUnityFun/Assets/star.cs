using UnityEngine;
using System.Collections;

public class star : MonoBehaviour {
	private Vector3 rotationt = new Vector3(0f,0f,3f);
	public SkinnedMeshRenderer skinnedmesh;

	void Start() {
		skinnedmesh = GetComponent<SkinnedMeshRenderer>();
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotationt);
		Debug.Log((float)(Time.frameCount % 500));
		skinnedmesh.SetBlendShapeWeight (0, (float)(Time.frameCount % 500));

	}
}
