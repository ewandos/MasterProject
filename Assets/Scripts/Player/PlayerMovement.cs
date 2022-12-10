using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	// Mouselook-related variables
	public float mouseSensitivity = 1.5f;
	public Transform playerCamera;
	private float xRotation = 0f;

	// Movement-related variables
	private CharacterController controller;
	public float speed = 3.5f;
	public float sprintMultiplier = 2.25f;
	public float gravity = 9.8f;
	private float verticalSpeed = 0;

	[SerializeField]
	private float maxSpringEnergy = 100f;
	
	[SerializeField]
	private float sprintEnergy = 100f;
	private float sprintDepletionSpeed = 20f;
	private float sprintRechargeSpeed = 10f;
	public FloatSO sprintEnergyHolder;

	private bool hasMovementInput = false;
	public AudioClipSequencer audioClipSequencer;

	public Vector3 velocity = Vector3.zero;
	
	void Start()
	{
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
    {
		float finalSpeed = HandleMovement();
		HandleMouseLook();
		
		if (hasMovementInput)
			audioClipSequencer.SetInterval(2 / finalSpeed);
		else
			audioClipSequencer.Stop();
    }

	float HandleMovement() {
		float finalSpeed = speed;

		float xInput = Input.GetAxis("Horizontal");
		float zInput = Input.GetAxis("Vertical");
		hasMovementInput = (xInput != 0) || (zInput != 0);

		if (IsSprinting(hasMovementInput))
		{
			finalSpeed *= sprintMultiplier;
		}

		if (controller.isGrounded)
		{
			verticalSpeed = 0;
		}
		
		
		velocity = transform.right * xInput + transform.forward * zInput;
		velocity *= finalSpeed;
		verticalSpeed -= gravity * Time.deltaTime;
		velocity.y = verticalSpeed;

		controller.Move(velocity * Time.deltaTime);

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

	bool IsSprinting(bool hasMovementInput)
	{
		bool tryingToSprint = Input.GetKey(KeyCode.LeftShift) && hasMovementInput;

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
