using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BallState
{
    Ball
}



public class Ball : MonoBehaviour
{

    GameObject _player;
    Player _playerScript;
    public bool isColHit = false;

    Collider col;
    Rigidbody rb;
    Vector3 throwDir;//投げる向き

    Transform playerTransform;//プレイヤーのTransform

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _playerScript = _player.GetComponent<Player>();
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform = transform.root;//親オブジェクト(player)のTransformを取得
       
        throwDir = playerTransform.forward + transform.up * 0.5f;//投げる向きはプレイヤーの向き＋少し上
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            isColHit = true;


        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ground"))
        {
            isColHit = false;


        }
        
    }
    public void Hold()
    {
        this.transform.SetParent(_player.transform,false);
       this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        col.enabled = false;
        rb.useGravity= false;
        rb.isKinematic = true;
        Debug.Log("成功");
    }
    public void QuitHold()
    {
        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        Debug.Log("失った");
       
    }
    public void Throw()
    {
        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.AddForce(throwDir.normalized * 10f, ForceMode.Impulse);

    }
}
