using Godot;
using System;

public class LightFollow : Node2D
{
    private Node2D player;

    public override void _Ready()
    {
        player = GetTree().GetNodesInGroup("Player")[0] as Node2D;

    }

    public override void _Process(float delta)
    {
        GlobalPosition = player.GlobalPosition;
        base._Process(delta);
    }
}
