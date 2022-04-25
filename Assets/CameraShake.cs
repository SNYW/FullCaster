using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    public static CameraShake Instance;

    private void Awake()
    {
        initialPosition = transform.position;

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    void Update()
    {
        if (shakeDuration > 0 && GameManager.Instance.playing)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void AddShakeDuration(float f)
    {
        shakeDuration += f;
    }

    public void AddShakeDuration(float duration, float mag, float damp)
    {
        shakeDuration += duration;
        shakeMagnitude = mag;
        dampingSpeed = damp;
    }

    public void StopShake()
    {
        shakeDuration = 0;
    }
}
