using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MaterialManager
{
	public class MaterialStock : MonoBehaviour
	{
		public Material[] mat=new Material[12];
		public Material GetMaterial(int index)
		{
			if (index >= 12)
				return null;

			return mat[index];
		}
		public int GetLength()
		{
			int field_count = mat.GetLength(0);
			int count = field_count;

			for (int i=mat.GetLength(0)-1; i>=0; i--)
			{
				if(mat[i] != null)
					return count;
				count--;
			}

			return count;
		}

	}
}
