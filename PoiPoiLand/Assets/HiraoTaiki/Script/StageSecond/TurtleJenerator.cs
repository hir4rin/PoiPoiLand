using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleJenerator : MonoBehaviour
{
    [SerializeField] GameObject _graTurtlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        turtleSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void turtleSpawn()
    {
        Vector3 pos = new Vector3(151.4f, 32.8f, -3.2f);

        GameObject obj = Instantiate(_graTurtlePrefab, pos, Quaternion.identity);
        BowlingNokonokoController _skr = obj.GetComponent<BowlingNokonokoController>();
        _skr.currentState = BowlingNokonokoState.pop; 
    }
}
