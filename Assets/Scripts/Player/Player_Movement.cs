using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
	// Mouselook-related variables
	public float mouseSensitivity = 1.5f;
	public Transform playerCamera;
	private float xRotation = 0f;

	// Movement-related variables
	public CharacterController controller;
	public float speed = 3.5f;
	public float sprintMultiplier = 2.25f;

	private float sprintEnergy = 100f;
	private float sprintDepletionSpeed = 20f;
	private float sprintRechargeSpeed = 10f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
		HandleMovement();
		HandleMouseLook();

		//Debug.Log("Sprint Energy: " + sprintEnergy);
	}

	void HandleMovement()
	{
		float finalSpeed = speed;

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		bool isMoving = (x != 0) || (z != 0);

		if (IsSprinting(isMoving))
		{
			finalSpeed *= sprintMultiplier;
		}

		Vector3 move = transform.right * x + transform.forward * z;

		controller.Move(move * finalSpeed * Time.deltaTime);
	}

	void HandleMouseLook()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		transform.Rotate(Vector3.up * mouseX);
	}

	bool IsSprinting(bool isMoving)
	{
		bool tryingToSprint = Input.GetKey(KeyCode.LeftShift) && isMoving;

		if (tryingToSprint && sprintEnergy != 0.0f)
		{
			DepleteSprintEnergy();
			return true;
		}
		
		if (!tryingToSprint)
		{
			RechargeSprintEnergy();
		}
		
		return false;
	}

	void DepleteSprintEnergy()
	{
		sprintEnergy -= sprintDepletionSpeed * Time.deltaTime;
		sprintEnergy = sprintEnergy < 0.0f ? 0.0f : sprintEnergy;
	}

	void RechargeSprintEnergy()
	{
		sprintEnergy += sprintRechargeSpeed * Time.deltaTime;
		sprintEnergy = sprintEnergy > 100.0f ? 100.0f : sprintEnergy;
	}
}
