using ExileCore2.Shared.Attributes;
using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using System.Drawing;

namespace PlayerCircle;

public class PlayerCircleSettings : ISettings
{
    public string[] PartyElements { get; set; } = new string[5];

    public TextNode TargetPlayerName { get; set; } = new TextNode("Leader");

    [Menu(null, "Circle around yourself (it will override the target player)")]
    public ToggleNode CircleSelf { get; set; } = new ToggleNode(true);

    public ToggleNode Enable { get; set; } = new ToggleNode(false);

    [Menu(null, "Enable/Disable circle around player")]
    public ToggleNode ShowCircle { get; set; } = new ToggleNode(true);

    [Menu(null, "Circle radius around player")]
    public RangeNode<int> CircleRadius { get; set; } = new RangeNode<int>(1275, 0, 5000);

    [Menu(null, "Number of circle segments (higher = smoother)")]
    public RangeNode<int> CircleThickness { get; set; } = new RangeNode<int>(1, 2, 20);

    public CircleRender Render { get; set; } = new CircleRender();
}

[Submenu]
public class CircleRender
{
    [Menu(null, "Show circle in town areas")]
    public ToggleNode ShowInTown { get; set; } = new ToggleNode(false);

    [Menu(null, "Show circle in hideout")]
    public ToggleNode ShowInHideout { get; set; } = new ToggleNode(true);

    public ColorNode CircleColor { get; set; } = Color.FromArgb(127, 255, 0, 0);
}