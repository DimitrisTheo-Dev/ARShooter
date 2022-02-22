using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletDamage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HomeBase"))
        {
            other.GetComponent<Health>().ApplyDamage(bulletDamage);
            Destroy(gameObject);
        }
    }

}