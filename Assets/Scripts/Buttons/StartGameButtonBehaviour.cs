using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private TimerManager Timer;

    [SerializeField]
    private LockpickBehaviourScript Lockpick;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStartButtonPressed()
    {
        Timer.StartGame();
        Lockpick.EnableLockpick();
    }
}
