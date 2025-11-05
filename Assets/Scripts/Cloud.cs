using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Cloud : MonoBehaviour
{
    [field: SerializeField] public float xSpeed { get; set; }
    [field: SerializeField] public float ySpeed { get; set; }
    [field: SerializeField] public float scaleSpeed { get; set; }
    [field: SerializeField] public float rotateSpeed { get; set; }

    private void Start() 
    {
        Destroy(gameObject, 5);
    }

    private void Update() 
    {
        transform.Translate(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(2, 2, 2), scaleSpeed * Time.deltaTime);
        transform.GetChild(0).Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
