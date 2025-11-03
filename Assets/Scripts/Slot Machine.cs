using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;
using NUnit.Framework;
using System.Collections.Generic;

public class SlotMachine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_InputField betInput;
    [SerializeField] private Slot[] slots;
    [SerializeField] private float slotSpeed = 10;
    private int score = 1000, bet;

    private void Update()
    {

    }
    
    public void Begin()
    {
        bet = int.Parse(betInput.text);
        score -= bet;
        scoreText.text = "Score: " + score.ToString();

        foreach (Slot item in slots)
        {
            item.Begin();
        }
    }

    public void Stop()
    {
        foreach (Slot item in slots)
        {
            item.Stop();
        }
    }
}
