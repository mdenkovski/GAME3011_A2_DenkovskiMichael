using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TumblerBehaviour : MonoBehaviour
{
    public bool pressed = false;

    [SerializeField]
    [Range(0, 4)]
    private int StopPoint;
    [SerializeField]
    [Range(0.0f, 0.95f)]
    private float sensitivity = 0.5f;


    [SerializeField]
    private float HorizontalShakeAmount = 0.5f;
    [SerializeField]
    [Range(20, 60)]
    private float Shakeintensity = 20.0f;
    [SerializeField]
    [Range(0, 1)]
    private float movementTValue = 0.4f;


    

    [SerializeField]
    private bool IsLocked = false;

    private Vector3 startPosition;
    private float GoalYBegin;
    private float GoalYEnd;
    private float StopPosition;



    //[SerializeField]
    private AudioSource JinglingSound;
    private bool IsJinglePlaying;

    [SerializeField]
    private TimerManager Timer;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Start()
    {
        JinglingSound = GetComponent<AudioSource>();
    }



    private void OnDisable()
    {
        ResetTumbler();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsLocked) return;

        if (pressed)
        {
            float lerpedY = Mathf.Lerp(transform.position.y, StopPosition, movementTValue * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, lerpedY, transform.position.z);
        }

        //if it is within sucess range
        if (transform.position.y < GoalYBegin && transform.position.y > StopPosition)
        {
            float shakeDirection = -1 + Mathf.PingPong(Time.time * Shakeintensity, 2);
            transform.position = new Vector3(startPosition.x + (HorizontalShakeAmount * shakeDirection), transform.position.y, transform.position.z);

            if (!IsJinglePlaying)
            {
                JinglingSound.Play();
                IsJinglePlaying = true;
            }
        }

        //limit how far down the tumbler can move based on the stop point. 
        if (transform.position.y < StopPosition)
        {
            transform.position = new Vector3(transform.position.x, StopPosition, transform.position.z);
        }
    }

    public void RepositionTumbler()
    {
        pressed = false;

        if (transform.position.y < GoalYBegin && transform.position.y > GoalYEnd) //check if tumbler is sucessfully locked
        {
            IsLocked = true;
            transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z);
            Timer.TumblerCorrect();
        }
        else
        {
            transform.position = startPosition;
            
        }

        JinglingSound.Stop();
        IsJinglePlaying = false;    

    }

    public void UpdateDistances()
    {
        GoalYBegin = startPosition.y - (5 - StopPoint);
        GoalYEnd = startPosition.y - (5 + (1 - sensitivity) - StopPoint);
        StopPosition = startPosition.y - (6 - StopPoint);
    }

    private void RandomizeStopPoint()
    {
        StopPoint = Random.Range(0, 5);
        UpdateDistances();
    }

    public void ChangeDifficulty(float newSensiticity)
    {
        sensitivity = newSensiticity;
        ResetTumbler();
    }

    public void ResetTumbler()
    {
        IsLocked = false;
        transform.position = startPosition;
        RandomizeStopPoint();
    }
}
