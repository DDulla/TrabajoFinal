using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerCarController : MonoBehaviour
{
    public float forwardSpeed = 10f;
    public float rotationSpeed = 1000f;
    public float brakeSpeed = 5f;
    public float accelerationSpeed = 15f;
    private float currentSpeed;
    private Vector2 moveInput;
    public int maxTurbo = 100;
    private int currentTurbo;
    public float turboUsageRate = 20f;
    public float turboRechargeRate = 10f;
    public GameOverPanel gameOverPanel; 

    void Start()
    {
        currentSpeed = forwardSpeed;
        currentTurbo = maxTurbo;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        float rotateDirection = moveInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotateDirection);
        if (moveInput.x != 0)
        {
            float targetZRotation = -moveInput.x * 15f;
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, targetZRotation), 0.2f).SetEase(Ease.OutQuad);
        }
        else
        {
            transform.DORotate(new Vector3(0, transform.eulerAngles.y, 0), 0.2f).SetEase(Ease.OutQuad);
        }
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

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameOverPanel.ShowGameOverPanel();
            Destroy(gameObject);
        }
    }
}
