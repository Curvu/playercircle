using ExileCore2;
using ExileCore2.Shared.Nodes;
using System;

namespace PlayerCircle;

public class PlayerCircle : BaseSettingsPlugin<PlayerCircleSettings>
{
    public override void Render()
    {
        if (!Settings.Enable || !Settings.ShowCircle) return;

        if (!Settings.Render.ShowInTown && GameController.Area.CurrentArea.IsTown) return;
        if (!Settings.Render.ShowInHideout && GameController.Area.CurrentArea.IsHideout) return;

        var playerPos = GameController.Player.Pos; // Get the player's world position
        float radius = Settings.CircleRadius; // Circle radius in world units

        Graphics.DrawCircleInWorld(
            playerPos, 
            radius, 
            Settings.Render.CircleColor, 
            Settings.CircleThickness, 
            50 // Number of segments for smoothness
        );
    }
}
