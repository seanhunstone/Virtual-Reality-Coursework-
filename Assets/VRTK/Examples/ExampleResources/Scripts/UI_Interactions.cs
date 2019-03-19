namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    using System.Collections;

    public class UI_Interactions : MonoBehaviour
    {
		public Texture[] textures;

		static private int currentTexture;
		static private int currentNormal;
		static private int currentTexture2;
		static private int currentNormal2;
		static private int currentTexture3;
		static private int currentNormal3;
		static private int currentTexture4;
		static private int currentNormal4;
		static private int currentTexture5;
		static private int currentNormal5;

		public Renderer rend;
		public MeshRenderer meshRenderer;
		//public Material m_Material;
		Material[] mats;

		static private bool changeWall=false;
		static private bool changeFloor=false;
		static private bool changeFloor2=false;
		static private bool changeCeiling=false;
		static private bool changeCeiling2=false;
		static private bool changeFloorBathroom=false;
		static private bool changeFloorBathroom2=false;


		void Start(){
			VRTK_Logger.Info ("IN START");
		
			rend = GetComponent<Renderer>();
			mats = GetComponent<MeshRenderer>().materials;
			Debug.Log ("materials" + mats);


			meshRenderer = gameObject.GetComponent <MeshRenderer> ();
			//meshRenderer.materials[1].SetColor("_Color", Color.yellow);
			//meshRenderer.materials [1].SetTexture ("_MainTex", textures [1]);
			//meshRenderer.materials [1].SetTexture ("_BumpMap", textures [0]);

			VRTK_Logger.Info ("materials Mesh" + meshRenderer.materials[1]);
			VRTK_Logger.Info ("materials" + textures[1]);

			rend.material.shader = Shader.Find("Transparent/Diffuse");
			rend.material.shader = Shader.Find("Diffuse");
			//GameObject go = GameObject.CreatePrimitive(PrimitiveType.Plane);
			//rend = go.GetComponent<Renderer>();

			//rend.material.mainTexture = Resources.Load ("Floor") as Texture;
			//Debug.Log ("Material:" + rend);
			rend.material.shaderKeywords = new string[1]{"_NORMALMAP"};
			//Button_ChangeWallColour();
		}

		void Update(){


				

			if (changeFloorBathroom2 == true) {
				currentTexture2+=2;
				currentTexture2 %= textures.Length;
				meshRenderer.materials [2].SetTexture ("_MainTex", textures [currentTexture2]);
				currentNormal2+=3;
				currentNormal2 %= textures.Length;
				meshRenderer.materials [2].SetTexture ("_BumpMap", textures [currentNormal2]);

				changeFloorBathroom2 = false;
			}


			if (changeFloorBathroom == true) {

				currentTexture+=6;
				currentTexture %= textures.Length;
				meshRenderer.materials [2].SetTexture ("_MainTex", textures [currentTexture]);
				currentNormal=7;
				currentNormal %= textures.Length;
				meshRenderer.materials [2].SetTexture ("_BumpMap", textures [currentNormal]);


				changeFloorBathroom = false;
			}

			if (changeCeiling == true) {

				meshRenderer.material.EnableKeyword ("_NORMALMAP");
				meshRenderer.materials [3].SetTexture ("_MainTex", textures [2]);
				meshRenderer.materials [3].SetTexture ("_BumpMap", textures [3]);
				Debug.Log ("Map=" + textures [0]);
				changeCeiling = false;
			}

			if (changeCeiling2 == true) {

				meshRenderer.material.EnableKeyword ("_NORMALMAP");
				currentTexture3+=4;
				currentTexture3 %= textures.Length;
				meshRenderer.materials [3].SetTexture ("_MainTex", textures [currentTexture3]);
				currentNormal3=5;
				currentNormal3 %= textures.Length;
				meshRenderer.materials [3].SetTexture ("_BumpMap", textures [currentNormal3]);

				changeCeiling2 = false;
			}
			
			if (changeFloor == true) {
				
				currentTexture5+=10;
				currentTexture5 %= textures.Length;
				meshRenderer.materials [1].SetTexture ("_MainTex", textures [currentTexture5]);
				currentNormal5=11;
				currentNormal5 %= textures.Length;
				meshRenderer.materials [1].SetTexture ("_BumpMap", textures [currentNormal5]);

				changeFloor = false;
				Debug.Log ("IN FLOOR" + changeFloor);
			}

			if (changeFloor2 == true) {

				currentTexture4+=8;
				currentTexture4 %= textures.Length;
				meshRenderer.materials [1].SetTexture ("_MainTex", textures [currentTexture4]);
				currentNormal4=9;
				currentNormal4 %= textures.Length;
				meshRenderer.materials [1].SetTexture ("_BumpMap", textures [currentNormal4]);

				changeFloor2 = false;
				}


			if (changeWall == true) {
				rend.material.mainTexture = textures [currentTexture];
				changeWall = false;
				Debug.Log ("IN HERE" + changeWall);
			}
			//}
					
			//rend.material.mainTexture = textures [currentTexture];


		}




        private const int EXISTING_CANVAS_COUNT = 4;
		public void Button_ChangeWallColour()
		{
			
			VRTK_Logger.Info ("Change Wall Colour"+rend+" "+currentTexture);
				//rend = GetComponent<Renderer> ();
			    
				//Debug.Log ("change wall colour " + rend);
				//rend.material.shader = Shader.Find ("Transparent/Diffuse");
				//rend.material.shader = Shader.Find ("Diffuse");
				//rend.material.SetColor ("_Color", Color.yellow);
				//rend.material.shaderKeywords = new string[1]{ "_NORMALMAP" };

				currentTexture++;
				currentTexture %= textures.Length;
				//currentTexture++;
				//rend.material.mainTexture = textures [currentTexture];
			//state++;
			//log=true;
			changeWall=true;
			Debug.Log ("currentTexture:" + currentTexture + " bool=" + changeWall);
		
		}

		public void Button_ChangeBathroomFloor2()
		{
			currentTexture2+=2;
			currentTexture2 %= textures.Length;
			meshRenderer.materials [2].SetTexture ("_MainTex", textures [currentTexture2]);
			currentNormal2+=3;
			currentNormal2 %= textures.Length;
			meshRenderer.materials [2].SetTexture ("_BumpMap", textures [currentNormal2]);

			changeFloorBathroom2 = true;

		}

		public void Button_ChangeBathroomFloor()
		{
			currentTexture+=6;
			currentTexture %= textures.Length;
			meshRenderer.materials [2].SetTexture ("_MainTex", textures [currentTexture]);
			currentNormal=7;
			currentNormal %= textures.Length;
			meshRenderer.materials [2].SetTexture ("_BumpMap", textures [currentNormal]);
		

			changeFloorBathroom = true;
		}

		public void Button_ChangeFloorTexture()
		{
			
			currentTexture5+=10;
			currentTexture5 %= textures.Length;
			meshRenderer.materials [1].SetTexture ("_MainTex", textures [currentTexture5]);
			currentNormal5=11;
			currentNormal5 %= textures.Length;
			meshRenderer.materials [1].SetTexture ("_BumpMap", textures [currentNormal5]);
			changeFloor = true;

			Debug.Log ("Floor bool" + changeFloor);
		}

		public void Button_ChangeFloorTexture2()
		{
			currentTexture4+=4;
			currentTexture4 %= textures.Length;
			meshRenderer.materials [1].SetTexture ("_MainTex", textures [currentTexture4]);
			currentNormal4+=5;
			currentNormal4 %= textures.Length;
			meshRenderer.materials [1].SetTexture ("_BumpMap", textures [currentNormal4]);

			changeFloor2 = true;

			Debug.Log ("Floor bool" + changeFloor2);
		}

		public void Button_ChangeCeilingTexture()
		{
			meshRenderer.materials [3].SetTexture ("_MainTex", textures [1]);
			meshRenderer.materials [3].SetTexture ("_BumpMap", textures [0]);

			changeCeiling = true;
		}

		public void Button_ChangeCeilingTexture2()
		{
			currentTexture3+=8;
			currentTexture3 %= textures.Length;
			meshRenderer.materials [3].SetTexture ("_MainTex", textures [currentTexture3]);
			currentNormal3=9;
			currentNormal3 %= textures.Length;
			meshRenderer.materials [3].SetTexture ("_BumpMap", textures [currentNormal3]);

			changeCeiling2 = true;
		}



        public void Button_Red()
        {
            VRTK_Logger.Info("Red Button Clicked");
        }

        public void Button_Pink()
        {
            VRTK_Logger.Info("Pink Button Clicked");
        }

        public void Toggle(bool state)
        {
            VRTK_Logger.Info("The toggle state is " + (state ? "on" : "off"));
        }

        public void Dropdown(int value)
        {
            VRTK_Logger.Info("Dropdown option selected was ID " + value);
        }

        public void SetDropText(BaseEventData data)
        {
            var pointerData = data as PointerEventData;
            var textObject = GameObject.Find("ActionText");
            if (textObject)
            {
                var text = textObject.GetComponent<Text>();
                text.text = pointerData.pointerDrag.name + " Dropped On " + pointerData.pointerEnter.name;
            }
        }

        public void CreateCanvas()
        {
            StartCoroutine(CreateCanvasOnNextFrame());
        }

        private IEnumerator CreateCanvasOnNextFrame()
        {
            yield return null;

            var canvasCount = FindObjectsOfType<Canvas>().Length - EXISTING_CANVAS_COUNT;
            var newCanvasGO = new GameObject("TempCanvas");
            newCanvasGO.layer = 5;
            var canvas = newCanvasGO.AddComponent<Canvas>();
            var canvasRT = canvas.GetComponent<RectTransform>();
            canvasRT.position = new Vector3(-4f, 2f, 3f + canvasCount);
            canvasRT.sizeDelta = new Vector2(300f, 400f);
            canvasRT.localScale = new Vector3(0.005f, 0.005f, 0.005f);
            canvasRT.eulerAngles = new Vector3(0f, 270f, 0f);

            var newButtonGO = new GameObject("TempButton", typeof(RectTransform));
            newButtonGO.transform.SetParent(newCanvasGO.transform);
            newButtonGO.layer = 5;

            var buttonRT = newButtonGO.GetComponent<RectTransform>();
            buttonRT.position = new Vector3(0f, 0f, 0f);
            buttonRT.anchoredPosition = new Vector3(0f, 0f, 0f);
            buttonRT.localPosition = new Vector3(0f, 0f, 0f);
            buttonRT.sizeDelta = new Vector2(180f, 60f);
            buttonRT.localScale = new Vector3(1f, 1f, 1f);
            buttonRT.localEulerAngles = new Vector3(0f, 0f, 0f);

            newButtonGO.AddComponent<Image>();
            var canvasButton = newButtonGO.AddComponent<Button>();
            var buttonColourBlock = canvasButton.colors;
            buttonColourBlock.highlightedColor = Color.red;
            canvasButton.colors = buttonColourBlock;

            var newTextGO = new GameObject("BtnText", typeof(RectTransform));
            newTextGO.transform.SetParent(newButtonGO.transform);
            newTextGO.layer = 5;

            var textRT = newTextGO.GetComponent<RectTransform>();
            textRT.position = new Vector3(0f, 0f, 0f);
            textRT.anchoredPosition = new Vector3(0f, 0f, 0f);
            textRT.localPosition = new Vector3(0f, 0f, 0f);
            textRT.sizeDelta = new Vector2(180f, 60f);
            textRT.localScale = new Vector3(1f, 1f, 1f);
            textRT.localEulerAngles = new Vector3(0f, 0f, 0f);

            var txt = newTextGO.AddComponent<Text>();
            txt.text = "New Button";
            txt.color = Color.black;
            txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

            newCanvasGO.AddComponent<VRTK_UICanvas>();
        }
    }
}