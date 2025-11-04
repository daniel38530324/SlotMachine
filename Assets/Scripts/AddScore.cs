using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class AddScore : MonoBehaviour
{
    [SerializeField] private float moveSpeed, colorSpeed;

    private TMP_Text text;

    private void Awake() 
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start() 
    {
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        transform.Translate(0, moveSpeed * Time.deltaTime, 0);
        text.color = Color.Lerp(text.color, new Color(1, 1, 1, 0), colorSpeed * Time.deltaTime);
    }
}
