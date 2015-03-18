using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testwalk : MonoBehaviour {
	private List<GameObject> listeGameObject = new List<GameObject>();
	private GameObject currentGameobject;
	public GameObject next1;
	public GameObject next2;
	public Vector3 speed = new Vector3(0.1f,0f,0f);
	public float[] test = new float[1];
	public Material skyboxmaterial;
	public GameObject cube;
	public GameObject cube2;
	public Transform cam;
	public Vector3 difval;
	public List<GameObject> boules = new List<GameObject>();
	public Vector3 boulerotation;
	private Animator animator;
	private Animator pouletAnimator;
	public Material pouletMaterial;
	private Vector3 pouletcolor = new Vector3(0.9f,1.0f,0.7f);
	private Color pouletcolorended = new Color(0.9f,1.0f,0.7f,1f);
	private Vector3 rotationt = new Vector3(0f,0f,3f);
	private Transform road;
	private Transform pouletTransform;
	public SkinnedMeshRenderer skinnedmesh;
	public SkinnedMeshRenderer skinnedmesh2;
	public SkinnedMeshRenderer skinnedmesh3;
	public GameObject mainstar;
	public Material renderImageMaterial;

	void Start () {
		cube = GameObject.Find("Cube_001");
		cube2 = GameObject.Find("Cube");
		mainstar = GameObject.Find ("star");
		Debug.Log (mainstar);
		skinnedmesh2 = GameObject.Find ("donut1").GetComponent<SkinnedMeshRenderer>();
		skinnedmesh3 = GameObject.Find ("donut2").GetComponent<SkinnedMeshRenderer>();
		skinnedmesh = mainstar.GetComponent<SkinnedMeshRenderer>();
		skyboxmaterial = GameObject.Find ("Quad").renderer.material;
		skyboxmaterial.SetFloat ("_X", -(Screen.width/2));
		skyboxmaterial.SetFloat ("_Y", -(Screen.height/2));
		skyboxmaterial.SetFloat ("_Width", Screen.width);
		skyboxmaterial.SetFloat ("_Height", Screen.height);
		currentGameobject = GameObject.Find ("Cube1");
		listeGameObject.Add(currentGameobject);
		listeGameObject.Add(GameObject.Find("Cube2"));
		listeGameObject.Add(GameObject.Find("Cube3"));
		animator = GameObject.Find ("test").GetComponent<Animator>();
		pouletAnimator = GameObject.Find ("poulet").GetComponent<Animator>();
		pouletTransform = GameObject.Find ("poulet").transform;
		animator.speed = 4;
		boulerotation= new Vector3(0f,0f,1f);
		road = GameObject.Find ("BezierCurve_000").transform;
		cam = camera.gameObject.transform;
		for (int i = 1; i < 38; i++) {
			boules.Add(GameObject.Find ("Sphere"+i.ToString()));
				}

	}
	
	/*void OnRenderImage(RenderTexture src, RenderTexture dest) {
		if(test[0]*500f>50f)
		renderImageMaterial.SetFloat("_test",2f);
		else
		renderImageMaterial.SetFloat("_test",1f);
		Graphics.Blit(src, dest, renderImageMaterial);

	}*/

	void Update () {
		if(Input.GetKey(KeyCode.LeftArrow) && pouletTransform.localPosition.y <=0.01315997)
			pouletTransform.localPosition = new Vector3(pouletTransform.localPosition.x,pouletTransform.localPosition.y+0.001f,pouletTransform.localPosition.z);
		if(Input.GetKey(KeyCode.RightArrow) && pouletTransform.localPosition.y >= -0.009840034f)
			pouletTransform.localPosition = new Vector3(pouletTransform.localPosition.x,pouletTransform.localPosition.y-0.001f,pouletTransform.localPosition.z);
		audio.GetOutputData (test, 0);
		//transform.LookAt(pouletTransform.position);
		transform.LookAt(cube.transform.position);
		skyboxmaterial.SetFloat ("test", test [0]);
		pouletcolorended.r = pouletcolor.x+(Mathf.Abs(test[0]));
		pouletcolorended.g = pouletcolor.y-(Mathf.Abs(test[0]));
		pouletcolorended.a = 1f;
		mainstar.transform.Rotate(rotationt);
		skinnedmesh.SetBlendShapeWeight (0, (Mathf.Abs (test[0])*1000f));
		skinnedmesh2.SetBlendShapeWeight (0, (Mathf.Abs (test[0])*500f));
		skinnedmesh3.SetBlendShapeWeight (0, (Mathf.Abs (test[0])*500f));
		pouletMaterial.SetColor("_Color",pouletcolorended);
		pouletMaterial.SetTextureOffset ("_MainTex", new Vector2(test[0]*10f,1f));
		difval =  cube.transform.position - cube2.transform.position;
		float andl = cam.transform.rotation.eulerAngles.y;
		if(andl >=200)
		skyboxmaterial.SetFloat ("_axe", (difval.z/12f));
		else
			skyboxmaterial.SetFloat ("_axe", (-difval.z/12f));
		foreach(GameObject Boule in boules){
			Boule.transform.Rotate(boulerotation);
			if(Mathf.Abs(test[0])>0.1)
			Boule.transform.localScale = new Vector3(0.00125f*Mathf.Abs(test[0]/1),0.00125f*Mathf.Abs(test[0]/1),0.00125f*Mathf.Abs(test[0]/1));
			else
				Boule.transform.localScale = new Vector3(0.00125f,0.00125f,0.00125f);
		}
	}
}
