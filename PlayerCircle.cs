using System;
using System.Linq;
using ImGuiNET;

using ExileCore2;
using ExileCore2.PoEMemory.Components;
using ExileCore2.PoEMemory.MemoryObjects;
using ExileCore2.Shared.Enums;

namespace PlayerCircle;

public class PlayerCircle : BaseSettingsPlugin<PlayerCircleSettings>
{
    public override void DrawSettings()
    {
        try
        {
            if (ImGui.Button("Get Party List")) GetPartyList();

            ImGui.SameLine();
            ImGui.TextDisabled("(?)");
            if (ImGui.IsItemHovered()) ImGui.SetTooltip("Get the list of party members");

            // draw the party list
            ImGui.Text("Party List:");
            var i = 0;
            foreach (var playerName in Settings.PartyElements)
            {
                if (string.IsNullOrEmpty(playerName)) continue;
                i++;
                if (ImGui.Button("Set " + playerName + " as target"))
                    Settings.TargetPlayerName.Value = playerName;
            }
            if (i == 0) ImGui.Text("No party members found");
        }
        catch (Exception) { /* Handle exceptions silently */ }
        base.DrawSettings();
    }

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
            if (Settings.CircleSelf) return GameController.Player;
            var leaderName = Settings.TargetPlayerName.Value.ToLower();
            return GameController.EntityListWrapper.ValidEntitiesByType[EntityType.Player].FirstOrDefault(x => string.Equals(x.GetComponent<Player>()?.PlayerName.ToLower(), leaderName, StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception)
        {
            return null;
        }
    }

    private void GetPartyList()
    {
        var partyElements = new string[5];
        try
        {
            var partyElementList = GameController?.IngameState?.IngameUi?.PartyElement.Children?[0]?.Children;
            var i = 0;
            foreach (var partyElement in partyElementList)
            {
                var playerName = partyElement?.Children?[0]?.Children?[0]?.Text;
                partyElements[i] = playerName;
                i++;
            }
            Settings.PartyElements = partyElements;
        }
        catch (Exception) { /* Handle exceptions silently */ }
    }
}
