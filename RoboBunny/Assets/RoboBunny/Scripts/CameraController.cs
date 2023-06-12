using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraYMin;
    
    public float freezeDuration = 0.5f;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private float freezeTimer;
    private float shakeTimer;
    private bool isFrozen;
    private bool isShaking;
    private Vector3 originalCameraPosition;

    private void Update()
    {

        if (isFrozen)
        {
            freezeTimer -= Time.unscaledDeltaTime;
            if (freezeTimer <= 0f)
            {
                isFrozen = false;
                Time.timeScale = 1f; // Resume normal time scale
            }
        }
        if (isShaking)
        {
            if (shakeTimer > 0f)
            {
                Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
                transform.position = originalCameraPosition + shakeOffset;
                shakeTimer -= Time.deltaTime;
            }
            else
            {
                transform.position = originalCameraPosition;
                isShaking = false;
            }
        }

        if (!isShaking && !isFrozen)
        {
            transform.position = new Vector3(player.position.x, Mathf.Max(player.position.y, cameraYMin), transform.position.z);
        }
    }

    public void FreezeScreen()
    {
        if (!isFrozen)
        {
            isFrozen = true;
            freezeTimer = freezeDuration;
            Time.timeScale = 0f; // Pause the game by setting time scale to 0
        }
    }
    
    public void ShakeScreen()
    {
        if (!isShaking)
        {
            originalCameraPosition = transform.position;
            shakeTimer = shakeDuration;
            isShaking = true;
        }
    }
}




