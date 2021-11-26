using Godot;
using System;

public class Dummy : Node2D
{
    public CommonEntity entity;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        entity = new CommonEntity();
        entity.Health = 100;
        entity.tag = Name;
        GD.Print(Name);

        EntityManager.AddEntity(Name, entity);
    }

    public void decrease_health(int damage)
    {
        entity.Health -= damage;
    }

    public override void _Process(float delta)
    {
        if (entity.Health >= 75)
            this.Modulate = new Color(0, 255, 0);
        else if (entity.Health >= 50 && entity.Health < 75)
            this.Modulate = new Color(255, 145, 0);
        else
            this.Modulate = new Color(255, 0, 0);

        if (entity.Health <= 0)
            QueueFree();


        base._Process(delta);
    }

}
