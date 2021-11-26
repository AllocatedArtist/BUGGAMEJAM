using Godot;
using System;

public class Spawner : Position2D
{
    PackedScene enemyInstance;

    Timer spawnTimer;

    [Export] public int MAX_ENEMY_COUNT = 20;
    [Export] public bool PATROL = true;
    private Vector2 playerPos;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemyInstance = ResourceLoader.Load("Enemy/Enemy.tscn") as PackedScene;
        spawnTimer = GetNode("Spawn") as Timer;
        spawnTimer.WaitTime = 1f;

        playerPos = (GetTree().GetNodesInGroup("Player")[0] as Node2D).GlobalPosition;
        Spawn();
    }

    public void ChangeTimerSecond()
    {
        var rng = new RandomNumberGenerator();
        rng.Randomize();

        var seconds = rng.RandfRange(2f, 4f);

        spawnTimer.WaitTime = seconds;
    }

    private void Spawn()
    {
            var enemy = enemyInstance.Instance() as Enemy;
            var rng = new RandomNumberGenerator();

            rng.Randomize();

            float x = rng.RandfRange(-10, 10);
            float y = rng.RandfRange(-10, 10);

            Vector2 offset = new Vector2(x, y);
            
            enemy.GlobalPosition = this.GlobalPosition + offset;
            enemy.GlobalRotation = Mathf.Deg2Rad(rng.RandfRange(0, 360));

            enemy.patrol = PATROL;
            GetParent().CallDeferred("add_child", enemy);
    }

    private int enemyNumber = 0;

    public void SpawnTimer()
    {
        if (enemyNumber < MAX_ENEMY_COUNT)
        {
            enemyNumber += 1;
            Spawn();
        }
    }

}
