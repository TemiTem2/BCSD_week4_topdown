using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float h;
    float v;

    Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2 (h, v);
    }
}
