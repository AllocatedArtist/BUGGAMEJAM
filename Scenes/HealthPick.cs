using Godot;
using System;

public class HealthPick : Area2D
{
    
    public void PlayerEnter(KinematicBody2D body)
    {
        if (body.IsInGroup("Player"))
        {
            var player = (Player)body;

            if (player.Health < 100)
            {
                player.Health = 100;
                QueueFree();
            }
        }
    }


}
