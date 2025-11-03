using UnityEngine;

public class Icon : MonoBehaviour
{
    public string tag {get; private set;}
    private bool isCollider;
    private bool isStop;

    private void OnTriggerStay(Collider other)
    {
        if(!isCollider) return;
        tag = other.gameObject.tag;
    }

    public void SetCollider(bool isCollider)
    {
        this.isCollider = isCollider;
    }
}
