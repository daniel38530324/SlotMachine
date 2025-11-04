using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Slot : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform[] icons;
    [SerializeField] private float slotSpeed = 3000;
    private bool isStop = true;
    private List<Transform> tempPoints;

    private void Start()
    {
        tempPoints = points.ToList();
    }
    private void Update()
    {
        if(isStop) return;
        foreach(Transform item in icons)
        {
            item.Translate(0, -slotSpeed * Time.deltaTime, 0);
            if(item.transform.position.y <= points[6].transform.position.y)
            {
                item.transform.position = points[0].transform.position;
            }
        }
    }

    public void Begin()
    {
        isStop = false;
    }

    public void Stop()
    {
        isStop = true;
        foreach (Transform item in icons)
        {
            float minDistance = Mathf.Infinity;
            int closestIndex = -1;

            for (int i = 0; i < tempPoints.Count; i++)
            {
                float currentDistance = Vector3.Distance(item.position, tempPoints[i].position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestIndex = i;
                }
            }

            if (closestIndex != -1)
            {
                item.position = tempPoints[closestIndex].position;
                tempPoints.RemoveAt(closestIndex);
            }
        }

        tempPoints = points.ToList();
    }
}
