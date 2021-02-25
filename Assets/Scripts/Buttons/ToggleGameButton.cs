using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameButton : MonoBehaviour
{
    public GameObject LockpickingGame;
    public void OnToggleGamePressed()
    {
        Debug.Log("Clicked");
        Debug.Log(LockpickingGame.activeInHierarchy);

        LockpickingGame.SetActive(!LockpickingGame.activeInHierarchy);
    }
}
