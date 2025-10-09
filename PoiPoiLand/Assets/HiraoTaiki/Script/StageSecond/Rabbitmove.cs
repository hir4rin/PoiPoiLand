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

    float speed = 1.8f;

    public RabbitJenerator _RJ;
    //ââèoóp
    private Rigidbody _rb;
    bool isKnockedOver = false;

    // Start is called before the first frame update
    void Start()
    {
      _RJ = GameObject.Find("RabbitJenerator").GetComponent<RabbitJenerator>();
        _rb = GetComponent<Rigidbody>();
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
        //ÉVÉXÉeÉÄÇÃìsçáÇ…ÇÊÇËãtå¸Ç´
        transform.position += - Velocity * speed * Time.fixedDeltaTime;
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
            if (!isKnockedOver)
            {
                isKnockedOver = true;
                _rb.isKinematic = false;//ï®óùóLå¯âª
                _rb.useGravity = true;
                _rb.constraints = RigidbodyConstraints.None;//Freezeâèú

                //è’ìÀï˚å¸
                Vector3 dir = (transform.position - other.transform.position).normalized;

                _rb.AddForce(dir * 10f + Vector3.up * 25f,ForceMode.Impulse);

                //âÒì]
                _rb.AddTorque(Random.insideUnitSphere * 200f);

                //êîïbå„Ç…è¡Ç¶ÇÈ
                Destroy(gameObject, 3f);
            }
            

           // Destroy(gameObject);
            _RJ.rabbitCount++;
        }

    }
    
    public void AllDeath()
    {
        Destroy(gameObject);
    }
    
}
