using System.Collections.Generic;
using System;
using Godot;

class EnemyMoveComponent : MoveComponent
{
    List<Vector2> path = new List<Vector2>();
    Navigation2D levelNavigation;
    private Vector2 destination;
    Vector2 dir;
    Line2D line;

    public Vector2 Destination { get { return destination; } set { destination = value; } }

    public EnemyMoveComponent(Navigation2D level, KinematicBody2D body, Vector2 destination, int velocity) : base(Vector2.Zero, velocity, body)
    {
        line = body.GetNode("line") as Line2D;
        levelNavigation = level;
        this.destination = destination;
    }

    public void Navigate()
    {
        if (path.Count > 0)
        {
            try
            {
                dir = body.GlobalPosition.DirectionTo(path[1]);

                if (body.GlobalPosition == path[0])
                    path.RemoveAt(0);
            }
            catch (Exception exception)
            {
                GD.Print(exception.Message);
            }
        }
    }

    public void generatePath()
    {
        if (body != null)
        {
            path.Clear();
            Vector2[] simplePath = levelNavigation.GetSimplePath(body.GlobalPosition, destination, false);

            foreach (var position in simplePath)
            {
                path.Add(position);
            }
        }
    }

    public void Update()
    {
        if (destination != null)
        {
            path.Clear();
            line.GlobalPosition = Vector2.Zero;

            Vector2 velocity = dir * Velocity;

            velocity = body.MoveAndSlide(velocity);
        }
    }
}