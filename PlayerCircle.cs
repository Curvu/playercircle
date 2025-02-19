using System;
using System.Linq;
using ExileCore2;
using ExileCore2.PoEMemory.Components;
using ExileCore2.PoEMemory.MemoryObjects;

namespace PlayerCircle;

public class PlayerCircle : BaseSettingsPlugin<PlayerCircleSettings>
{
    public override void Render()
    {
        if (!Settings.Enable || !Settings.ShowCircle) return;

        if (!Settings.Render.ShowInTown && GameController.Area.CurrentArea.IsTown) return;
        if (!Settings.Render.ShowInHideout && GameController.Area.CurrentArea.IsHideout) return;

        var player = GetFollowingTarget();
        if (player == null) return;

        var playerPos = player.Pos;
        float radius = Settings.CircleRadius; // Circle radius in world units

        Graphics.DrawCircleInWorld(
            playerPos, 
            radius, 
            Settings.Render.CircleColor, 
            Settings.CircleThickness, 
            50 // Number of segments for smoothness
        );
    }

    private Entity GetFollowingTarget()
    {
        try
        {
            var leaderName = Settings.TargetPlayerName.Value.ToLower();
            return GameController.Entities
                .Where(e => e.Type == ExileCore2.Shared.Enums.EntityType.Player)
                .FirstOrDefault(e => e.GetComponent<Player>().PlayerName.ToLower() == leaderName);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
