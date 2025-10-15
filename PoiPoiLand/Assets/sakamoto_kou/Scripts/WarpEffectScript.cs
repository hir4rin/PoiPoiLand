using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpEffectScript : MonoBehaviour
{
    [SerializeField] GameObject Player;
    //ワープ時のエフェクト
    [SerializeField] GameObject Effect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject effect = Instantiate(Effect, this.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
