using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeWallMaterial : MonoBehaviour {

	public Renderer rend;
	public Texture[] textures;



	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		//Material material = new Material (Shader.Find ("Wall"));
		rend.material.shader = Shader.Find("Transparent/Diffuse");
		rend.material.shader = Shader.Find("Diffuse");
		//rend.material.SetColor ("_Color", Color.yellow);

		rend.material.shaderKeywords = new string[1]{"_NORMALMAP"};



	}

	// Update is called once per frame
	public void testcall(){
		Debug.Log ("changing wall color");
	}
	public void changeMat(Texture t){
		Debug.Log ("changing wall caolor");
		//if(Input.GetButtonDown("Material1")) {

			MeshRenderer m = rend.GetComponent<MeshRenderer> ();
			m.material = new Material(Shader.Find("Transparent/Diffuse"));
			m.material = new Material(Shader.Find("Diffuse"));
			rend = GetComponent<Renderer> ();
			//Material material = new Material (Shader.Find ("Wall"));
			rend.material.shader = Shader.Find("Transparent/Diffuse");
			rend.material.shader = Shader.Find("Diffuse");
			rend.material.mainTexture = t;
			//rend.material.SetTexture ("BumpMap", normal);
			//rend.material.shaderKeywords = new string[1]{"_NORMALMAP"};

		//}
	}

}

