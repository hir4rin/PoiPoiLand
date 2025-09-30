using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage1StoneManager : MonoBehaviour
{
    public float stoneHealthPoint;
    [SerializeField] private Image hpGauge;
    [SerializeField] private Image burnHpGauge;
    [SerializeField] private GameObject stage1;
    private Stage1Manager stage1Manager;
    private Stage1State stageState;
    // Start is called before the first frame update
    void Start()
    {
        stoneHealthPoint = 3.0f;
        hpGauge.fillAmount = stoneHealthPoint / 3.0f;
        burnHpGauge.fillAmount = stoneHealthPoint / 3.0f;
        stage1Manager = stage1.GetComponent<Stage1Manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stageState = stage1Manager.State;
        Debug.Log(stoneHealthPoint);
        hpGauge.fillAmount = stoneHealthPoint / 3.0f;
        if (burnHpGauge.fillAmount > hpGauge.fillAmount)
        {
            burnHpGauge.fillAmount -= 0.01f;
        }
        if (burnHpGauge.fillAmount <= 0)
        {
            stage1Manager.State = Stage1State.Failed;
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            stoneHealthPoint -= 0.50f;
        }
    }
}
