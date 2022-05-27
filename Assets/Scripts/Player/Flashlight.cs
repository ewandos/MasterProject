using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

	public GameObject lightSource;
	private Light light;

    void Start()
    {
		this.light = this.lightSource.GetComponent<Light>();
	}

    void Update()
    {
		if (Input.GetKeyUp(KeyCode.F))
		{
			this.light.enabled = !this.light.enabled;
		}
	}
}
