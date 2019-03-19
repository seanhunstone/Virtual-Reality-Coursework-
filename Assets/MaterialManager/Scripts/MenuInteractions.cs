
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MaterialManager;
using VRTK;

namespace MaterialManager
{	
	/// <summary>
	/// Specifies which controller button should be used to activate all objects material manager menus
	/// </summary>
		 
	public enum MenuButtonAlias
	{
		Trigger_Press,
		Grip_Press,
		Touchpad_Press,
		Button_One_Press,
		Button_Two_Touch,
		Vive_Menu_Button,
		Start_Menu_Press
	}

	public class MenuInteractions : MonoBehaviour

	{
		[Space]
		[Tooltip("Loads material saved in preferences. Every the time user selects a material in PLAYMODE its index is saved in preferences.")]
		public bool LoadSavedMaterialPreferences = true;

		//The button used to activate/deactivate the Material Manager for the pointer.
		//Change the MenuButtonAlias here to desired one here. This is only tested for Vive. Oculus rift may work differently.
		[Space]
		private MenuButtonAlias activationButton = MenuButtonAlias.Vive_Menu_Button;
		
		private Renderer rend;
		private GameObject TargetGameObject;
		private MaterialStock Stock;
		private GameObject Menu;
		private int MaterialCount;
		private bool HideLocalMenu = false;
		private bool ShowMenusPrev;

		//Static members used to controll all instances of this class.
		private static bool ShowMenus;
		private static int StartMenuPressedFunctionCalls = 0;
		private static int TotalMenusInScene = 0;
		private static int NumberClosedMenus = 0;

		static private GameObject left_controller;
		static private GameObject right_controller;

		VRTK_ControllerEvents LeftControllerEvents;
		VRTK_ControllerEvents RightControllerEvents;
		

		private void Awake()
		{
			GameObject eventSystem;
			//Check if event system is in the scene. If not create one.
			if (!FindObjectOfType<EventSystem>())
			{
				eventSystem = new GameObject("EventSystem");
				eventSystem.AddComponent<EventSystem>();
				eventSystem.AddComponent<StandaloneInputModule>();
			}
		}
		void Start()
		{
			
		
			InitControllers();
			InitTargetGameObject();

			if (LoadSavedMaterialPreferences)
				LoadFromPreferences();

		}
		
		public void CloseMenu()
		{
			HideLocalMenu = true;
			NumberClosedMenus++;

			//If all menus have been hidden then reset ShowMenus to false. 
			//This ensures that all menus will be shown on the next start menu button press. 
			if (NumberClosedMenus == TotalMenusInScene)
			{
				ShowMenus = false;
				NumberClosedMenus = 0;
			}
		}
				
		
		public void Update()
		{
			Transform rotateTowards = VRTK_DeviceFinder.HeadsetCamera();
			
			//Rotate Menu towards the HMD
			if (Menu != null)
			{
				if (Menu.activeSelf)
					Menu.transform.rotation = Quaternion.LookRotation((rotateTowards.position - Menu.transform.position) * -1, Vector3.up);
			}
			else
				return;

			//Hide and show menu logic
			if (ShowMenusPrev != ShowMenus && ShowMenus == true)
				HideLocalMenu = false;

			if (HideLocalMenu == true && ShowMenus == true)
				Menu.SetActive(false);

			else if (Menu.activeSelf == true && ShowMenus == false)
				Menu.SetActive(false);

			else if (Menu.activeSelf == false && ShowMenus == true)
			{
				Menu.SetActive(true);
				HideLocalMenu = false;
			}

			ShowMenusPrev = ShowMenus;
		}

		private void InitTargetGameObject()
		{
			if (transform.parent == null)
			{
				Debug.LogError("There is a 'loose' instance of Material manager in the hierarchy.Attach it to a gameobject or remove it from hierarchy!");
				return;
			}

			TargetGameObject = transform.parent.gameObject;

			rend = TargetGameObject.GetComponent<Renderer>();

			if (rend == null)
				Debug.Log("The target game object must have a renderer!");

			//Initialize Menu
			Menu = TargetGameObject.GetComponentInChildren<Canvas>(true).gameObject;
			Menu.SetActive(false);
			ShowMenus = false;

			//Initialize Stock 
			Stock = TargetGameObject.GetComponentsInChildren<MaterialStock>()[0];
			MaterialCount = Stock.GetLength();

			if (MaterialCount == 0)
				return;

			CreateMenuUI();
		}

		private void InitControllers()
		{
			left_controller = FindObjectOfType<VRTK_SDKManager>().scriptAliasLeftController.gameObject;
			right_controller = FindObjectOfType<VRTK_SDKManager>().scriptAliasRightController.gameObject;

			if (!left_controller)
			{
				Debug.LogError("Failed to initialize left controller");
			}

			if (!right_controller)
			{
				Debug.LogError("Failed to initialize right controller");
			}

			if (!left_controller.GetComponent<VRTK_UIPointer>() || !right_controller.GetComponent<VRTK_UIPointer>())
				Debug.LogError("Material Manager requires VRTK_UIPointer scrpit attached to at least one of the controllers!");

			CreateControllersEventsHandler();


		}

