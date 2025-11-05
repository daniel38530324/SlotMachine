using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance{ get; private set;}

    [SerializeField] private Transform canvas;
    [SerializeField] private Transform[] cloudPoint;
    [SerializeField] private GameObject[] clouds;

    private float timer = 0;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        SpawnCloud();
    }

    private void Update() 
    {
        timer += Time.deltaTime;
        if(timer >= 6)
        {
            timer = 0;
            SpawnCloud();
        }
    }

    private void SpawnCloud()
    {
            int cloudnum = Random.Range(0, 4);
            int pointNum = Random.Range(0, 2);
            if(pointNum == 1)
            {
                foreach (var item in clouds)
                {
                    item.GetComponent<Cloud>().xSpeed = -700;
                }
            }
            else
            {
                foreach (var item in clouds)
                {
                    item.GetComponent<Cloud>().xSpeed = 700;
                }
            }
            Instantiate(clouds[cloudnum], cloudPoint[pointNum].position, Quaternion.identity, canvas);
    }
}
