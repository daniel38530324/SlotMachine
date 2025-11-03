using UnityEngine;
using System.Collections.Generic;

public class Slot : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    [SerializeField] private Transform[] icons;
    [SerializeField] private float slotSpeed = 3000;
    private bool isStop = true;
    private void Update()
    {
        if(isStop) return;
        foreach(Transform item in icons)
        {
            item.Translate(0, -slotSpeed * Time.deltaTime, 0);
            if(item.transform.position.y <= points[points.Count - 1].transform.position.y)
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
        
        foreach(Transform item in icons)
        {
            int num = 0;
            float minDistance = Mathf.Infinity;
            
            foreach (Transform point in points)
            {
                float currentDistance = Vector3.Distance(item.position, point.position);
                if(currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    item.transform.position = point.transform.position;
                    num = points.IndexOf(point);
                }
            }
            points.RemoveAt(num);
        }
    }
}
