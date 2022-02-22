using UnityEngine;

public class DeathHandler : MonoBehaviour
{

    private void OnEnable()
    {
        GetComponent<Health>().OnDied += HandleDeath;
    }

    private void HandleDeath()
    {
        if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("HomeBase"))
        {
            Debug.Log("<<<<<<<< GAME OVER >>>>>>>>");
            GameManager.instance.StartGameOver();
        }
    }

    private void OnDisable()
    {
        GetComponent<Health>().OnDied -= HandleDeath;
    }
}