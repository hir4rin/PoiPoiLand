using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleFakeMove : MonoBehaviour
{
    //見た目だけ回転している甲羅
    [SerializeField] BowlingNokonokoController _turtle;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = Resources.Load<GameObject>("GravityTurtle").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (rb == null)
        {
            Debug.Log("Rigidbodyがアタッチされていません");
            return;
        }
     
        // ノコノコが投げられた状態またはボス状態のときに回転させる
        if (_turtle.currentState == BowlingNokonokoState.thrownzerogravity
        || _turtle.currentState == BowlingNokonokoState.throwngravity
        || _turtle.currentState == BowlingNokonokoState.Boss)
        {
           // Debug.Log("Rigidbodyがアタッチされています");
            // Rigidbodyの速度ベクトルの大きさを取得
            float speed = rb.velocity.magnitude;

            // Y軸回転のスピードを速度に応じて変化させる
            float spinSpeed = speed * 120f; // 30は倍率。調整可能

            // Y軸回転のみ
            transform.Rotate(Vector3.up, spinSpeed * Time.fixedDeltaTime, Space.Self);

            // 位置をRigidbodyに追従
            //transform.position = rb.position;
        }
    }
    public void Init(Rigidbody targetRb)
    {
        rb = targetRb;
    }

}
