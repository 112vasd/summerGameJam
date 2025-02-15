using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
public class Fill_Loding : MonoBehaviour
{

    [SerializeField] GameObject baby;
    public float startPos;
    public float endPos;

    float time;
    float startTime;
    float loding_speed;     //�ε� �Ұ�
    bool loding_Term;       //�ε� �� T/F
    float loding_Random;    //�ε� ������
    float loding_Temp;      //�ε� ��Ȳ

    public int loding_Smin;
    public int loding_Smax;

    public int loding_Tmin;
    public int loding_Tmax;

    public float dil;

    private void Awake()
    {
        time = 0.0f;
        loding_Random = 0.0f;
        this.GetComponent<Image>().fillAmount = 0.0f;
        loding_speed = Random.Range(loding_Tmin, loding_Tmax) * 0.1f;
    }
    private void Update()
    {
        if (this.GetComponent<Image>().fillAmount > 7.4f)
        {
            startTime = Time.time;
        }
        time += Time.deltaTime;
        if (time > loding_speed)
        {
            loding_speed = Random.Range(loding_Tmin, loding_Tmax) * 0.1f;
            loding_Term = true;
        }
        if (loding_Term)
        {
            time = 0.0f;
            loding_Random = Random.Range(loding_Smin, loding_Smax) * 0.01f;
            Debug.Log(loding_Random);
            loding_Temp = this.GetComponent<Image>().fillAmount;
            loding_Term = false;
            startTime = Time.time;
        }
        if (!loding_Term)
        {
            float t = (Time.time - startTime) / dil;
            this.GetComponent<Image>().fillAmount = Mathf.SmoothStep(loding_Temp, loding_Temp + loding_Random, t);
            baby.transform.position = new Vector2(Mathf.Lerp(startPos, endPos, this.GetComponent<Image>().fillAmount), 0.12f);
        }
    }
}