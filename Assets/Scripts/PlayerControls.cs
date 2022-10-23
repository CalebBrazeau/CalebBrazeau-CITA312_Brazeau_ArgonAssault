using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction shoot;

    [SerializeField] float fltControlSpeed = 30f;
    [SerializeField] float fltXRange = 10f;
    [SerializeField] float fltYRange = 5f;

    [SerializeField] float fltPositionPitchFactor = -2f;
    [SerializeField] float fltControlPitchFactor = -15f;

    [SerializeField] float fltPositionYawFactor = 2f;

    [SerializeField] float fltControlRollFactor = -20f;

    float fltXThrow, fltYThrow;

    [SerializeField] GameObject[] lasers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
        shoot.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        shoot.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        // Read up and down movement inputs
        fltXThrow = movement.ReadValue<Vector2>().x; 
        fltYThrow = movement.ReadValue<Vector2>().y;

        float fltXOffset = fltXThrow * Time.deltaTime * fltControlSpeed;
        float fltRawXPos = transform.localPosition.x + fltXOffset;
        float fltClampedXPos = Mathf.Clamp(fltRawXPos, -fltXRange, fltXRange);

        float fltYOffset = fltYThrow * Time.deltaTime * fltControlSpeed;
        float fltRawYPos = transform.localPosition.y + fltYOffset;
        float fltClampedYPos = Mathf.Clamp(fltRawYPos, -fltYRange, fltYRange);

        transform.localPosition = new Vector3(fltClampedXPos, fltClampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float fltPitchDueToPosition = transform.localPosition.y * fltPositionPitchFactor;
        float fltPitchDueToControlThrow = fltYThrow * fltControlPitchFactor;
        float fltPitch = fltPitchDueToPosition + fltPitchDueToControlThrow;

        float fltYaw = transform.localPosition.x * fltPositionYawFactor;

        float fltRoll = fltXThrow * fltControlRollFactor;

        transform.localRotation = Quaternion.Euler(fltPitch, fltYaw, fltRoll);
    }

    void ProcessFiring()
    {
        if (shoot.ReadValue<float>() > 0.1)
        {
            ActivateLasers();
        }
        else
        {
            DeactivateLasers();
        }
    }

    void ActivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(true);
        }
    }

    void DeactivateLasers()
    {
        foreach (GameObject laser in lasers)
        {
            laser.SetActive(false);
        }
    }
}
