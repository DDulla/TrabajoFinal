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
    public int lives = 1;

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

        if (lives <= 0 && GameOverPanel.Instance != null)
        {
            GameOverPanel.Instance.ShowGameOverPanel();
            Time.timeScale = 0;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void ApplyPowerUp(PowerUp.PowerUpType type)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.Score:
                GameManager.Instance.AddScore(5);
                break;
            case PowerUp.PowerUpType.Speed:
                accelerationSpeed += 4;
                break;
            case PowerUp.PowerUpType.Life:
                lives += 1;
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            GameManager.Instance.CollectPowerUp();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lives -= 1;

            if (lives <= 0)
            {
                if (GameOverPanel.Instance != null)
                {
                    GameOverPanel.Instance.ShowGameOverPanel();
                }
                Time.timeScale = 0;
                Destroy(gameObject);
            }
        }
    }
}
