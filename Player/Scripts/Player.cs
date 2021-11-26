using Godot;
using System;

public class Player : KinematicBody2D
{
    PlayerMovementComponent playerMove;
    private ProjectileWeapon gun;
    private PlayerGUI gui;
    private PlayerAnimation playerAnim;
    public ProjectileWeapon Gun { get { return gun; } set { gun = value; } }
    public AnimationPlayer animation;

    public int Health { get { return entity.Health; } set { entity.Health = value; } }

    [Export] public float FIRERATE = 0.1f;
    [Export] public int Velocity = 100;

    Position2D bulletPos;

    CommonEntity entity = new CommonEntity();
    ProgressBar healthbar;
    Label ammoLabel;

    Timer timer;

    AudioStreamPlayer hurtSound;
    AudioStreamPlayer2D gunshot;

    public float score = 0;

    Label scoreLabel;
    Label timeLabel;

    public float scoreMultiplier = 0;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var position = GetNode("Weapon/muzzle/bulletSpawn") as Position2D;
        bulletPos = position;

        healthbar = GetTree().GetNodesInGroup("HealthBar")[0] as ProgressBar;
        ammoLabel = GetTree().GetNodesInGroup("ammoLabel")[0] as Label;
        scoreLabel = GetTree().GetNodesInGroup("ScoreLabel")[0] as Label;
        timeLabel = GetTree().GetNodesInGroup("TimeLabel")[0] as Label;

        playerMove = new PlayerMovementComponent(Velocity, this);
        timer = GetNode("reloadTimer") as Timer;
        gun = new ProjectileWeapon(this, bulletPos, "Enemy", FIRERATE, 35, false, true, timer);
        gui = new PlayerGUI(healthbar, ammoLabel);
        playerAnim = new PlayerAnimation(GetNode("AnimatedSprite") as AnimatedSprite);
        animation = GetNode("AnimationPlayer") as AnimationPlayer;
        hurtSound = GetNode("hurt") as AudioStreamPlayer;
        gunshot = GetNode("gun") as AudioStreamPlayer2D;

        gun.gunSound = gunshot;
        Health = 100;

        SetProcess(false);

    }

    public void Hurt()
    {
        hurtSound.Play();
    }

    private void Death()
    {
        SetPhysicsProcess(false);

        healthbar.Value = 0f;
        var timer = GetNode("restartTimer") as Timer;
        timer.Start();

        SetProcess(false);
    }


    public void onReloadTimeout()
    {
        gun.reloadDone();
    }

    private void restart()
    {
        EntityManager.Restart();
        GetTree().ReloadCurrentScene();
    }

    public int seconds;
    public int minutes;

    public void Score()
    {
        seconds += 1;

        if (seconds == 60)
        {
            minutes += 1;
            seconds = 0;
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        gui.data = new PlayerGUI.guiData(entity.Health, gun.Bullets, gun.MagazineSize, gun.Reloading);
        gui.Update();
        gun.Update(delta);


        var scoreDisplay = (int)score;
        scoreLabel.Text = "Score: " + scoreDisplay.ToString();
        timeLabel.Text = "Time: " + minutes.ToString() + ":" + seconds.ToString(); 

        if (Input.IsActionPressed("Reload"))
            gun.Reload();
        if (Input.IsActionPressed("Fire"))
            gun.Run();


        if (entity.Health <= 0)
            Death();

        if (gun.Reloading)
            playerAnim.state = PlayerAnimation.STATE.RELOADING;
        else
            playerAnim.state = PlayerAnimation.STATE.IDLE;

        playerAnim.Update();

        LookAt(GetGlobalMousePosition());
        playerMove.Update();
    }
}
