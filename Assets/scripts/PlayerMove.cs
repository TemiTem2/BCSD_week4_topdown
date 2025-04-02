using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 2f;

    float h;
    int h1;
    float v;
    int v1;
    bool isHorizonMove;
    Animator anim;
    Vector3 dirvec;
    Rigidbody2D rigid;
    GameObject scanObject;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //움직임 값
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        h1 = (int)h;
        v1 = (int)v;

        //버튼 업다운
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");


        //상하좌우확인
        if (hDown || vUp)
            isHorizonMove = true;
        else if (vDown || hUp)
            isHorizonMove = false;

        //애니메이션

        if (anim.GetInteger("hAxisRaw") != h1)
        {
            anim.SetBool("ischanged", true);
            anim.SetInteger("hAxisRaw", h1);
        }
        else if (anim.GetInteger("vAxisRaw") != v1)
        {
            anim.SetBool("ischanged", true);
            anim.SetInteger("vAxisRaw", v1);
        }
        else
            anim.SetBool("ischanged", false);

        //direction
        if(vDown && v1 == 1)
        {
            dirvec = Vector3.up;
        }
        else if (vDown && v1 == -1)
        {
            dirvec = Vector3.down;
        }
        else if (hDown && h1 == -1)
        {
            dirvec = Vector3.left;
        }
        else if (hDown && h1 == 1)
        {
            dirvec = Vector3.right;
        }

        //scan object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            Debug.Log("This is :"+scanObject.name);
        }

    }




    void FixedUpdate()
    {
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
        rigid.linearVelocity = moveVec * Speed;

        //ray
        Debug.DrawRay(rigid.position,dirvec * 0.7f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirvec, 0.7f,LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
