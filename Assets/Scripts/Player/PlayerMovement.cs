using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Mouselook-related variables
	public float mouseSensitivity = 1.5f;
	public Transform playerCamera;
	private float xRotation = 0f;

	// Movement-related variables
	public CharacterController controller;
	public float speed = 3.5f;
	public float sprintMultiplier = 2.25f;

	[SerializeField]
	private float maxSpringEnergy = 100f;
	
	[SerializeField]
	private float sprintEnergy = 100f;
	private float sprintDepletionSpeed = 20f;
	private float sprintRechargeSpeed = 10f;
	public FloatSO sprintEnergyHolder;

	private bool isMoving = false;
	public AudioClipSequencer audioClipSequencer;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
		float finalSpeed = HandleMovement();
		HandleMouseLook();
		
		if (isMoving)
			audioClipSequencer.SetInterval(2 / finalSpeed);
		else
			audioClipSequencer.Stop();
    }

	float HandleMovement()
	{
		float finalSpeed = speed;

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		isMoving = (x != 0) || (z != 0);

		if (IsSprinting(isMoving))
		{
			finalSpeed *= sprintMultiplier;
		}

		Vector3 move = transform.right * x + transform.forward * z;

		controller.Move(move * finalSpeed * Time.deltaTime);

		sprintEnergyHolder.Value = sprintEnergy;

		return finalSpeed;
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
		sprintEnergy = Mathf.Max(0.0f, sprintEnergy);
	}

	void RechargeSprintEnergy()
	{
		sprintEnergy += sprintRechargeSpeed * Time.deltaTime;
		sprintEnergy = Mathf.Min(sprintEnergy, maxSpringEnergy);
	}
}
