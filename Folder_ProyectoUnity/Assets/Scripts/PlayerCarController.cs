using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCarController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float rotationSpeed = 100f;
    public float brakeSpeed = 5f;
    public float accelerationSpeed = 15f;
    private float currentSpeed;
    private Vector2 moveInput;
    public int maxTurbo = 100;
    private int currentTurbo;
    public float turboUsageRate = 20f;
    public float turboRechargeRate = 10f;

    private PlayerControls controls;

    private void OnEnable()
    {
        controls = new PlayerControls();
        controls.Gameplay.Enable();
        controls.Gameplay.Move.performed += OnMove;
        controls.Gameplay.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        controls.Gameplay.Move.performed -= OnMove;
        controls.Gameplay.Move.canceled -= OnMove;
        controls.Gameplay.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Start()
    {
        currentSpeed = forwardSpeed;
        currentTurbo = maxTurbo;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        float rotateDirection = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotateDirection);
        if (moveInput.y > 0 && currentTurbo > 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, accelerationSpeed, Time.deltaTime);
            currentTurbo -= (int)(turboUsageRate * Time.deltaTime);
        }
        else if (moveInput.y < 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, brakeSpeed, Time.deltaTime);
        }
        else
        {
            currentSpeed = forwardSpeed;
            if (currentTurbo < maxTurbo)
            {
                currentTurbo += (int)(turboRechargeRate * Time.deltaTime);
            }
        }
        currentTurbo = Mathf.Clamp(currentTurbo, 0, maxTurbo);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("Menu"); 
        }
    }
}
