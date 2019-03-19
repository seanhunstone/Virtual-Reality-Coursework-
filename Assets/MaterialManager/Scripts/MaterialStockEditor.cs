using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using MaterialManager;

	[CustomEditor(typeof(MaterialStock))]
	[CanEditMultipleObjects]

	public class MaterialStockEditor : Editor
	{
	SerializedProperty Mat;

	void OnEnable()
	{
		Mat = serializedObject.FindProperty("mat");
	}

	public override void OnInspectorGUI()
	{
		SerializedProperty element;
		MaterialStock myTarget = (MaterialStock)target;
		int num_materials = myTarget.GetLength();
		int mat_array_capacity = myTarget.mat.GetLength(0);
		serializedObject.Update();		
		EditorGUILayout.Separator();
		EditorGUILayout.LabelField(new GUIContent("Material List"));
		EditorGUILayout.Separator();

		for (int i = 0; i < num_materials; i++)
		{
			element = Mat.GetArrayElementAtIndex(i);
			EditorGUILayout.PropertyField(Mat.GetArrayElementAtIndex(i), new GUIContent(""));
		}
		if(num_materials < mat_array_capacity)
			EditorGUILayout.PropertyField(Mat.GetArrayElementAtIndex(myTarget.GetLength()), new GUIContent("Add new material "));

		serializedObject.ApplyModifiedProperties();
		serializedObject.Update();
	}
}
#endif

