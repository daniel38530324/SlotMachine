using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.PlayerLoop;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;

public class SlotMachine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_InputField betInput;
    [SerializeField] private Slot[] slots;
    [SerializeField] private Icon[] icons;
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
        StartCoroutine(StopMatchine());
        
        // foreach (Icon item in icons)
        // {
        //     item.SetCollider(true);
        //     StartCoroutine(StopMatchine());
        // }

        //CheckIcon();
    }

    public void CheckIcon()
    {
        if(icons[1].tag == icons[5].tag || icons[9].tag == icons[13].tag)
        {
            if(icons[1].tag == icons[5].tag && icons[9].tag == icons[13].tag && icons[13].tag == icons[17].tag)
            {
                score += bet * 2;
                return;
            }
            score += bet * 2;
        }
    }

    IEnumerator StopMatchine()
    {
        slots[0].Stop();
        yield return new WaitForSeconds(0.5f);
        slots[1].Stop();
        yield return new WaitForSeconds(0.5f);
        slots[2].Stop();
        yield return new WaitForSeconds(0.5f);
        slots[3].Stop();
        yield return new WaitForSeconds(0.5f);
        slots[4].Stop();

    }
}
