using Godot;
using System;

public class EndGame : Node2D
{

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var score = GetNode("Score") as Label;
        var time = GetNode("Time") as Label;

        score.Text = "Score: " + (int)FinalData.Score;
        time.Text = "Final Time: " + FinalData.Minute + " Minutes and " + FinalData.Seconds + " Seconds!";
    }


}
