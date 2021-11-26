using Godot;
using System;

public class ExitDoor : Area2D
{
    public void PlayerEnter(KinematicBody2D body)
    {
        if (body.IsInGroup("Player"))
        {
            var player = (Player)body;
            FinalData.Score = player.score;
            FinalData.Seconds = player.seconds;
            FinalData.Minute = player.minutes;

             GetTree().ChangeScene("res://Scenes/EndGame.tscn");
        }
    }
}
