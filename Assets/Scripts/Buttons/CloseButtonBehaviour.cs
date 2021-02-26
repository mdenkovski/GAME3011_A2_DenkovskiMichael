using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonBehaviour : MonoBehaviour
{

    [SerializeField]
    private GameObject PanelToClose;

    public void OnCloseButtonPressed()
    {
        PanelToClose.SetActive(false);
    }
}
