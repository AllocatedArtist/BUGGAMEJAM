using Godot;
using System;

public class AmmoPickup : Area2D
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void PlayerEnter(KinematicBody2D body)
    {
        if (body.IsInGroup("Player"))
        {
            var player = (Player)body;
            
            if (player.Gun.MagazineSize < 100)
            {
                player.Gun.MagazineSize = 100;
                
                if (player.Gun.Bullets < 20)
                {
                    player.Gun.Bullets = 20;
                }
            }

            QueueFree();
        }
    }
}
