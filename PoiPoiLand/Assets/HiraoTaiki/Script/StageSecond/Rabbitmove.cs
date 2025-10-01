using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbitmove : MonoBehaviour
{

    Vector3 right = new Vector3(1, 0, 0);
    Vector3 left = new Vector3(-1, 0, 0);
    [SerializeField] bool isRightMove = true;
    [SerializeField] bool isColHit = false;
    Vector3 Velocity = Vector3.zero;

    float speed = 0.5f;

    public RabbitJenerator _RJ;

    // Start is called before the first frame update
    void Start()
    {
      _RJ = GameObject.Find("RabbitJenerator").GetComponent<RabbitJenerator>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    private void FixedUpdate()
    {
        if (isRightMove)
        {
            Velocity = right;
           
        }
        else
        {
            Velocity = left;
         
        }
        transform.position += Velocity * speed * Time.fixedDeltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("êGÇÍÇΩ");
        if (other.CompareTag("Wall"))
        {
            isColHit = true;
            if(isRightMove)
            {
                isRightMove = false;
            }
            else if (!isRightMove)
            {
                isRightMove = true;
            }

        }
        if (other.CompareTag("Bowling"))
        {
            Destroy(gameObject);
            _RJ.rabbitCount++;
        }

    }
    public void AllDeath()
    {
        Destroy(gameObject);
    }
    
}
