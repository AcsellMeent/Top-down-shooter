using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.ApplyDammage();
        }
    }
}
