using UnityEngine;

public abstract class DangertZone : MonoBehaviour
{
    [SerializeField]
    protected float Radius;
    //так как я именую protected с большой буквы как public поля, я переименовал свойство с Radius в GetRadius, не самый лучший  способ)
    public float GetRadius => Radius;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerDangerZoneHandler>(out PlayerDangerZoneHandler dangerZoneHandler))
        {
            OnEnter(dangerZoneHandler);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerDangerZoneHandler>(out PlayerDangerZoneHandler dangerZoneHandler))
        {
            OnExit(dangerZoneHandler);
        }
    }

    protected virtual void OnEnter(PlayerDangerZoneHandler dangerZoneHandler) { }
    protected virtual void OnExit(PlayerDangerZoneHandler dangerZoneHandler) { }
}
