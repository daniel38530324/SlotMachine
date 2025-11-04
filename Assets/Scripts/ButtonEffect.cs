using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    public void SetScale(bool isEnter)
    {
        if(isEnter)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
