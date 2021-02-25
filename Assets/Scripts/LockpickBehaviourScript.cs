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
        //if (pressed.isPressed)
        //{
        //    Debug.Log();
        //}

        //Debug.Log(pressed.isPressed);
        IsPickPressed = pressed.isPressed;
        if (IsPickPressed)
        {
            float pickLocation = transform.position.x;

            if (pickLocation > MinXPosition && pickLocation < MinXPosition + 2.0f)
            {
                Tumblers[0].pressed = true;
            }
            else if (pickLocation > MinXPosition + 4.0f && pickLocation < MinXPosition + 6.0f)
            {
                Tumblers[1].pressed = true;
            }
            else if (pickLocation > MinXPosition + 8.0f && pickLocation < MinXPosition + 10.0f)
            {
                Tumblers[2].pressed = true;
            }
            else if (pickLocation > MinXPosition + 12.0f && pickLocation < MinXPosition + 14.0f)
            {
                Tumblers[3].pressed = true;
            }
            else if (pickLocation > MinXPosition + 16.0f && pickLocation < MinXPosition + 18.0f)
            {
                Tumblers[4].pressed = true;
            }
        }
        else
        {
            foreach (TumblerBehaviour tumbler in Tumblers)
            {
                tumbler.RepositionTumbler();
                transform.rotation = startingOrientation;
            }
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
}
