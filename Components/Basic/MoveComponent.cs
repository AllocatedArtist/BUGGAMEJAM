using Godot;
using System;

using BugGameJam.Components.Basic;

public class MoveComponent : IComponent
{
    private Vector2 direction;
    private Vector2 movement;
    protected KinematicBody2D body;


    public Vector2 Direction { get { return direction; } set { direction = value; } }
    public Vector2 Movement { get { return movement; } set { movement = value; } }
    public int Velocity { get; set; }

    public MoveComponent(Vector2 direction, int velocity, KinematicBody2D body)
    {
        this.direction = direction;
        this.body = body;
        Velocity = velocity;
    }

    public void Run()
    {
        movement = direction * Velocity;
        movement = body.MoveAndSlide(movement);
    }
    

}
