using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//プレイヤーの状態
public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Hold,
    Throw,
    Damage,
    Dead
}
public class Player : MonoBehaviour
{
   //プレイヤーの座標
   Vector3 playerPos = Vector3.zero;
    //プレイヤーに加える方向
    Vector3 playerVelocity = Vector3.zero;


    //移動方向
    Vector3 Forward;
    Vector3 Back;
    Vector3 Right;
    Vector3 Left;
    Vector3 JumpPower = new Vector3(0,15,0);
    //キャラクターの向き
    public Vector3 moveDirection; //キャラクターの向き
    //プレイヤーマネージャー
    public PlayerManager _playerManager;


    //移動スピード
    float speed = 0.08f;

    //ジャンプのちから
    float jumpForce = 1.5f;
    public bool isGround = false;//地面についているかどうか
    Rigidbody rb;

    Vector3 verticalSpeed;
    Vector3 velocity;

    //アニメーション
    public Animator _animator;

    Warp_Controller _checkPoint;

    public Image fadeImage;
    bool isCoroutine = false;//コルーチンの旗

    public bool isMovie = false;
    [SerializeField] GameObject blackImage;//黒いImageで登録
    float darkDuration = 1f;//暗転時間

    // プレイヤーが鳴らす音のリスト 0: ジャンプ音 1: 投げる音 2: 持つ音
    [SerializeField] private List<AudioClip> _audioClips;
    // 音再生用AudioSource
    private AudioSource _audioSource;
    private bool isPlayThrowSE;
    private bool isPlayHoldSE;

    [SerializeField] private GameObject _uiManagerObj;
    private UIManager _uiManager;

    float fadeDuration = 2f;
    BowlingNokonokoController _bowling = null;//のこのこボーリング


    //最初のプレイヤーの状態
    public PlayerState _state = PlayerState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        blackImage.SetActive(false);
        rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _checkPoint = GameObject.Find("CheckPoint").GetComponent<Warp_Controller>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClips[0];
        isPlayThrowSE = false;
        isPlayHoldSE = false;

