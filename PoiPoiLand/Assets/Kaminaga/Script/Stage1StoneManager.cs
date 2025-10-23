using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Stage1StoneManager : MonoBehaviour
{
    public float stoneHealthPoint;
    [SerializeField] private Image hpGaugeBack;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Image burnHpGauge;
    [SerializeField] private GameObject stage1;
    [SerializeField] private GameObject warpEffect;
    private GameObject warpEffectInstance;
    private GameObject burnEffectInstance;
    private Stage1Manager stage1Manager;
    private Stage1State stage1State;
    private bool isReset;
    private bool isFailed;
    private bool isMoving;
    private float moveTimer;
    private Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        stoneHealthPoint = 3.0f;
        hpGauge.fillAmount = stoneHealthPoint / 3.0f;
        burnHpGauge.fillAmount = stoneHealthPoint / 3.0f;
        stage1Manager = stage1.GetComponent<Stage1Manager>();
        isReset = false;
        isFailed = false;
        isMoving = false;
        moveTimer = 0.0f;
        initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            moveTimer += Time.fixedDeltaTime;
            this.transform.position += new Vector3(Mathf.Sin(moveTimer * 50.0f) * 0.1f, 0.0f, 0.0f);
            if (moveTimer >= 0.2f)
            {
                if(burnEffectInstance != null)
                {
                    Destroy(burnEffectInstance);
                }
                this.transform.position = initialPosition;
                isMoving = false;
                moveTimer = 0.0f;
            }
        }
        stage1State = stage1Manager.State;
        hpGauge.fillAmount = stoneHealthPoint / 3.0f;
        if (burnHpGauge.fillAmount > hpGauge.fillAmount)
        {
            burnHpGauge.fillAmount -= 0.01f;
        }
        if (burnHpGauge.fillAmount <= 0)
        {
            if (!isFailed)
            {
                stage1Manager.State = Stage1State.Failed;
                isFailed = true;
            }
        }
        hpGaugeBack.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position + new Vector3(0.0f, 2.0f, 0.0f));

        if (stage1State == Stage1State.Start)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if (isReset)
            {
                stoneHealthPoint = 3.0f;
                hpGauge.fillAmount = stoneHealthPoint / 3.0f;
                burnHpGauge.fillAmount = stoneHealthPoint / 3.0f;
                isReset = false;
                isFailed = false;
            }
        }

        if (stage1State == Stage1State.Failed)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            isReset = true;
        }
        if (stage1State == Stage1State.Cleared)
        {
            hpGaugeBack.enabled = false;
            hpGauge.enabled = false;
            burnHpGauge.enabled = false;
            if (warpEffectInstance == null)
            {
                warpEffectInstance = Instantiate(warpEffect, this.transform.position, Quaternion.identity);
            }
            // ワープポータルの回転(いらないかもしれない)
            //this.transform.rotation *= Quaternion.AngleAxis(1.0f, new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if(burnEffectInstance == null)
            {
                burnEffectInstance = Instantiate(warpEffect, this.transform.position, Quaternion.identity); // ここを燃焼エフェクトに変更
            }
            stoneHealthPoint -= 0.25f;
            isMoving = true;
        }
        if (other.gameObject.tag == "Player" && stage1State == Stage1State.Failed)
        {
            stage1Manager.RestartStage();
        }
    }
    
}
