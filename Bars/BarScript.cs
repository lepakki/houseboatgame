using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    private float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image Filler;

    [SerializeField]
    private Text valueText;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            string[] tmp = valueText.text.Split(':');
            valueText.text = tmp[0] + ": " + Mathf.FloorToInt(value);
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

	// Use this for initialization
	void Start () {
        //fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        HandleBar();
	}

    void HandleBar()
    {
        if (fillAmount != Filler.fillAmount)
        {
            Filler.fillAmount = Mathf.Lerp(Filler.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    private float Map(float value, float inMin, float inMax,float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