        Death();

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
        _uiManager = _uiManagerObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGround)//ジャンプ
        {
            // Debug.Log("飛んでいます");
            // ジャンプ音再生
            ChangeSE(0);
            SoundManager.Instance.PlaySE(_audioSource);

            rb.AddForce(JumpPower, ForceMode.Impulse);
            isGround = false;
            if (_state != PlayerState.Hold)
            {
                _animator.SetTrigger("TriggerJump");
            }
            if (_state == PlayerState.Hold)
            {
                _animator.SetTrigger("TriggerJumpHold");
            }
        }

        //Forward = Camera.main.transform.forward.normalized;
        //Forward.y = 0;
        //Back = -Forward;
        //Right = Camera.main.transform.right.normalized;
        //Right.y = 0;
        //Left = -Right;

        //キャラクターの向き
        moveDirection = playerVelocity;
        moveDirection.y = 0;//y軸を0にして、水平面のみ回転するようにする
        moveDirection.Normalize();
        
        //Debug.Log($"movedirectionは{moveDirection}");
    

    }

    private void FixedUpdate()
    {
        //Pad対応
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // -------------------------------------
        // ステート変更時に音を再生する用の処理
        if (_state == PlayerState.Hold)
        {
            if(!isPlayHoldSE)
            {
                // 持つ音再生
                ChangeSE(2);
                SoundManager.Instance.PlaySE(_audioSource);
                isPlayHoldSE = true;
            }
        }
        else
        {
            isPlayHoldSE = false;
        }

        if(_state == PlayerState.Throw)
        {
            if (!isPlayThrowSE)
            {
                // 投げる音再生
                ChangeSE(1);
                SoundManager.Instance.PlaySE(_audioSource);
                isPlayThrowSE = true;
            }
        }
        else
        {
            isPlayThrowSE = false;
        }
        // -------------------------------------

        //if (playerVelocity.magnitude == 0 && _state != PlayerState.Hold)
        //{
        //    _state = PlayerState.Idle;
        //}

        playerVelocity = Vector3.zero;
       // Debug.Log(isMovie);
        if (isMovie) return;
        if (!isGround)
        {
            transform.position += velocity * 2;//常にかかっている速度
        }
        Transform camTransform = Camera.main.transform;
        Vector3 camForward = Vector3.ProjectOnPlane(camTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(camTransform.right, Vector3.up).normalized;

        Forward = camForward;
        Back = -Forward;
        Right = camRight;
        Left = -Right;

        bool isHorizontalX = Mathf.Abs(camRight.x) > Mathf.Abs(camRight.z);
        //ジャンプの重力計算
        //verticalSpeed += gravity * Time.deltaTime;
        if (Input.GetKey(KeyCode.W) || v > 0.1f)//前移動
        {
            Vector3 move = Forward;
            if (isHorizontalX)
            {
                move.x = 0;
            }
            else
            {
                move.z = 0;
            }
            playerVelocity += move.normalized * speed;
           // Debug.Log("前に移動しています");
        }
        if (Input.GetKey(KeyCode.S) || v < -0.1f)//後ろ移動
        {
            Vector3 move = Back;
            if (isHorizontalX)
            {
                move.x = 0;
            }
            else
            {
                move.z = 0;
            }
            playerVelocity += move.normalized * speed;

        }
        if (Input.GetKey(KeyCode.A) || h < -0.1f)//左移動
        {
            Vector3 move = Left;
            if (isHorizontalX)
            {
                move.z = 0;
            }
            else
            {
                move.x = 0;
            }
            playerVelocity += move.normalized * speed;

        }
        if (Input.GetKey(KeyCode.D) || h > 0.1f)//右移動
        {
            Vector3 move = Right;
            if (isHorizontalX)
            {
                move.z = 0;
            }
            else
            {
                move.x = 0;
            }
            playerVelocity += move.normalized * speed;

        }

     

        //持ち歩き
        if (playerVelocity.magnitude != 0 && _state == PlayerState.Hold)
        {
            _animator.SetBool("isWalkHold", true);
        }
        //ただの歩き
        if (playerVelocity.magnitude != 0 && _state != PlayerState.Hold)
        {
            _animator.SetBool("isWalk", true);
        }
        

        //Debug.Log($"{playerVelocity}");
        //止まっているとき
        if (playerVelocity.magnitude < 0.00001f) //=0で動かなくてこんだけ小さくしてもfalseにならない//急にこうなった//一旦放置//今は解決(疑似)
        {
            _animator.SetBool("isWalk", false);
            _animator.SetBool("isRun",false);
            _animator.SetBool("isWalkHold", false);
            _animator.SetBool("isRunHold", false);
        }


        //単位ベクトル化(斜め用)
        playerVelocity = playerVelocity.normalized;
        playerVelocity = playerVelocity * speed;


        //走る
        if (Input.GetButton("Dash") && playerVelocity.magnitude != 0)
        {
         //   Debug.Log("走る");
            playerVelocity *= 1.5f;
            if (_state != PlayerState.Hold)
            {
                _animator.SetBool("isWalk", false);
                _animator.SetBool("isRun", true);
            }
            if (_state == PlayerState.Hold)
            {
                _animator.SetBool("isWalkHold", false);
                _animator.SetBool("isRunHold", true);
            }

        }
        else
        {
            _animator.SetBool("isRun", false);
            _animator.SetBool("isRunHold", false);
        }
            //加える
            transform.position += playerVelocity;

        //キャラの回転
        if (moveDirection.sqrMagnitude > 0.0001f)//ベクトルの長さの2乗
        {
            //Debug.Log("回転しています");
            Quaternion rotation = Quaternion.LookRotation(moveDirection);
            // Lerpでなめらかに補間
            float rotationSpeed = 10f; // 回転速度（調整可）
            transform.rotation = Quaternion.Lerp(
                transform.rotation,        // 現在の回転
                rotation,            // 目標の回転
                rotationSpeed * Time.deltaTime // 補間割合
                );
        }

        if (transform.position.y < 9)
        {
            if (!isCoroutine)
            {
                //
                //StartCoroutine(DarkenRoutine());
                isCoroutine = true;
                 StartCoroutine(DieSequence2());
            }

        }

    }

    //地面に触れたら着地と判定
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            velocity.y = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //HitBoxに移して呼び出すように変更した
        //if (isMovie) return;
        ////死ぬアニメーションとフェード
        //if (other.CompareTag("Hit"))
        //{
        //    isMovie = true;
        //    _animator.SetTrigger("TriggerDie");
        //    StartCoroutine(DieSequence());
        //}
        ////if (other.CompareTag("Enemy"))
        ////{
        ////    isMovie = true;
        ////    _animator.SetTrigger("TriggerDie");
        ////    StartCoroutine(DieSequence());
        ////}
        //if (other.CompareTag("Bowling"))
        //{

        //    _bowling = other.GetComponent<BowlingNokonokoController>();
        //    if (_bowling.currentState == BowlingNokonokoState.Boss)
        //    {
        //        isMovie = true;
        //        _animator.SetTrigger("TriggerDie");
        //        StartCoroutine(DieSequence());
        //    }
        //}


    }
    public void Die(Collider other)
    {
        if (isMovie) return;
        //死ぬアニメーションとフェード
        if (other.CompareTag("Hit"))
        {
            isMovie = true;
            _animator.SetTrigger("TriggerDie");
            StartCoroutine(DieSequence());
        }
        //if (other.CompareTag("Enemy"))
        //{
        //    isMovie = true;
        //    _animator.SetTrigger("TriggerDie");
        //    StartCoroutine(DieSequence());
        //}
        if (other.CompareTag("Bowling"))
        {

            _bowling = other.GetComponent<BowlingNokonokoController>();
            if (_bowling.currentState == BowlingNokonokoState.Boss)
            {
                isMovie = true;
                _animator.SetTrigger("TriggerDie");
                StartCoroutine(DieSequence());
            }
        }
    }
    private IEnumerator DieSequence()
    {
        Debug.Log("イーなむれーた");
        ChangeSE(3);
        SoundManager.Instance.PlaySE(_audioSource); // miss音再生
        yield return new WaitForSeconds(0.5f);
        //ここでフェード
        yield return StartCoroutine(FadeOut());
        _uiManager.MissAnimation(5, 0.3f, 0.3f, 0.3f); // MISS時のUIを動かすアニメーション


        yield return new WaitForSeconds(1.3f);
        Death();
       
        yield return StartCoroutine(FadeIn());
        isMovie = false;
    }
    private IEnumerator DieSequence2()
    {
        ChangeSE(3);
        SoundManager.Instance.PlaySE(_audioSource); // miss音再生
        //yield return new WaitForSeconds(0.5f);
        //ここでフェード
        yield return StartCoroutine(FadeOut2());
        _uiManager.MissAnimation(5, 0.3f, 0.3f, 0.3f); // MISS時のUIを動かすアニメーション

        yield return new WaitForSeconds(1f);
        Death();
       
        yield return StartCoroutine(FadeIn2());
       
    }
    //private IEnumerator DieSequence2()
    //{
        
    //}

    private IEnumerator FadeOut()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration); // 透明→黒
            fadeImage.color = c;
            yield return null;
        }
    }
    private IEnumerator FadeOut2()
    {
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < (fadeDuration *0.1f))
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / (fadeDuration * 0.1f)); // 透明→黒
            fadeImage.color = c;
            yield return null;
        }
    }
    private IEnumerator FadeIn()
    {

        Debug.Log("フェードイン中");
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            c.a = 1f - Mathf.Clamp01(elapsed / (fadeDuration *0.5f)); // 黒→透明
            fadeImage.color = c;
            yield return null;
        }
    }
    private IEnumerator FadeIn2()
    {

        Debug.Log("フェードイン中");
        float elapsed = 0f;
        Color c = fadeImage.color;

        while (elapsed < fadeDuration * 0.5f)
        {
            elapsed += Time.deltaTime;
            c.a = 1f - Mathf.Clamp01(elapsed / (fadeDuration *0.5f)); // 黒→透明
            fadeImage.color = c;
            yield return null;
            isCoroutine = false;

        }
    }
    private IEnumerator DarkenRoutine()
    {
        blackImage.SetActive(true);
        Death();
        yield return new WaitForSeconds(darkDuration);
        blackImage.SetActive(false);
    }

    public void Death()
    {

        _state = PlayerState.Idle;

        _playerManager.Init();

        //ここでチェックポイントによって座標を変える
        //Debug.Log($"{PlayerPrefs.GetInt("PointNum")}");
        switch (PlayerPrefs.GetInt("PointNum"))
        {
            case 0:
                this.transform.position = _checkPoint.StartPos;
                break;
            case 1:
                this.transform.position = _checkPoint.warpToFirstStage;
                break;
            case 2:
                this.transform.position = _checkPoint.warpToMapFirst;
                break;
            case 3:
                this.transform.position = _checkPoint.warpToSecondStage;
                break;
            case 4:
                this.transform.position = _checkPoint.warpToMapSecond;
                break;
            case 5:
                this.transform.position = _checkPoint.warpToThirdStage;
                break;
            case 6:
                this.transform.position = _checkPoint.warpToMapThird;
                break;
            default:
                break;
        }
    }

    private void ChangeSE(int index)
    {
        _audioSource.clip = _audioClips[index];
    }
}