		//Create controller event handler 
		private void CreateControllersEventsHandler()
		{
			//handle events sent by the controllers when menu button is presseds
			LeftControllerEvents = left_controller.GetComponent<VRTK_ControllerEvents>();
			RightControllerEvents = right_controller.GetComponent<VRTK_ControllerEvents>();

			//In VRTK Button Two refers to the menu button in the Vive controllers

			switch (activationButton)
			{
				case MenuButtonAlias.Vive_Menu_Button://For some unknown reason this is the menu button for the vive
					{
						LeftControllerEvents.ButtonTwoPressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.ButtonTwoPressed += new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
				case MenuButtonAlias.Touchpad_Press:
					{
						LeftControllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.TouchpadPressed+= new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
				case MenuButtonAlias.Trigger_Press:
					{
						LeftControllerEvents.TriggerPressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.TriggerPressed += new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
				case MenuButtonAlias.Grip_Press:
					{
						LeftControllerEvents.GripPressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.GripPressed += new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
				case MenuButtonAlias.Button_One_Press:
					{
						LeftControllerEvents.ButtonOnePressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.ButtonOnePressed += new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
				case MenuButtonAlias.Start_Menu_Press:
					{
						LeftControllerEvents.StartMenuPressed += new ControllerInteractionEventHandler(DoLeftControllerButtonPressed);
						RightControllerEvents.StartMenuPressed += new ControllerInteractionEventHandler(DoRightControllerButtonPressed);
					}
					break;
			}
			
		
		}

		private void DoLeftControllerButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			ActivateMenuOnButtonPressed();
		}


		private void DoRightControllerButtonPressed(object sender, ControllerInteractionEventArgs e)
		{
			ActivateMenuOnButtonPressed();
		}


		private void ActivateMenuOnButtonPressed()
		{
			//register how many times this fucntion is called from different instances of this class
			StartMenuPressedFunctionCalls++;
			if (StartMenuPressedFunctionCalls == TotalMenusInScene)
			{
				ShowMenus = !ShowMenus;
				StartMenuPressedFunctionCalls = 0;
			}
		}		

		//Changes the material in the target objects
		//The target object needs to have renderer
		private void RenderStockMaterial(int index)
		{			

			if (rend == null)
				Debug.LogError("Game objects needs to have Renderer component atached to it!");

			rend.sharedMaterial = Stock.mat[index];
		}

		//Loads the material type from player preferences
		private void LoadFromPreferences()
		{
			if (TargetGameObject == null)
				return;

			int saved_mat_index = PlayerPrefs.GetInt(TargetGameObject.name);
			
			if(saved_mat_index >0 && Stock.GetLength() > saved_mat_index)
				rend.sharedMaterial = Stock.mat[saved_mat_index-1];

		}

		private void CreateMenuUI()
		{

			GameObject FirstButton;
			RectTransform firstButtonRT, buttonRT, ImageRT;
			GameObject Button, LabelTopButton, ImageButton;
			int shift = 0;

			FirstButton = Menu.GetComponentInChildren<Button>().gameObject;
			firstButtonRT = FirstButton.GetComponent<RectTransform>();

			for (int i = 0; i < MaterialCount + 2; i++)
			{
				
				if (i == 0)
				{
					Text ButtonText = FirstButton.GetComponentInChildren<Text>();
					ButtonText.text = "Hide Menu";
					ButtonText.color = Color.white;
					ButtonText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
					FirstButton.GetComponent<Button>().onClick.AddListener(CloseMenu);
					
				}else if (i > 0 && i < MaterialCount + 1)
				{
					if (Stock.GetMaterial(i - 1) == null)					
						continue;//do not add button if the current material in the list is none
					

					Button = GameObject.Instantiate(FirstButton);
					Button.transform.SetParent(Menu.transform, false);

					buttonRT = Button.GetComponent<RectTransform>();
					buttonRT.position = firstButtonRT.position;
					buttonRT.anchoredPosition = firstButtonRT.anchoredPosition;
					buttonRT.localPosition = firstButtonRT.localPosition + new Vector3(-firstButtonRT.rect.width * 1 / 8, shift * (firstButtonRT.rect.height * 1.1f), 0);
					buttonRT.sizeDelta = firstButtonRT.sizeDelta + new Vector2(-firstButtonRT.rect.width * 1 / 4, 0);
					buttonRT.localScale = firstButtonRT.localScale;
					buttonRT.localEulerAngles = firstButtonRT.localEulerAngles;


					ImageButton = GameObject.Instantiate(Button);
					ImageButton.transform.SetParent(Menu.transform, false);
					ImageRT = ImageButton.GetComponent<RectTransform>();
					ImageRT.sizeDelta = firstButtonRT.sizeDelta + new Vector2(-buttonRT.rect.width, 0);
					ImageRT.localPosition = buttonRT.localPosition + new Vector3(firstButtonRT.rect.width / 2, 0, 0);



					Text ImageText = ImageButton.GetComponentInChildren<Text>();
					ImageButton.GetComponent<Image>().material = Stock.GetMaterial(i - 1);
					ImageText.text = "";

					Text ButtonText = Button.GetComponentInChildren<Text>();
					ButtonText.text = Stock.GetMaterial(i - 1).name;

					ButtonText.color = Color.white;
					PickMaterial(Button.GetComponent<Button>(), i - 1);
				}
				else if (i == MaterialCount + 1)
				{
					LabelTopButton = GameObject.Instantiate(FirstButton);
					LabelTopButton.transform.SetParent(Menu.transform, false);

					buttonRT = LabelTopButton.GetComponent<RectTransform>();
					buttonRT.position = firstButtonRT.position;
					buttonRT.anchoredPosition = firstButtonRT.anchoredPosition;
					buttonRT.localPosition = firstButtonRT.localPosition + new Vector3(0, shift * (firstButtonRT.rect.height * 1.1f), 0);
					buttonRT.sizeDelta = firstButtonRT.sizeDelta;
					buttonRT.localScale = firstButtonRT.localScale;
					buttonRT.localEulerAngles = firstButtonRT.localEulerAngles;
					Text ButtonText = LabelTopButton.GetComponentInChildren<Text>();

					ButtonText.text = TargetGameObject.name;
					ButtonText.color = Color.white;
					LabelTopButton.GetComponent<Button>().interactable = false;
					ButtonText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

				}
				shift++;

			}
			TotalMenusInScene++;
		}

		//Adds listener to every selectable material button
		public void PickMaterial(Button curButton, int index)
		{
			switch (index)
			{
				case 0: curButton.onClick.AddListener(ChangeMaterial); break;
				case 1: curButton.onClick.AddListener(ChangeMaterial1); break;
				case 2: curButton.onClick.AddListener(ChangeMaterial2); break;
				case 3: curButton.onClick.AddListener(ChangeMaterial3); break;
				case 4: curButton.onClick.AddListener(ChangeMaterial4); break;
				case 5: curButton.onClick.AddListener(ChangeMaterial5); break;
				case 6: curButton.onClick.AddListener(ChangeMaterial6); break;
				case 7: curButton.onClick.AddListener(ChangeMaterial7); break;
				case 8: curButton.onClick.AddListener(ChangeMaterial8); break;
				case 9: curButton.onClick.AddListener(ChangeMaterial9); break;
				case 10: curButton.onClick.AddListener(ChangeMaterial10); break;
				case 11: curButton.onClick.AddListener(ChangeMaterial11); break;
				default: curButton.onClick.AddListener(ChangeMaterial); break;
			}
		}

		//
		public void ChangeMaterial() { RenderStockMaterial(0); PlayerPrefs.SetInt(TargetGameObject.name, 1); }
		public void ChangeMaterial1() { RenderStockMaterial(1); PlayerPrefs.SetInt(TargetGameObject.name, 2); }
		public void ChangeMaterial2() { RenderStockMaterial(2); PlayerPrefs.SetInt(TargetGameObject.name, 3); }
		public void ChangeMaterial3() { RenderStockMaterial(3); PlayerPrefs.SetInt(TargetGameObject.name, 4); }
		public void ChangeMaterial4() { RenderStockMaterial(4); PlayerPrefs.SetInt(TargetGameObject.name, 5); }
		public void ChangeMaterial5() { RenderStockMaterial(5); PlayerPrefs.SetInt(TargetGameObject.name, 6); }
		public void ChangeMaterial6() { RenderStockMaterial(6); PlayerPrefs.SetInt(TargetGameObject.name, 7); }
		public void ChangeMaterial7() { RenderStockMaterial(7); PlayerPrefs.SetInt(TargetGameObject.name, 8); }
		public void ChangeMaterial8() { RenderStockMaterial(8); PlayerPrefs.SetInt(TargetGameObject.name, 9); }
		public void ChangeMaterial9() { RenderStockMaterial(9); PlayerPrefs.SetInt(TargetGameObject.name, 10); }
		public void ChangeMaterial10() { RenderStockMaterial(10); PlayerPrefs.SetInt(TargetGameObject.name, 11); }
		public void ChangeMaterial11() { RenderStockMaterial(11); PlayerPrefs.SetInt(TargetGameObject.name, 12); }
	}
	
}