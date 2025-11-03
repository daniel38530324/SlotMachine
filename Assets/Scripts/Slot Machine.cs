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
    private string[,] board;
    private List<List<Vector2Int>> paylines = new List<List<Vector2Int>> {
        // 橫線
        new List<Vector2Int> { new(0,0), new(1,0), new(2,0), new(3,0), new(4,0) }, // 上排
        new List<Vector2Int> { new(0,1), new(1,1), new(2,1), new(3,1), new(4,1) }, // 第二排
        new List<Vector2Int> { new(0,2), new(1,2), new(2,2), new(3,2), new(4,2) }, // 第三排
        new List<Vector2Int> { new(0,3), new(1,3), new(2,3), new(3,3), new(4,3) }, // 最下排
        // 斜線
        new List<Vector2Int> { new(0,0), new(1,1), new(2,2), new(3,3), new(4,3) }, // 左上 → 右下
        new List<Vector2Int> { new(0,3), new(1,2), new(2,1), new(3,0), new(4,0) }  // 左下 → 右上
    };

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
    }

    public void CheckIcon()
    {
        board = new string[4, 5]
        {
            {icons[0].tag, icons[4].tag, icons[8].tag, icons[12].tag, icons[16].tag},
            {icons[1].tag, icons[5].tag, icons[9].tag, icons[13].tag, icons[17].tag},
            {icons[2].tag, icons[6].tag, icons[10].tag, icons[14].tag, icons[18].tag},
            {icons[3].tag, icons[7].tag, icons[11].tag, icons[15].tag, icons[19].tag}
        };

        // foreach(string item in board)
        // {
        //     Debug.Log(item);
        // }

        Debug.Log(board[0,0]);

        foreach (var line in paylines)
        {
            string firstSymbol = board[line[0].y, line[0].x];
            int count = 1;

            // 檢查連續相同圖案
            for (int i = 1; i < line.Count; i++)
            {
                string currentSymbol = board[line[i].y, line[i].x];
                if (currentSymbol == firstSymbol)
                    count++;
                else
                    break;
            }

            // 假設3個以上才算中獎
            if (count >= 3)
            {
                Debug.Log($"中獎線！符號 {firstSymbol} × {count}");
                score += bet * 2;
                scoreText.text = "Score: " + score.ToString();
            }
        }
        // if(icons[1].tag == icons[5].tag || icons[9].tag == icons[13].tag)
        // {
        //     if(icons[1].tag == icons[5].tag && icons[9].tag == icons[13].tag && icons[13].tag == icons[17].tag)
        //     {
        //         score += bet * 2;
        //         return;
        //     }
        //     score += bet * 2;
        // }
    }

    IEnumerator StopMatchine()
    {
        foreach (Slot item in slots)
        {
            item.Stop();
            yield return new WaitForSeconds(0.5f);
        }

        foreach (Icon item in icons)
        {
            item.SetCollider(true);
        }

        yield return new WaitForSeconds(0.5f);
        CheckIcon();

        // slots[0].Stop();
        // yield return new WaitForSeconds(0.5f);
        // slots[1].Stop();
        // yield return new WaitForSeconds(0.5f);
        // slots[2].Stop();
        // yield return new WaitForSeconds(0.5f);
        // slots[3].Stop();
        // yield return new WaitForSeconds(0.5f);
        // slots[4].Stop();

    }
}
