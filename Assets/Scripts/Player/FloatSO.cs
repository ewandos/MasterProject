using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
	private float value;

	public float Value
	{
		get { return value; }
		set { this.value = value; }
	}
}
