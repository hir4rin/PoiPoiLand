using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1StoneManager : MonoBehaviour
{
    public float stoneHealthPoint;
    [SerializeField] private Image hpGaugeBack;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Image burnHpGauge;
    [SerializeField] private GameObject stage1;
    private Stage1Manager stage1Manager;
    private Stage1State stage1State;
    private bool isReset;
    private bool isFailed;
    // Start is called before the first frame update
    void Start()
    {
        stoneHealthPoint = 3.0f;
        hpGauge.fillAmount = stoneHealthPoint / 3.0f;
        burnHpGauge.fillAmount = stoneHealthPoint / 3.0f;
        stage1Manager = stage1.GetComponent<Stage1Manager>();
        isReset = false;
        isFailed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                stage1Manager.State = Stage1State.Failed; // こいつのせいでステートがおかしい
                isFailed = true;
            }
        }
        hpGaugeBack.rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, this.transform.position + new Vector3(0.0f, 2.0f, 0.0f));

        if (stage1State == Stage1State.Start)
        {
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
            isReset = true;
        }
        if (stage1State == Stage1State.Cleared)
        {
            hpGaugeBack.enabled = false;
            hpGauge.enabled = false;
            burnHpGauge.enabled = false;
            this.transform.rotation *= Quaternion.AngleAxis(1.0f, new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            stoneHealthPoint -= 0.25f;
        }
    }
}
