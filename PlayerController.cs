using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //二人プレイか
    public static bool Player2_Join;

    //Playerをコントローラーの左右どちらで操作するか
    public bool Player_LEFT;

    //死亡ライン
    public static float DETH_LINE;

    //加速減速値
    public static float SPEED_UP_DOWN;


    //スピード値
    private static float SPEED = 15.0f;

    //Rigidbody対応
    private Rigidbody rb;

    //三次方向
    private float MOVE_X;
    private float MOVE_Z;
    private float MOVE_Y;

    //入力値
    private float moveHorizontal;
    private float moveVertical;

    //加速値・減速値
    private float Speed_Up;
    private float Speed_Down;

    //ジャンプ
    private float Jump_Y;
    private float Jump_Sub;
    private bool Jump_bool;
    private bool Jump_Down;


    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody>();

        Jump_bool = false;
	
	}
	
	// Update is called once per frame
	void Update() {

        //ジャンプ処理
        if (Jump_bool == true && Jump_Down == false)
        {
            Jump_Y = 3.0f;
        }

        if (Jump_Y >= 3.0f)
        {
            Jump_Down = true;
        }

        if (Jump_Down == true)
        {
            Jump_Y -= 0.2f;
        }

        //ステージ落下
        if (this.transform.position.y <= DETH_LINE)
        {
            //ジャンプ無効化
            Jump_Y = 0.0f;
        }

        //回転停止

        float Speed = SPEED + Speed_Up - Speed_Down;

        if (Speed<= 0)
        {
            Speed = 0.0f;

            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.None;
        }

        //移動
        Vector3 movement = new Vector3(
            MOVE_X,
            MOVE_Y,
            MOVE_Z
            );

        rb.AddForce(movement * Speed);
        rb.AddForce(new Vector3(0, Jump_Y, 0) * 15.0f);
    }


    //コントローラー操作
    void FixedUpdate()
    {
        //二人でやるのか
        if (Player2_Join == true)
        {
            if (Player_LEFT == true)
            {
                //Player1
                moveHorizontal = Input.GetAxis("P1_RIGHT_X_MOVE");
                moveVertical = Input.GetAxis("P1_RIGHT_Y_MOVE");

                MOVE_X = moveHorizontal;
                MOVE_Z = moveVertical;

                //加速する設定（R2）
                if (Input.GetButton("P1_R2") || Input.GetButtonDown("P1_R2"))
                {
                    Speed_Up = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Up = 0;
                }

                //減速する設定（L2）
                if (Input.GetButton("P1_L2") || Input.GetButtonDown("P1_L2"))
                {
                    Speed_Down = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Down = 0;
                }

                //ジャンプする設定（×）
                if (Input.GetButtonDown("P1_Cross")&& Jump_bool == false)
                {
                    Jump_Y = 0.0f;

                    Jump_bool = true;
                }
            }
            else
            {
                //Player2
                moveHorizontal = Input.GetAxis("P2_RIGHT_X_MOVE");
                moveVertical = Input.GetAxis("P2_RIGHT_Y_MOVE");

                MOVE_X = moveHorizontal;
                MOVE_Z = moveVertical;

                //加速する設定（R2）
                if (Input.GetButton("P2_R2") || Input.GetButtonDown("P2_R2"))
                {
                    Speed_Up = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Up = 0;
                }

                //減速する設定（L2）
                if (Input.GetButton("P2_L2") || Input.GetButtonDown("P2_L2"))
                {
                    Speed_Down = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Down = 0;
                }

                //ジャンプする設定（×）
                if (Input.GetButtonDown("P2_Cross") && Jump_bool == false)
                {
                    Jump_Y = 0.0f;

                    Jump_bool = true;
                }
            }
        }
        else
        {
            //一人プレイ
            if (Player_LEFT == true)
            {
                //左
                moveHorizontal = Input.GetAxis("P1_REFT_X_MOVE");
                moveVertical = Input.GetAxis("P1_REFT_Y_MOVE");

                MOVE_X = moveHorizontal;
                MOVE_Z = moveVertical;

                //加速する設定（L2）
                if (Input.GetButton("P1_L2") || Input.GetButtonDown("P1_L2"))
                {
                    Speed_Up = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Up = 0;
                }

                //減速する設定（L1）
                if (Input.GetButton("P1_L1") || Input.GetButtonDown("P1_L1"))
                {
                    Speed_Down = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Down = 0;
                }

                //ジャンプする設定（□）
                if (Input.GetButtonDown("P1_Square")&& Jump_bool == false)
                {
                    Jump_Y = 0.0f;

                    Jump_bool = true;
                }
            }
            else
            {
                //右
                moveHorizontal = Input.GetAxis("P1_RIGHT_X_MOVE");
                moveVertical = Input.GetAxis("P1_RIGHT_Y_MOVE");

                MOVE_X = moveHorizontal;
                MOVE_Z = moveVertical;

                //加速する設定（R2）
                if (Input.GetButton("P1_R2") || Input.GetButtonDown("P1_R2"))
                {
                    Speed_Up = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Up = 0;
                }

                //減速する設定（R1）
                if (Input.GetButton("P1_R1") || Input.GetButtonDown("P1_R1"))
                {
                    Speed_Down = SPEED_UP_DOWN;
                }
                else
                {
                    Speed_Down = 0;
                }

                //ジャンプする設定(×)
                if (Input.GetButtonDown("P1_Cross") && Jump_bool == false)
                {
                    Jump_Y = 0.0f;

                    Jump_bool = true;
                }
            }
        }
    }

    //Stageに触れた瞬間
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stage"))
        {
            Jump_bool = false;

            Jump_Y = 0.0f;

            Jump_Down = false;
        }
    }
}
