using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private void OnEnable()
    {
        GetComponent<Health>().OnHealthChanged += HealthChanged;
    }
    private void OnDisable()
    {
        GetComponent<Health>().OnHealthChanged -= HealthChanged;
    }

    private void HealthChanged(float newFillAmount)
    {
        StartCoroutine(LerpHealthbar(newFillAmount));
    }

    private IEnumerator LerpHealthbar(float newFillAmount)
    {
        float elapsedTime = 0;
        float duration = 1;
        while (elapsedTime < duration)
        {
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, newFillAmount, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
        healthBarImage.fillAmount = newFillAmount;
    }
}