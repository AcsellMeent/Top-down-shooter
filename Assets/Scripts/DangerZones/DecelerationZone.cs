using UnityEngine;

public class DecelerationZone : DangertZone
{
    protected override void OnEnter(PlayerDangerZoneHandler dangerZoneHandler)
    {
        dangerZoneHandler.GetComponent<PlayerMovement>().AddSpeedMultiplier(0.6f);
    }

    protected override void OnExit(PlayerDangerZoneHandler dangerZoneHandler)
    {
        dangerZoneHandler.GetComponent<PlayerMovement>().RemoveSpeedMultiplier(0.6f);
    }
}
