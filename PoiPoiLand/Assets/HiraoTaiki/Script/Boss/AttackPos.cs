using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackPos : MonoBehaviour
{

    [SerializeField] private GameObject magicballPrefab;//魔法弾のプレハブ
    [SerializeField] private Transform firePoint;//発射位置
    [SerializeField] private GameObject _gravityTurtle;//亀のプレハブ

    //プレイヤー
    public Transform _player;//弾に渡す用
    public Transform boss;//亀に渡す用

    [SerializeField] GameObject shotTurtleEffect;   //亀を放ったときのeffect
    [SerializeField] GameObject shotMagicEffect;    //弾を放った時のeffect

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
     

    }
    public void MagicAttack()
    {
        GameObject ballobj = Instantiate(magicballPrefab, firePoint.position, firePoint.rotation);
        magicball ball = ballobj.GetComponent<magicball>();
        ball.SetTarget(_player);

        //エフェクトを生成
        GameObject effect = Instantiate(shotMagicEffect, firePoint.position, firePoint.rotation);
    }
    public void TurtleAttack()
    {
        GameObject turtleobj = Instantiate(_gravityTurtle, firePoint.position, firePoint.rotation);
        BowlingNokonokoController turtle = turtleobj.GetComponent<BowlingNokonokoController>();
        turtle.currentState = BowlingNokonokoState.Boss;
        turtle.SetTarget(boss);

        //回転用
        Rigidbody turtleRb = turtleobj.GetComponent<Rigidbody>();
        TurtleFakeMove _fake = turtleobj.GetComponentInChildren<TurtleFakeMove>();
        _fake.Init(turtleRb);

        //エフェクトを生成
        GameObject effect = Instantiate(shotTurtleEffect, firePoint.position, Quaternion.identity);
    }
}
