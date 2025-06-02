using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("�ٶ�")]
    public float rawSpeed;
    public float factor;
    public float speed;
    public float jumpSpeed;
    [Header("��Ծ�߶�")]
    public float secondJumpDistance;
    Rigidbody2D rg;
    Vector2 movement;
    Ray2D downRay;
    RaycastHit2D hitInfo;
    bool isGrounded;
    bool isMoving;
    bool isSecondJump;
    public int jumpCount;
    Animator animator;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        rg = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        //�����ٶ�
        speed = rawSpeed * factor;
        //������
        movement = new Vector2(Input.GetAxis("Horizontal") * speed, rg.linearVelocity.y + BoolToInt(Input.GetButtonDown("Jump")) * jumpSpeed * (BoolToInt(isGrounded))) ;
        if(!isGrounded&&jumpCount == 0)
        {
            movement = new Vector2(Input.GetAxis("Horizontal") * speed, rg.linearVelocity.y + BoolToInt(Input.GetButtonDown("Jump")) * jumpSpeed * (BoolToInt(isSecondJump)));     
        }
        if(isGrounded)
        {
            if(jumpCount != 0)
            {
                Debug.Log(jumpCount);
            }
            jumpCount = 0;
        }
        if(Input.GetButtonDown("Jump"))
        {
            jumpCount++;
        }
        rg.linearVelocity = movement;   
        //�ж�����
        downRay = new Ray2D(this.transform.position, Vector2.down);
        hitInfo = Physics2D.Raycast(downRay.origin, downRay.direction);
        isGrounded = hitInfo.collider != null && hitInfo.distance <= secondJumpDistance;
        isSecondJump = hitInfo.distance > secondJumpDistance && hitInfo.distance <= 4*secondJumpDistance && jumpCount <=1;
        //Debug.Log(hitInfo.distance);
        isMoving = movement.magnitude != 0;
        SetUpAnimator();
        //��ҷ���仯
        if(Input.GetAxisRaw("Horizontal") != 0)
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") < 0;
        }
    }
    void SetUpAnimator()
    {
        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isMoving", isMoving);
    }
    int BoolToInt(bool a)
    {
        int b = 0;
        if(a)
        {
            b = 1;
        }
        return b;
    }
}
