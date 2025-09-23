using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackPos : MonoBehaviour
{

    [SerializeField] private GameObject magicballPrefab;//魔法弾のプレハブ
    [SerializeField] private Transform firePoint;//発射位置
 
    //プレイヤー(仮置き)
    public Transform Hero;//弾に渡す用



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
        ball.SetTarget(Hero);

    }
}
