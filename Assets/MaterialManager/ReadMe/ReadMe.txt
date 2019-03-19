
	VR Material manager is a utility which which allows you to attach multiple materials to a game object in edit mode and select different material
in playmode. It is perfect for VR interior designs or architectural walkthroughs.
It has been developed and tested for HTC vive. It should work on the rift as well. Requires SteamVR and VRTK.

Editmode:
1. Install SteamVR and VRTK plugins if you don't have them.
1. Go to prefab folder
2.Drag and drop MaterialManager prefab to a gameobject. The game object needs to have renderer.
3.Position it to the desired location in world space.
4.Add materials to the MaterialStock script
5. Make sure at least one controller have the following scrpits attached
VRTK_Controller_Events
VRTK_Pointer
VRTK_Straight_Pointer_Renderer
VRTK_UI_Pointer

Playmode example
1.Open demoscene
2.Press menu button 
3.Press touchpad and select material

See screenshots for reference.

Any questions email to 
dimitar@keswickvr.com

Notes:
Sometimes VRTK shows errors in the console window. It might solve the issue if you click on [VRTK] in the hierarchy window and 
them play/stop a few times.