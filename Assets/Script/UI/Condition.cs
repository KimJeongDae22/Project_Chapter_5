using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    [SerializeField] private float curValue;
    [SerializeField] private float startValue;
    [SerializeField] private float maxValue;
    [SerializeField] private float passiveValue;
    [SerializeField] private Image uiBar;
    void Start()
    {
        curValue = startValue;
    }

    // Update is called once per frame
    void Update()
    {
        uiBar.fillAmount = GetPercent();
    }
    private float GetPercent()
        { return curValue / maxValue; }
    public void AddValue(float value)
    {
        curValue = Mathf.Min(curValue + value, maxValue);
    }
    public void SubtractValue(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);
    }
    public float GetPassiveValue()
    {
        return passiveValue;
    }
    public float GetCurValue()
        { return curValue; }

}
