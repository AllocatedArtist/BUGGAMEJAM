using Godot;
using System;

public class Enemy : KinematicBody2D
{
    EnemyMoveComponent move;
    CommonEntity entity = new CommonEntity();

    RayCast2D sight;

    Player player;

    RayCast2D fireSight;

    [Export] public float fireRate = 1f;
    [Export] public int velocity = 100;

    ProjectileWeapon weapon;
    public AnimationPlayer animation;
    AudioStreamPlayer2D gunshot;
    public AudioStreamPlayer2D hit;

    public int Health = 100;

    public override void _Ready()
    {

        var level = GetParent().GetNode("LevelNavigation") as Navigation2D;
        var tree = GetTree();
        var players = tree.GetNodesInGroup("Player");
        var player = players[0] as Player;

        this.player = player;

        sight = GetNode("sight") as RayCast2D;
        fireSight = GetNode("firesight") as RayCast2D;

        var position2d = GetNode("bulletSpawn") as Position2D;

        weapon = new ProjectileWeapon(this, position2d, "Player", fireRate, 5, ProjectileWeapon.TYPE.BIG);
        move = new EnemyMoveComponent(level, this, Vector2.Zero, velocity);
        animation = GetNode("enemyAnimation") as AnimationPlayer;

        gunshot = GetNode("gun") as AudioStreamPlayer2D;
        weapon.gunSound = gunshot;
        hit = GetNode("hurt") as AudioStreamPlayer2D;

        var timer = GetNode("update") as Timer;
        timer.Start();

    }

    public bool patrol = true;

    public void UpdatePath()
    {
        if (player != null)
             move.generatePath();
    }

    public void CheckDistance()
    {
        if (GlobalPosition.DistanceSquaredTo(player.GlobalPosition) < 30000)
        {
            var timer = GetNode("checkDistance") as Timer;
            timer.Stop();
            patrol = false;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        if (!patrol)
        {
            move.Destination = player.GlobalPosition;

            if (player != null)
            {
                move.Navigate();
                move.Update();
            }

            LookAt((player.GetNode("BODY") as Node2D).GlobalPosition);

            if (fireSight.IsColliding())
            {
                var hit = fireSight.GetCollider() as Node2D;

                if (hit.IsInGroup("Player"))
                {
                    var player = (Player)hit;
                    if (player.Health <= 0)
                        SetPhysicsProcess(false);

                    weapon.Run();
                }
            }

            weapon.Update(delta);

            if (Health <= 0)
            {
                if (player.Gun.Bullets <= 5)
                     player.Gun.Bullets = 10;

                player.scoreMultiplier += 0.1f;

                var multiply = player.score * player.scoreMultiplier;
                player.score += (multiply + 10);
                QueueFree();
            }

        }

        base._PhysicsProcess(delta);

    }
}
