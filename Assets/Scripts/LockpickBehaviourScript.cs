using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LockpickBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float MovementSpeed = 5.0f;
    [SerializeField]
    private float MinXPosition = -16f;
    [SerializeField]
    private float MaxXPosition = 0.0f;

    [SerializeField]
    private bool IsPickPressed = false;

    [SerializeField]
    private TumblerBehaviour[] Tumblers;

    private Quaternion startingOrientation;

    [SerializeField]
    private AudioSource SlidingSound;

    [SerializeField]
    private bool Movable = false;

    private int ActiveTumbler = -1;


    private void Awake()
    {
        startingOrientation = transform.rotation;
    }


    private void Update()
    {
        if (IsPickPressed)
        {
            transform.Rotate(new Vector3(0, 0, -5 * Time.deltaTime));
        }
    }


    void OnPickLock(InputValue pressed)
    {
        if (!Movable) return;

        //Debug.Log(pressed.isPressed);
        IsPickPressed = pressed.isPressed;
        if (IsPickPressed)
        {
            float pickLocation = transform.position.x;

            if (pickLocation > MinXPosition && pickLocation < MinXPosition + 2.0f)
            {
                ActiveTumbler = 0;
                Tumblers[0].pressed = true;
                SlidingSound.Play();
            }
            else if (pickLocation > MinXPosition + 4.0f && pickLocation < MinXPosition + 6.0f)
            {
                ActiveTumbler = 1;
                Tumblers[1].pressed = true;
                SlidingSound.Play();
            }
            else if (pickLocation > MinXPosition + 8.0f && pickLocation < MinXPosition + 10.0f)
            {
                ActiveTumbler = 2;
                Tumblers[2].pressed = true;
                SlidingSound.Play();
            }
            else if (pickLocation > MinXPosition + 12.0f && pickLocation < MinXPosition + 14.0f)
            {
                ActiveTumbler = 3;
                Tumblers[3].pressed = true;
                SlidingSound.Play();
            }
            else if (pickLocation > MinXPosition + 16.0f && pickLocation < MinXPosition + 18.0f)
            {
                ActiveTumbler = 4;
                Tumblers[4].pressed = true;
                SlidingSound.Play();
            }
        }
        else
        {
            ResetPickPosition();
            ActiveTumbler = -1; 
        }
    }

    void OnMoveLockpick(InputValue value)
    {
        if (IsPickPressed) return; //dont allow to move the lockpick when pressed

        //Debug.Log(value.Get<Vector2>());
        Vector2 moveDirection = value.Get<Vector2>();
        Vector3 Movement = new Vector3(moveDirection.x, 0, 0);
        transform.position += Movement * MovementSpeed * Time.deltaTime;

        if (transform.position.x < MinXPosition - 1)
        {
            transform.position = new Vector3(MinXPosition - 1, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > MaxXPosition + 1)
        {
            transform.position = new Vector3(MaxXPosition + 1, transform.position.y, transform.position.z);
        }
    }

    private void ResetPickPosition()
    {
        if (ActiveTumbler >= 0)
        {

        Tumblers[ActiveTumbler].RepositionTumbler();
        }
        SlidingSound.Stop();
        transform.rotation = startingOrientation;
        IsPickPressed = false;
    }

    public void DisableLockpick()
    {
        Movable = false;
        ResetPickPosition();
    }

    public void EnableLockpick()
    {
        Movable = true;
        foreach (TumblerBehaviour tumbler in Tumblers)
        {
            tumbler.ResetTumbler();
        }
        ResetPickPosition();
    }

    


}
