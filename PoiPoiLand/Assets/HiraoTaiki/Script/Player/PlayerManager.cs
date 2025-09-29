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
    HammerController _hammer;//ハンマー
    HammerState _hammerState;
    NokonokoController _nokonoko;//のこのこ
    NokonokoState _nokonokoState;
    BowlingNokonokoController _bowling;//のこのこボーリング
    BowlingNokonokoState _bowlingState;

    bool isHaving = false;

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

        //_hammer = GameObject.Find("Hammer_Prefab").GetComponent<HammerController>();
        _hammer = Resources.Load<GameObject>("Hammer_Prefab").GetComponent<HammerController>();
        _nokonoko = GameObject.Find("Nokonoko").GetComponent<NokonokoController>();
        _bowling = GameObject.Find("BowlingNokonoko").GetComponent<BowlingNokonokoController>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerState = _player._state;//_playerStateの更新

        Debug.Log($"PlayerStateは{_playerState}です");
        //ハンマー
        if (_holdManager.isColHit　&& _hammer.isColHit)
        {
            if(!isHaving)
            {
                if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
                {
                    Debug.Log("持ちました");
                    _player._state = PlayerState.Hold;
                    _hammer.currentState = HammerState.held;
                    _player._animator.SetBool("isHold", true);
                    isHaving = true;
                }
            }
        }
        //ノコノコ
        if (_holdManager.isColHit && _nokonoko.isColHit)
        {
            if (!isHaving)
            {
                if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
                {
                    Debug.Log("持ちました");
                    _player._state = PlayerState.Hold;
                    _nokonoko.currentState = NokonokoState.held;
                    _player._animator.SetBool("isHold", true);
                    isHaving = true;
                }
            }
        }
        //ボーリング
        if (_holdManager.isColHit && _bowling.isColHit)
        {

            if (!isHaving)
            {
                if (Input.GetKeyDown(KeyCode.J))//現在、結構ラグがある感じ
                {
                    Debug.Log("持ちました");
                    _player._state = PlayerState.Hold;
                    _bowling.currentState = BowlingNokonokoState.held;
                    _player._animator.SetBool("isHold", true);
                    isHaving = true;
                }
            }   
        }
        if (_player._state == PlayerState.Hold)
        {
            //ハンマー
            if (Input.GetKeyDown(KeyCode.K) && _hammer.currentState == HammerState.held)//ものを落とすとき
            {
                _hammer.QuitHold();
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
                isHaving = false;
            }
            
            if (Input.GetKeyDown(KeyCode.L) && _hammer.currentState == HammerState.held)//ものを投げるとき
            {
                _player._animator.SetTrigger("TriggerThrow");
                _hammer.currentState =  HammerState.thrown;
                _player._state = PlayerState.Idle;
                StartCoroutine(WaitAndRelease(0.5f)); // 1.2秒後にisHoldをfalseに
                isHaving = false;
            }
            //のこのこ
            if (Input.GetKeyDown(KeyCode.K) && _nokonoko.currentState == NokonokoState.held)//ものを落とすとき
            {
                _nokonoko.currentState = NokonokoState.pop;
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
                isHaving = false;
            }

            if (Input.GetKeyDown(KeyCode.L) && _nokonoko.currentState == NokonokoState.held)//ものを投げるとき
            {
                _player._animator.SetTrigger("TriggerThrow");
                
                _nokonoko.currentState = NokonokoState.thrownzerogravity;
                _player._state = PlayerState.Idle;
                StartCoroutine(WaitAndRelease(0.5f)); // 1.2秒後にisHoldをfalseに
                isHaving = false;
            }

            //ボーリング
            if (Input.GetKeyDown(KeyCode.K) && _bowling.currentState == BowlingNokonokoState.held)//ものを落とすとき
            {

                _bowling.currentState = BowlingNokonokoState.pop;
                _player._state = PlayerState.Idle;
                _player._animator.SetBool("isHold", false);
                isHaving = false;
            }

            if (Input.GetKeyDown(KeyCode.L) && _bowling.currentState == BowlingNokonokoState.held)//ものを投げるとき
            {
                _player._animator.SetTrigger("TriggerThrow");

                _bowling.currentState = BowlingNokonokoState.thrownzerogravity;
                _player._state = PlayerState.Idle;
                StartCoroutine(WaitAndRelease(0.5f)); // 1.2秒後にisHoldをfalseに//Playerの処理
                isHaving = false;
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

    public void SetHammer(HammerController hammer)
    {
        _hammer = hammer;
    }

    public void SetNokonoko(NokonokoController nokonoko)
    {
        _nokonoko = nokonoko;
    }

    public void SetBowling(BowlingNokonokoController bowling)
    {
        _bowling = bowling;
    }
}
