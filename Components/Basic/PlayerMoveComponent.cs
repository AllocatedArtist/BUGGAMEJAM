using Godot;
using System;


class PlayerMovementComponent : MoveComponent
{

    public PlayerMovementComponent(int speed, KinematicBody2D body) : base(Vector2.Zero, speed, body)
    {}

    public void Update()
    {
        Vector2 input = new Vector2(Input.GetActionStrength("right") - Input.GetActionStrength("left"),
                                    Input.GetActionStrength("down") - Input.GetActionStrength("up")).Normalized();

        Direction = input;

        base.Run();
    }

}

