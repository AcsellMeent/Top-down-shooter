using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerBonusHandler>(out PlayerBonusHandler bonusHandler))
        {
            ApplyBonus(bonusHandler);
        }
    }

    protected abstract void ApplyBonus(PlayerBonusHandler bonusHandler);
}
