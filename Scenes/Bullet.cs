using Godot;
using System;

public class Bullet : Node2D
{

    [Export] public int speed = 1200;
    public int damage = 10;

    public Vector2 direction;
    public string target;



    public override void _Process(float delta)
    {
        this.Position += direction.Normalized() * speed * delta;

        base._Process(delta);
    }

    public void timeout()
    {
        QueueFree();
    }

    public void enemyDetected(Node2D body)
    {
        if (body.IsInGroup(target))
        {      
            if (body.IsInGroup("Enemy"))
            {
                var animationParent = (Enemy)body;
                var animation = animationParent.animation;
                animation.Play("hit");
                animationParent.Health -= damage;


                animationParent.hit.Play();
            }
            else if (body.IsInGroup("Player"))
            {
                var animationParent = (Player)body;
                var animation = animationParent.animation;
                animationParent.Hurt();

                animationParent.scoreMultiplier = 0;

                animation.Play("hit");


                animationParent.Health -= damage;
            }
        }
           

        QueueFree();
    }
}
