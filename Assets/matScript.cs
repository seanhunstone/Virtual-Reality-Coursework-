using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matScript : MonoBehaviour {
	public Texture[] textures;
	public Texture[] normal;
	public int currentNormal;
	public int currentTexture;




	public Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();

		rend.material.shader = Shader.Find("Transparent/Diffuse");
		rend.material.shader = Shader.Find("Diffuse");
		//rend.material.SetColor ("_Color", Color.yellow);

		rend.material.shaderKeywords = new string[1]{"_NORMALMAP"};



	}
	
	// Update is called once per frame

	void Update() {
		if(Input.GetKeyDown(KeyCode.Space)) {

			//(Input.GetKeyDown(KeyCode.Space))
			//MeshRenderer m = rend.GetComponent<MeshRenderer> ();
			//m.material = new Material(Shader.Find("Transparent/Diffuse"));
			//m.material = new Material(Shader.Find("Diffuse"));
			currentTexture++;
			currentTexture %= textures.Length;
			rend.material.mainTexture = textures[currentTexture];
			//rend.material.SetTexture ("BumpMap", normal);
			//rend.material.shaderKeywords = new string[1]{"_NORMALMAP"};
			//currentNormal++;
			//currentNormal %= normal.Length;
			//rend.material.mainTexture = normal [currentNormal];
		}
	}


}
