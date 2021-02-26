using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{

    [SerializeField]
    private TMP_Text RemainingSecondsText;
    public float MaxTime = 7.0f;
    [SerializeField]
    private float countdownTime;

    [SerializeField]
    private Slider RemainingSecondsSlider;
    [SerializeField]
    private Image SliderFill;

    [SerializeField]
    private LockpickBehaviourScript Lockpick;

    [SerializeField]
    private GameObject FailedCanvas;
    [SerializeField]
    private GameObject SucessCanvas;

    [SerializeField]
    private int NumTumblersCorrect = 0;


    public bool TimerEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!TimerEnabled) return; //if timer isnt enabled dont update


        if (NumTumblersCorrect == 5)
        {
            Sucess();
        }


        countdownTime -= Time.deltaTime;

        SliderFill.color = new Color( (1- (countdownTime/ MaxTime)), (countdownTime / MaxTime), 0,1);
        RemainingSecondsSlider.value = countdownTime;
        RemainingSecondsText.text = ((int)countdownTime).ToString() + " Seconds";

        if (countdownTime <= 0)
        {
            GameOver();
        }

    }

    public void ResetTimer()
    {
        countdownTime = MaxTime;
        RemainingSecondsSlider.maxValue = MaxTime;
        RemainingSecondsSlider.value = countdownTime;
        SliderFill.color = Color.green;
        RemainingSecondsText.text = ((int)countdownTime).ToString() + " Seconds";
        NumTumblersCorrect = 0;
    }

    public void StartGame()
    {
        TimerEnabled = true;
        ResetTimer();
    }

    private void GameOver()
    {
        TimerEnabled = false;
        Lockpick.DisableLockpick();
        FailedCanvas.SetActive(true);
        
    }

    public void StopTimer()
    {
        TimerEnabled = false;
    }

    public void Sucess()
    {
        TimerEnabled = false;
        Lockpick.DisableLockpick();
        SucessCanvas.SetActive(true);
    }

    public void TumblerCorrect()
    {
        NumTumblersCorrect += 1;
    }

}
