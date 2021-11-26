using Godot;
using System;

public class PlayerAnimation
{
    public AnimatedSprite sprite;

    public enum STATE
    {
        IDLE,
        RELOADING
    }

    public STATE state { get; set; }

    public PlayerAnimation(AnimatedSprite sprite)
    {
        state = STATE.IDLE;
        this.sprite = sprite;
    }

    public void Update()
    {
        switch(state)
        {
            case STATE.IDLE:
                sprite.Play("idle");
                break;
            case STATE.RELOADING:
                sprite.Play("reload");
                break;
        }
    }
}