using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovie : MonoBehaviour
{

    //移動スピード
    float speed = 0.06f;
    Animator animator;
    [SerializeField] movieManager _movieManager;

    bool prevChange = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            animator.SetBool("isWalk", true);
        }
        
        transform.position += new Vector3(0, 0, 1) * speed * speed;//前進
        if(transform.position.z > -3.7)
        {
            animator.SetBool("isWalk", false);
            _movieManager.animator.SetTrigger("isOpen");
            speed = 0;
        }
        if (_movieManager.isChange && !prevChange)
        {
            animator.SetTrigger("TriggerJump");
        }

        prevChange = _movieManager.isChange;
    }

}
