using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 4f;
    public GameManager Manager;
    
    float h;
    int h1;
    float v;
    int v1;
    bool isHorizonMove;
    Animator anim;
    Vector3 dirvec;
    Rigidbody2D rigid;
    GameObject scanObject;

    //Mobile Key var
    int up_Value;
    int down_Value;
    int left_Value;
    int right_Value;
    bool up_down;
    bool down_down;
    bool left_down;
    bool right_down;
    bool up_up;
    bool down_up;
    bool left_up;
    bool right_up;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //PC 움직임 값
        h = Manager.isAction? 0 : Input.GetAxisRaw("Horizontal");
        v = Manager.isAction? 0 : Input.GetAxisRaw("Vertical");
        h1 = (int)h;
        v1 = (int)v;
        //Moblie
        h = Manager.isAction ? 0 : right_Value+left_Value;
        v = Manager.isAction ? 0 : up_Value+down_Value;
        h1 = (int)h;
        v1 = (int)v;


        //버튼 업다운
        bool hDown = Manager.isAction? false : Input.GetButtonDown("Horizontal")|| right_down || left_down;
        bool hUp = Manager.isAction ? false : Input.GetButtonUp("Horizontal")|| right_up || left_up;
        bool vDown = Manager.isAction ? false : Input.GetButtonDown("Vertical")||up_down || down_down;
        bool vUp = Manager.isAction ? false : Input.GetButtonUp("Vertical")|| up_up || down_up;
 

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
            Manager.Action(scanObject);
        }

        //Mobile Var init
        up_down=false;
        down_down=false;
        left_down=false;
        right_down=false;
        up_up=false;
        down_up=false;
        left_up=false;
        right_up = false;
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

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 1;
                up_down = true;
                break;
            case "D":
                down_Value =- 1;
                down_down = true;
                break;
            case "L":
                left_Value = -1;
                left_down = true;
                break;
            case "R":
                right_Value = 1;
                right_down = true;
                break;
            case "A":
                if (scanObject != null)
                {
                    Manager.Action(scanObject);
                }
                break;
            case "C":
                Manager.SubmenuActive();
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "U":
                up_Value = 0;
                up_up = true;
                break;
            case "D":
                down_Value = 0;
                down_up = true;
                break;
            case "L":
                left_Value = 0;
                left_up = true;
                break;
            case "R":
                right_Value = 0;
                right_up = true;
                break;
        }
    }
}
