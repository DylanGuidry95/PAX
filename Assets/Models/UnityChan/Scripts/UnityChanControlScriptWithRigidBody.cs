//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class UnityChanControlScriptWithRigidBody : MonoBehaviour
{

    public float animSpeed = 1.5f;
    public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
    public bool useCurves = true;
    // このスイッチが入っていないとカーブは使われない
    public float useCurvesHeight = 0.5f;        

    // 以下キャラクターコントローラ用パラメタ
    // 前進速度
    public float forwardSpeed = 7.0f;
    // 後退速度
    public float backwardSpeed = 2.0f;
    // 旋回速度
    public float rotateSpeed = 2.0f;
    // ジャンプ威力
    public float jumpPower = 3.0f;
    // キャラクターコントローラ（カプセルコライダ）の参照
    private CapsuleCollider col;
    private Rigidbody rb;
    // キャラクターコントローラ（カプセルコライダ）の移動量
    private Vector3 velocity;
    // CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
    private float orgColHight;
    private Vector3 orgVectColCenter;
     

    // アニメーター各ステートへの参照
    

    // 初期化
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        
        orgColHight = col.height;
        orgVectColCenter = col.center;
    }

    public enum InputState
    {
        DEFAULT,
        CONTROLLER1,
        CONTROLLER2,
        KEYBOARD1,
        KEYBOARD2,
    }

    /// <summary>
    /// set from inputhandler
    /// </summary>
    public InputState inputType;
    
    enum PlayerState
    { 
        INIT,
        IDLE,
        LOCO,
        JUMP,
        REST,
    }
    private PlayerState c_state = PlayerState.INIT;
    private Animator anim;                          
    private AnimatorStateInfo currentBaseState;         
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int locoState = Animator.StringToHash("Base Layer.Locomotion");
    static int jumpState = Animator.StringToHash("Base Layer.Jump");
    static int restState = Animator.StringToHash("Base Layer.Rest");
    static int punchState = Animator.StringToHash("Base Layer.Punch");

    void FixedUpdate()
    {
    

        #region Input
        ///DEFAULT  
        string jump = "Jump";
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        switch (inputType)
        {
            case InputState.CONTROLLER1:
                h = Input.GetAxis("p1Horizontal");
                v = Input.GetAxis("p1Vertical");
                jump = "c1Jump";
                break;
            case InputState.CONTROLLER2:
                h = Input.GetAxis("p2Horizontal");
                v = Input.GetAxis("p2Vertical");
                jump = "c2Jump";
                break;
            case InputState.KEYBOARD1:
                h = Input.GetAxis("p1KeyBoardHorizontal");
                v = Input.GetAxis("p1KeyBoardVertical");
                break;
            case InputState.DEFAULT:
                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");
                break;

        }

        anim.SetFloat("Speed", v);
        anim.SetFloat("Direction", h);
        anim.speed = animSpeed;
        currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
        rb.useGravity = true;




        velocity = new Vector3(0, 0, v);

        velocity = transform.TransformDirection(velocity);

        if (v > 0.1)
        {
            velocity *= forwardSpeed;
        }
        else if (v < -0.1)
        {
            velocity *= backwardSpeed;
        }
        #endregion input
        
        if (Input.GetButtonDown("Attack"))
        {
            Debug.Log("punch");
            anim.SetTrigger("Punch");
        }
        if (Input.GetButtonDown(jump))
        {
            if (currentBaseState.fullPathHash == locoState)
            {
                if (!anim.IsInTransition(0))
                {
                    rb.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
                    c_state = PlayerState.JUMP;
                    anim.SetBool("Jump", true);
                }
            }
        }


        transform.localPosition += velocity * Time.fixedDeltaTime;

        transform.Rotate(0, h * rotateSpeed, 0);

        //moving
        
        if (currentBaseState.fullPathHash == locoState)
        {
            if (useCurves)
            {
                resetCollider();
            }
        }
        //jumping
        else if (currentBaseState.fullPathHash == jumpState)
        { 
            if (!anim.IsInTransition(0))
            {

                if (useCurves)
                {

                    float jumpHeight = anim.GetFloat("JumpHeight");
                    float gravityControl = anim.GetFloat("GravityControl");
                    if (gravityControl > 0)
                    {
                        rb.useGravity = false;
                    }
                    Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
                    RaycastHit hitInfo = new RaycastHit();
                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.distance > useCurvesHeight)
                        {
                            col.height = orgColHight - jumpHeight;
                            float adjCenterY = orgVectColCenter.y + jumpHeight;
                            col.center = new Vector3(0, adjCenterY, 0);
                        }
                        else
                        {
                            resetCollider();
                        }
                    }
                }
                anim.SetBool("Jump", false);
            }
        }
        //idle
        else if (currentBaseState.fullPathHash == idleState)
        { 
            if (useCurves)
            {
                resetCollider();
            } 
            if (Input.GetButtonDown(jump))
            {
                anim.SetBool("Rest", true);
            }
        } 
        //resting?
        else if (currentBaseState.fullPathHash == restState)
        { 
            if (!anim.IsInTransition(0))
            {
                anim.SetBool("Rest", false);
            }
        }
   
    }
     
    void resetCollider()
    { 
        col.height = orgColHight;
        col.center = orgVectColCenter;
    }
}