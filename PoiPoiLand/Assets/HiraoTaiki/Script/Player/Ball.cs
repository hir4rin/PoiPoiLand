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
    public bool isColHit = false;

    Collider col;
    Rigidbody rb;
    Vector3 throwDir;//ìäÇ∞ÇÈå¸Ç´
   
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        col = this.GetComponent<Collider>();
        rb = this.GetComponent<Rigidbody>();
        throwDir = transform.forward + transform.up * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            isColHit = true;


        }
        
    }
    public void Hold()
    {
        this.transform.SetParent(_player.transform,false);
       this.transform.localPosition = new Vector3(0.5f, 1, 0.5f);
        col.enabled = false;
        rb.useGravity= false;
        rb.isKinematic = true;
        Debug.Log("ê¨å˜");
    }
    public void QuitHold()
    {
        this.transform.SetParent(null);
        col.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
        Debug.Log("é∏Ç¡ÇΩ");
       
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
