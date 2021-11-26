using System;
using Godot;

public class ProjectileWeapon 
{
    private bool canFire = true;
    private bool isInfinite = true;

    private int bullets = 20;
    private int magazineSize = 100;

    public float fireRate = 1f;
    public string Target { get; set; }

    public int Bullets { get { return bullets; } set { bullets = value; } }
    public int MagazineSize { get { return magazineSize; } set { magazineSize = value; } }

    public bool Reloading { get { return isReloading; } }

    public float reloadTime = 1f;

    Bullet bullet;
    Position2D bulletPos;
    Node2D node;

    private int damage;
    private bool playerControl = false;

    private Timer reloadTimer = new Timer();
    public AudioStreamPlayer2D gunSound { get; set; }

    public ProjectileWeapon(Node2D node, Position2D bulletPos, string target, float fireRate, int damage)
    {
        this.fireRate = fireRate;
        Target = target;

        this.node = node;
        this.bulletPos = bulletPos;
        this.damage = damage;
    }

    public enum TYPE
    {
        BIG,
        NORMAL
    }

    private TYPE type = TYPE.NORMAL;

    public ProjectileWeapon(Node2D node, Position2D bulletPos, string target, float fireRate, int damage, TYPE type)
    {
        this.fireRate = fireRate;
        Target = target;

        this.node = node;
        this.bulletPos = bulletPos;
        this.damage = damage;
        this.type = type;
    }


    public ProjectileWeapon(Node2D node, Position2D bulletPos, string target, float fireRate, int damage, bool isInfinite, bool isPlayer, Timer reloadTimer)
    {
        this.isInfinite = isInfinite;
        this.fireRate = fireRate;
        Target = target;

        playerControl = isPlayer;

        this.node = node;
        this.bulletPos = bulletPos;
        this.damage = damage;

        this.reloadTimer = reloadTimer;

    }

    float timer = 0f;

    public void Update(float delta)
    {
        timer += delta;

        if (!isReloading)
        if (timer >= fireRate)
        {
            timer = 0f;
            canFire = true;
        }
    }

    private bool isReloading = false;

    public void reloadDone()
    {
        canFire = true;
        isReloading = false;
    }

    public void Reload()
    {

        if (bullets < 20 && magazineSize > 0)
        {
            reloadTimer.Start();
            canFire = false;
            isReloading = true;
        }

        if (bullets < 20 && magazineSize >= 20)
        {
            magazineSize -= (20 - bullets);
            bullets = 20;
        }
        else if (bullets < 20 && magazineSize < 20)
        {
            if (magazineSize - (20 - bullets) > 0)
            {
                magazineSize -= (20 - bullets);
                bullets = 20;
            }
            else
            {
                bullets += magazineSize;
                magazineSize = 0;
            }
        }
    }

    private void spawnBullet()
    {
        if (gunSound != null)
            gunSound.Play();

        PackedScene bulletScene = new PackedScene();

        switch(type)
        {
            case TYPE.NORMAL:
                bulletScene = ResourceLoader.Load("FX/Bullet.tscn") as PackedScene;
                break;
            case TYPE.BIG:
                bulletScene = ResourceLoader.Load("FX/BulletBig.tscn") as PackedScene;
                break;
        }


        bullet = bulletScene.Instance() as Bullet;
        bullet.Position = bulletPos.GlobalPosition;
        bullet.direction = Vector2.Right.Rotated(node.Rotation);
        bullet.target = Target;
        bullet.damage = damage;
        node.GetParent().AddChild(bullet);
    }

    public void Run()
    {
        if (bullets <= 0 && magazineSize > 0)
            Reload();

        if (canFire)
        {
            canFire = false;
            if (isInfinite)
                spawnBullet();
            else if (bullets > 0)
            {
                bullets -= 1;
                spawnBullet();
            }
        }
    }

}