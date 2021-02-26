using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private Difficulty Difficulty;

    [SerializeField]
    private TumblerBehaviour[] Tumblers;

    [SerializeField]
    private TMP_Text DifficultyText;

    [SerializeField]
    [Range(0.0f, 0.95f)]
    private float EasySensitivity = 0.3f;
    [SerializeField]
    [Range(0.0f, 0.95f)]
    private float MediumSensitivity = 0.5f;
    [SerializeField]
    [Range(0.0f, 0.95f)]
    private float HardSensitivity = 0.75f;

    [SerializeField]
    private TimerManager Timer;
    [SerializeField]
    private float MaxTimePerRound = 15;


    private void OnEnable()
    {
        SetDifficulty(Difficulty.Easy);
    }

    public void OnNewDifficulty(int chosenDifficulty)
    {
        SetDifficulty((Difficulty)chosenDifficulty);
    }

    public void SetDifficulty(Difficulty newDifficulty)
    {

        Difficulty = newDifficulty;

        float newSensitivity = 0.0f;
        switch (newDifficulty)
        {
            case Difficulty.Easy:
                newSensitivity = EasySensitivity;
                break;
            case Difficulty.Medium:
                newSensitivity = MediumSensitivity;
                break;
            case Difficulty.Hard:
                newSensitivity = HardSensitivity;
                break;
            default:
                break;
        }

        Timer.MaxTime = MaxTimePerRound;
        Timer.ResetTimer();

        UpdateDifficultyText();

        foreach (TumblerBehaviour tumbler in Tumblers)
        {
            tumbler.ChangeDifficulty(newSensitivity);
        }
    }

    private void UpdateDifficultyText()
    {
        DifficultyText.text = Difficulty.ToString();
    }


}
