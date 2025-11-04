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
    [SerializeField] private UILineRenderer prizeLine;
    [SerializeField] private GameObject addScore;
    [SerializeField] private Button startButton, stopButton;
    [SerializeField] private TMP_Text dialogText;
    [SerializeField] private Slot[] slots;
    [SerializeField] private Icon[] icons;

    [Header("Seting")]
    [SerializeField] private float slotSpeed = 10;

    private int score = 1000, bet;
    private string[,] board;
    private List<List<Vector2Int>> paylines = new List<List<Vector2Int>> 
    {    
        new List<Vector2Int> { new(0,1), new(1,1), new(2,1), new(3,1), new(4,1) }, // 1
        new List<Vector2Int> { new(0,2), new(1,2), new(2,2), new(3,2), new(4,2) }, // 2
        new List<Vector2Int> { new(0,0), new(1,0), new(2,0), new(3,0), new(4,0) }, // 3
        new List<Vector2Int> { new(0,3), new(1,3), new(2,3), new(3,3), new(4,3) }, // 4
        new List<Vector2Int> { new(0,1), new(1,2), new(2,3), new(3,2), new(4,1) }, // 5
        new List<Vector2Int> { new(0,2), new(1,1), new(2,0), new(3,1), new(4,2) }, // 6
        new List<Vector2Int> { new(0,0), new(1,0), new(2,1), new(3,2), new(4,3) }, // 7
        new List<Vector2Int> { new(0,3), new(1,3), new(2,2), new(3,1), new(4,0) }, // 8
        new List<Vector2Int> { new(0,1), new(1,0), new(2,0), new(3,0), new(4,1) }, // 9
        new List<Vector2Int> { new(0,2), new(1,3), new(2,3), new(3,3), new(4,2) }, // 10
        new List<Vector2Int> { new(0,0), new(1,1), new(2,2), new(3,3), new(4,3) }, // 11
        new List<Vector2Int> { new(0,3), new(1,2), new(2,1), new(3,0), new(4,0) }, // 12
        new List<Vector2Int> { new(0,1), new(1,0), new(2,1), new(3,2), new(4,1) }, // 13
        new List<Vector2Int> { new(0,2), new(1,3), new(2,2), new(3,1), new(4,2) }, // 14
        new List<Vector2Int> { new(0,0), new(1,1), new(2,0), new(3,1), new(4,0) }, // 15
        new List<Vector2Int> { new(0,3), new(1,2), new(2,3), new(3,2), new(4,3) }, // 16
        new List<Vector2Int> { new(0,1), new(1,2), new(2,1), new(3,0), new(4,1) }, // 17
        new List<Vector2Int> { new(0,2), new(1,1), new(2,2), new(3,3), new(4,2) }, // 18
        new List<Vector2Int> { new(0,0), new(1,1), new(2,1), new(3,1), new(4,0) }, // 19
        new List<Vector2Int> { new(0,3), new(1,2), new(2,2), new(3,2), new(4,3) }, // 20
        new List<Vector2Int> { new(0,1), new(1,1), new(2,2), new(3,3), new(4,3) }, // 21
        new List<Vector2Int> { new(0,2), new(1,2), new(2,1), new(3,0), new(4,0) }, // 22
        new List<Vector2Int> { new(0,1), new(1,1), new(2,0), new(3,1), new(4,1) }, // 23
        new List<Vector2Int> { new(0,2), new(1,2), new(2,3), new(3,2), new(4,2) }, // 24
        new List<Vector2Int> { new(0,1), new(1,2), new(2,2), new(3,2), new(4,3) }, // 25
        new List<Vector2Int> { new(0,2), new(1,1), new(2,1), new(3,1), new(4,0) }, // 26
        new List<Vector2Int> { new(0,0), new(1,0), new(2,1), new(3,0), new(4,0) }, // 27
        new List<Vector2Int> { new(0,3), new(1,3), new(2,2), new(3,3), new(4,3) }, // 28
        new List<Vector2Int> { new(0,0), new(1,1), new(2,2), new(3,2), new(4,3) }, // 29
        new List<Vector2Int> { new(0,3), new(1,2), new(2,1), new(3,1), new(4,0) }, // 30
        new List<Vector2Int> { new(0,0), new(1,0), new(2,0), new(3,1), new(4,2) }, // 31
        new List<Vector2Int> { new(0,3), new(1,3), new(2,3), new(3,2), new(4,1) }, // 32
        new List<Vector2Int> { new(0,1), new(1,0), new(2,0), new(3,1), new(4,2) }, // 33
        new List<Vector2Int> { new(0,2), new(1,3), new(2,3), new(3,2), new(4,1) }, // 34
        new List<Vector2Int> { new(0,0), new(1,1), new(2,1), new(3,2), new(4,3) }, // 35
        new List<Vector2Int> { new(0,3), new(1,2), new(2,2), new(3,1), new(4,0) }, // 36
        new List<Vector2Int> { new(0,1), new(1,0), new(2,1), new(3,2), new(4,3) }, // 37
        new List<Vector2Int> { new(0,2), new(1,3), new(2,2), new(3,1), new(4,0) }, // 38
        new List<Vector2Int> { new(0,0), new(1,1), new(2,2), new(3,3), new(4,2) }, // 39
        new List<Vector2Int> { new(0,3), new(1,2), new(2,1), new(3,0), new(4,1) }, // 40
        new List<Vector2Int> { new(0,1), new(1,0), new(2,1), new(3,0), new(4,1) }, // 41
        new List<Vector2Int> { new(0,2), new(1,3), new(2,2), new(3,3), new(4,2) }, // 42
        new List<Vector2Int> { new(0,0), new(1,1), new(2,0), new(3,1), new(4,2) }, // 43
        new List<Vector2Int> { new(0,3), new(1,2), new(2,3), new(3,2), new(4,1) }, // 44
        new List<Vector2Int> { new(0,2), new(1,1), new(2,0), new(3,0), new(4,1) }, // 45
        new List<Vector2Int> { new(0,1), new(1,2), new(2,3), new(3,3), new(4,2) }, // 46
        new List<Vector2Int> { new(0,2), new(1,1), new(2,0), new(3,0), new(4,0) }, // 47
        new List<Vector2Int> { new(0,1), new(1,2), new(2,3), new(3,3), new(4,3) }, // 48
        new List<Vector2Int> { new(0,0), new(1,0), new(2,1), new(3,2), new(4,2) }, // 49
        new List<Vector2Int> { new(0,3), new(1,3), new(2,2), new(3,1), new(4,1) }, // 50
    };
    
    public void Begin()
    {
        bet = int.Parse(betInput.text);
        score -= bet;
        startButton.interactable = false;
        stopButton.interactable = true;
        scoreText.text = "分數: " + score.ToString();

        foreach (Slot item in slots)
        {
            item.Begin();
        }

        foreach (Icon item in icons)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }

        if(prizeLine.transform.childCount > 0)
        {
            foreach (Transform child in prizeLine.transform)
            {
                Destroy(child.gameObject);
            }
        }

        Setdialog(false);
    }

    public void Stop()
    {
        stopButton.interactable = false;
        StartCoroutine(StopMatchine());
    }

    // public void CheckIcon()
    // {
    //     board = new string[4, 5]
    //     {
    //         {icons[0].tag, icons[4].tag, icons[8].tag, icons[12].tag, icons[16].tag},
    //         {icons[1].tag, icons[5].tag, icons[9].tag, icons[13].tag, icons[17].tag},
    //         {icons[2].tag, icons[6].tag, icons[10].tag, icons[14].tag, icons[18].tag},
    //         {icons[3].tag, icons[7].tag, icons[11].tag, icons[15].tag, icons[19].tag}
    //     };

    //     foreach (var line in paylines)
    //     {
    //         string firstSymbol = board[line[0].y, line[0].x];
    //         int count = 1;
    //         List<RectTransform> pointRectTransform = new List<RectTransform>();
    //         pointRectTransform.Add(GetIconTransform(line[0].x, line[0].y));

    //         for (int i = 1; i < line.Count; i++)
    //         {
    //             string currentSymbol = board[line[i].y, line[i].x];
    //             if (currentSymbol == firstSymbol)
    //             {
    //                 count++;
    //                 pointRectTransform.Add(GetIconTransform(line[i].x, line[i].y));
    //             }
    //             else
    //             {
    //                 break;
    //             }
                    
    //         }

    //         if (count >= 3)
    //         {
    //             Debug.Log($"中獎線！符號 {firstSymbol} × {count} -- {paylines.IndexOf(line)}號線");
    //             switch(count)
    //             {
    //                 case 3:
    //                 addScore.GetComponent<TMP_Text>().text = (bet * 3).ToString();
    //                 score += bet * 3;
    //                     break;
    //                 case 4:
    //                 addScore.GetComponent<TMP_Text>().text = (bet * 3).ToString();
    //                 score += bet * 4;
    //                     break;
    //                 case 5:
    //                 addScore.GetComponent<TMP_Text>().text = (bet * 3).ToString();
    //                 score += bet * 5;
    //                     break;
    //             }
    //             scoreText.text = "Score: " + score.ToString();
    //             for (int i = 0; i < pointRectTransform.Count; i++)
    //             {
    //                 prizeLine.AppendUIElement(pointRectTransform[i]);
    //                 pointRectTransform[i].transform.GetChild(0).gameObject.SetActive(true);
    //             }
                
    //             Instantiate(addScore.gameObject, pointRectTransform[(pointRectTransform.Count - 1) / 2].transform.position, Quaternion.identity, transform);
    //         }
    //     }
    // }

    public void CheckIcon()
    {
        board = new string[4, 5]
        {
            {icons[0].tag, icons[4].tag, icons[8].tag, icons[12].tag, icons[16].tag},
            {icons[1].tag, icons[5].tag, icons[9].tag, icons[13].tag, icons[17].tag},
            {icons[2].tag, icons[6].tag, icons[10].tag, icons[14].tag, icons[18].tag},
            {icons[3].tag, icons[7].tag, icons[11].tag, icons[15].tag, icons[19].tag}
        };

        int winLineCount = 0, winScore = 0;

        foreach (var line in paylines)
        {
            string firstSymbol = board[line[0].y, line[0].x];
            int count = 1;
            List<RectTransform> pointRectTransform = new List<RectTransform>();
            pointRectTransform.Add(GetIconTransform(line[0].x, line[0].y));

            for (int i = 1; i < line.Count; i++)
            {
                string currentSymbol = board[line[i].y, line[i].x];
                if (currentSymbol == firstSymbol)
                {
                    count++;
                    pointRectTransform.Add(GetIconTransform(line[i].x, line[i].y));
                }
                else break;
            }

            if (count >= 3)
            {
                winLineCount++;
                Debug.Log($"中獎線！符號 {firstSymbol} × {count} -- {paylines.IndexOf(line)}號線");

                switch(count)
                {
                    case 3:
                        winScore += bet * 3;
                        addScore.GetComponent<TMP_Text>().text = "+" + (bet * 3).ToString();
                        score += bet * 3;
                        break;
                    case 4:
                        winScore += bet * 4;
                        addScore.GetComponent<TMP_Text>().text = "+" + (bet * 4).ToString();
                        score += bet * 4;
                        break;
                    case 5:
                        winScore += bet * 5;
                        addScore.GetComponent<TMP_Text>().text = "+" + (bet * 4).ToString();
                        score += bet * 5;
                        break;
                }
                scoreText.text = "分數: " + score.ToString();
                dialogText.text = "你贏得 " + winScore + " 點";
                Setdialog(true);

                UILineRenderer newLine = Instantiate(prizeLine, prizeLine.transform);
                newLine.ResetSelf();
                newLine.SetWidth(15f);
                newLine.SetPoints(pointRectTransform);

                switch(winLineCount)
                {
                    case 1:
                        newLine.SetLineColor(Color.red);
                        break;
                    case 2:
                        newLine.SetLineColor(Color.blue);
                        break;
                    default:
                        newLine.SetLineColor(Random.ColorHSV(0f, 1f, 1f, 1f, 0.8f, 1f));
                        break;
                }
                
                StartCoroutine(BlinkLine(newLine, 0.5f, 4));

                foreach (var icon in pointRectTransform)
                {
                    icon.transform.GetChild(0).gameObject.SetActive(true);
                }

                int midIndex = (pointRectTransform.Count - 1) / 2;
                Instantiate(addScore.gameObject, pointRectTransform[midIndex].transform.position, Quaternion.identity, transform);
            }
            startButton.interactable = true;
        }
        if(winLineCount == 0)
        {
            dialogText.text = "再接再厲";
            Setdialog(true);
        }
    }

    private IEnumerator BlinkLine(UILineRenderer line, float interval, int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            line.enabled = false;
            yield return new WaitForSeconds(interval);
            line.enabled = true;
            yield return new WaitForSeconds(interval);
        }
    }

    private RectTransform GetIconTransform(int x, int y)
    {
        int index = y + x * 4;
        return icons[index].GetComponent<RectTransform>();
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
    }

    private void Setdialog(bool isActive)
    {
        if(isActive)
        {
            dialogText.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            dialogText.transform.parent.gameObject.SetActive(false);
        }
    }
}
