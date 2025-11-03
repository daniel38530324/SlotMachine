using UnityEngine;

public class Icon : MonoBehaviour
{
    public string tag {get; private set;}
    [SerializeField] private LayerMask targetLayer;
    private bool isCollider;
    private bool isStop;

    public float radius = 3f;
    

    void Update()
    {
        if (isCollider)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

            foreach (Collider2D hit in hits)
            {
                tag = hit.gameObject.tag;
                Debug.Log("偵測到2D物件：" + hit.name);
            }
        }
    }


    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     if(isCollider)
    //     {
    //         tag = other.gameObject.tag;
    //     }
    // }

    public void SetCollider(bool isCollider)
    {
        this.isCollider = isCollider;
    }

    // void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.cyan;
    //     Gizmos.DrawWireSphere(transform.position, radius);
    // }
}
