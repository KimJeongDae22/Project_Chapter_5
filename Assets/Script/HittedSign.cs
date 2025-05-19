using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HittedSign : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float flashSpeed;
    private Coroutine coroutine;
    void Start()
    {
        CharacterManager.Instance.Player.GetCondition().OnTakeDamage += Flash;
    }
    public void Flash()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        image.enabled = true;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
        coroutine = StartCoroutine(FadeAway());
    }
    private IEnumerator FadeAway()
    {
        float startAlpha = 0.5f;
        float a = startAlpha;

        while (a > 0)
        {
            a -= (startAlpha / flashSpeed) * Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return null;
        }

        image.enabled = false;
    }
}
