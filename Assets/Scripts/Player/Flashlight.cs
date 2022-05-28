using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{

	public GameObject lightSource;
	private Light light;
	private float maxIntensity;
	private bool isOn = true;

	private float energy = 100.0f;
	public float energyDepletionSpeed = 10.0f;
	public float energyRechargeSpeed = 25.0f;
	public FloatSO energyHolder;

    void Start()
    {
		light = lightSource.GetComponent<Light>();
		maxIntensity = light.intensity;
	}

    void Update()
    {
		if (Input.GetKeyUp(KeyCode.F))
		{
			isOn = !isOn;
		}

		if (isOn)
		{		
			DepleteEnergy();
		} else
		{
			RechargeEnergy();
		}

		if (energy == 0.0f)
		{
			isOn = false;
		}

		light.intensity = isOn? maxIntensity * energy / 100.0f : 0;

		energyHolder.Value = energy;
	}

	void DepleteEnergy()
	{
		energy -= energyDepletionSpeed * Time.deltaTime;
		energy = Mathf.Max(0.0f, energy);
	}

	void RechargeEnergy()
	{
		energy += energyDepletionSpeed * Time.deltaTime;
		energy = Mathf.Min(energy, 100.0f);
	}
}
