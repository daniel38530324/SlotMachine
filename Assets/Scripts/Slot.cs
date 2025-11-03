using UnityEngine;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private Transform[] icons;
    [SerializeField] private float slotSpeed = 3000;
    private bool isStop = true;
    private Transform[] originPoints;

    private void Start()
    {
        originPoints = points.ToArray();
    }
    private void Update()
    {
        if(isStop) return;
        foreach(Transform item in icons)
        {
            item.Translate(0, -slotSpeed * Time.deltaTime, 0);
            if(item.transform.position.y <= originPoints[6].transform.position.y)
            {
                item.transform.position = originPoints[0].transform.position;
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

            for (int i = 0; i < points.Count; i++)
            {
                float currentDistance = Vector3.Distance(item.position, points[i].position);
                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    closestIndex = i;
                }
            }

            if (closestIndex != -1)
            {
                item.position = points[closestIndex].position;
                points.RemoveAt(closestIndex);
            }
        }
    }
}
