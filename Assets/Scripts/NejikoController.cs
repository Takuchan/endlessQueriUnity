using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NejikoController: MonoBehaviour 
{
	const int MinLane = -2;
	const int MaxLane = 2;
	const float LaneWidth = 1.0f;
	const int DefaultLife = 3;
	const float StunDuration = 0.5f;

    //自分加筆
    private int level = 1;
    public Text levelGUI;
    private int countborder = 50;
    private int count = 0;
    //加筆終わり
 

	CharacterController controller;
	Animator animator;

	Vector3 moveDirection = Vector3.zero;
	int targetLane;
	int life = DefaultLife;
	float recoverTime = 0.0f;

	public float gravity;
	public float speedZ;
	public float speedX = 10;
	public float speedJump;
	public float accelerationZ;

    //音声ファイル再生　つまり効果音
    public AudioSource[] sources;



	public int Life ()
	{
		return life;
	}

	public bool IsStan ()
	{
		return recoverTime > 0.0f || life <= 0;
	}

	void Start ()
	{
		// 必要なコンポーネントを自動取得
		controller = GetComponent<CharacterController>();
		animator = GetComponent<Animator>();
        sources = gameObject.GetComponents<AudioSource>();

        //自分加筆（レベル上げ機能）
        levelGUI.text = "レベル " + level.ToString();
        //自分加筆終わり
    }

	void Update ()
	{
		// デバッグ用
		if (Input.GetKeyDown("left")) MoveToLeft();
		if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();
        //自分加筆（レベル上げ機能）
        if (transform.position.z >= countborder)
        {
            countborder += 50;
            count++;
            if (count <= 8)
            {
                Leveup();
        
            }
        if (level == 1 && speedX > 5) { speedX = 5; }
        if (level == 2 && speedX > 10) { speedX = 15; }
        if (level == 3 && speedX > 15) { speedX = 20; }
        if (level == 4 && speedX > 20) { speedX = 25; }
        if (level == 5 && speedX > 25) { speedX = 30; }
        }
        
        //自分加筆終わり






        if (IsStan())
		{
			// 動きを止め気絶状態からの復帰カウントを進める
			moveDirection.x = 0.0f;
			moveDirection.z = 0.0f;
			recoverTime -= Time.deltaTime;
		}
		else
		{
			// 徐々に加速しZ方向に常に前進させる
			float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime); 
			moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

			// X方向は目標のポジションまでの差分の割合で速度を計算
			float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
			moveDirection.x = ratioX * speedX;
		}

		// 重力分の力を毎フレーム追加
		moveDirection.y -= gravity * Time.deltaTime;

		// 移動実行
		Vector3 globalDirection = transform.TransformDirection(moveDirection);
		controller.Move(globalDirection * Time.deltaTime);

		// 移動後接地してたらY方向の速度はリセットする
		if (controller.isGrounded) moveDirection.y = 0;

		// 速度が0以上なら走っているフラグをtrueにする
		animator.SetBool("run", moveDirection.z > 0.0f);
	}

	// 左のレーンに移動を開始
	public void MoveToLeft ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane > MinLane) targetLane--;
        sources[0].Play();
	}

	// 右のレーンに移動を開始
	public void MoveToRight ()
	{
		if (IsStan()) return;
		if (controller.isGrounded && targetLane < MaxLane) targetLane++;
        sources[1].Play();
	}

	public void Jump ()
	{
		if (IsStan()) return;
       
        if (controller.isGrounded) 
		{

			moveDirection.y = speedJump;

			// ジャンプトリガーを設定
			animator.SetTrigger("jump");
            sources[2].Play();  
		}
	}
	
	// CharacterControllerにコンジョンが生じたときの処理
	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		if(IsStan()) return;

		if(hit.gameObject.tag == "Robo")
		{
			// ライフを減らして気絶状態に移行
			life--;
			recoverTime = StunDuration;

			// ダメージトリガーを設定
			animator.SetTrigger("damage");

			// ヒットしたオブジェクトは削除
			Destroy(hit.gameObject);
		}
	}
    //自分加筆
    void Leveup()
    {
        if (count == 1) { level = 2; }
        if (count == 3) { level = 3; }
        if (count == 5) { level = 4; }
        if (count == 8) { level = 5; }
        
        levelGUI.text = "レベル " + level.ToString();
    }

}