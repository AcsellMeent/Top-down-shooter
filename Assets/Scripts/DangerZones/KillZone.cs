using UnityEngine;

public class KillZone : DangertZone
{
    protected override void OnEnter(PlayerDangerZoneHandler dangerZoneHandler)
    {
        dangerZoneHandler.GetComponent<PlayerHealth>().Kill();   
    }
}
