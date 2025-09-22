using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class PlayerManager : MonoBehaviour
{
    Player _player;
    PlayerState _playerState;
    HoldManager _holdManager;//つかみ管理
    HammerController _hammer;
    HammerState _hammerState;
    NokonokoController _nokonoko;//あか
    NokonokoState _nokonokoState;
    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _holdManager = GameObject.Find("HoldManager").GetComponent<HoldManager>();
        //クローン
        //_hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();

        //Addressables.LoadAssetAsync<GameObject>("Hammer_Prefab").Completed=>{_hammer= handle.}
        
        
        //ノコノコクローンあか
        //_nokonoko = Resources.Load<GameObject>("Nokonoko").GetComponent<NokonokoController>();
    }

    // Update is called once per frame
    void Update()
    {
        _hammer = GameObject.Find("Hammer_Prefab").GetComponent<HammerController>();
        _nokonoko = GameObject.Find("Nokonoko").GetComponent<NokonokoController>();
        //_hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();
        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("J押してる");
        }
        //------------------
        _playerState = _player._state;//_playerStateの更新

        

        //Debug.Log($"PlayerStateは{_playerState}です");
        //ハンマー
        if (_holdManager.isColHit　&& _hammer.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
            {
                Debug.Log("持ちました");
                _player._state = PlayerState.Hold;
                _hammer.currentState = HammerState.held;
                _player._animator.SetBool("isHold", true);

            }
        }
        //ノコノコ
        if (_holdManager.isColHit && _nokonoko.isColHit)
        {
            if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
            {
                Debug.Log("持ちました");
                _player._state = PlayerState.Hold;
                _nokonoko.currentState = NokonokoState.held;
                _player._animator.SetBool("isHold", true);

            }
        }
        if (_player._state == PlayerState.Hold)
        {
            //ハンマー
            if (Input.GetKeyDown(KeyCode.K)&& _hammer.currentState == HammerState.held)//ものを落とすとき
            {
                _hammer.currentState = HammerState.pop;
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }
            
            if (Input.GetKeyDown(KeyCode.L) && _hammer.currentState == HammerState.held)//ものを投げるとき
            {
                _player._animator.SetTrigger("TriggerThrow");
                _hammer.currentState =  HammerState.thrown;
                _player._state = PlayerState.Idle;
                StartCoroutine(WaitAndRelease(0.5f)); // 1.2秒後にisHoldをfalseに
               
            }
            //のこのこ
            if (Input.GetKeyDown(KeyCode.K) && _nokonoko.currentState == NokonokoState.held)//ものを落とすとき
            {
                
                _nokonoko.currentState = NokonokoState.pop;
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
            }

            if (Input.GetKeyDown(KeyCode.L) && _nokonoko.currentState == NokonokoState.held)//ものを投げるとき
            {
                _player._animator.SetTrigger("TriggerThrow");
                
                _nokonoko.currentState = NokonokoState.thrown;
                _player._state = PlayerState.Idle;
                StartCoroutine(WaitAndRelease(0.5f)); // 1.2秒後にisHoldをfalseに

            }

        }

    }

    private IEnumerator WaitAndRelease(float delay)
    {
        yield return new WaitForSeconds(delay);
        _player._animator.SetBool("isHold", false);
    }
    private void FixedUpdate()
    {
       
    }
}
