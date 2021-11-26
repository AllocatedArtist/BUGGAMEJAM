using System;
using Godot;

public class PlayerGUI
{
    ProgressBar healthBar;
    Label ammoLabel;

    public enum STATE
    {
        RELOADING,
        NOT_RELOADING
    }

    public struct guiData
    {
        public int Health { get; set; }
        public int bullet { get; set; }
        public int magazine { get; set; }
        public bool isReloading { get; set; }

        public guiData(int health, int bullet, int magazine, bool reloading)
        {
            Health = health;
            this.bullet = bullet;
            this.magazine = magazine;
            isReloading = reloading;
        }
    }

    public guiData data;
    private STATE state;

    public PlayerGUI(ProgressBar healthBar, Label ammoLabel)
    {
        (this.healthBar, this.ammoLabel) = (healthBar, ammoLabel);
        state = STATE.NOT_RELOADING;
    }

    public void Update()
    {
        if (healthBar.Value >= 99f)
            healthBar.Value = 100;

        healthBar.Value = Mathf.Lerp((float)healthBar.Value, data.Health, 0.2f);

        if (data.isReloading)
            state = STATE.RELOADING;
        else
            state = STATE.NOT_RELOADING;

        switch(state)
        {
            case STATE.RELOADING:
                ammoLabel.Text = "Reloading...";
                break;
            case STATE.NOT_RELOADING:
                ammoLabel.Text = "Ammo: " + data.bullet.ToString() + "/" + data.magazine.ToString();
                break;
        }
    }
}