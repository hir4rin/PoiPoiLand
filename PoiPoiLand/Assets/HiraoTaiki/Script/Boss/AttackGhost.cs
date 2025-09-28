using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGhost : MonoBehaviour
{

    [SerializeField] private GameObject RedGhost;//ghostのプレハブ
    public Transform _player;//色違いゴーストに渡す用
    public Transform boss;//色違いゴーストに渡す用
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GhostAttack()
    {
        GameObject ghostobj = Instantiate(RedGhost, transform.position, transform.rotation);
        RedGostMove ghost = ghostobj.GetComponent<RedGostMove>();
        //playerとbossをsetする
         ghost.SetTarget(_player);
        ghost.SetBoss(boss);
    }
}
