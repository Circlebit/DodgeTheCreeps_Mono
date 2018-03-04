using Godot;
using System;

public class Player : Area2D
{
    [Export]
    private int SPEED = 40; // how fast the player will move (pixels/sec)


    private Vector2 velocity; // the player's movement vector
    private Vector2 screensize; // size of the game window


    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        screensize = GetViewportRect().Size;
        velocity = new Vector2();
    }

   public override void _Process(float delta)
   {
       // Called every frame. Delta is time since last frame.
       // Update game logic here.
       velocity = new Vector2();

       // change velocity according to pressed keys
        if (Input.IsActionPressed("ui_right"))
            velocity.x += 1;
        if (Input.IsActionPressed("ui_left"))
            velocity.x -= 1;
        if (Input.IsActionPressed("ui_down"))
            velocity.y += 1;
        if (Input.IsActionPressed("ui_up"))
            velocity.y -= 1;

        var animatedSprite = ((AnimatedSprite)GetNode("AnimatedSprite"));
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * SPEED;
            animatedSprite.Play();
        }
        else
        {
            animatedSprite.Stop();
        }

        Position += velocity * delta;
        float clampedX = GameMath.Clamped(Position.x, 0f, screensize.x);
        float clampedY = GameMath.Clamped(Position.y, 0f, screensize.y);
        Position = new Vector2(clampedX, clampedY);
        //Position.y = GameMath.Clamped(Position.y, 0f, screensize.y);
        
        if (velocity.x != 0)
        {
            animatedSprite.Animation = "right";
            animatedSprite.FlipV = false;
            animatedSprite.FlipH = velocity.x < 0;
        }
        else if (velocity.y != 0)
        {
            animatedSprite.Animation = "up";
            animatedSprite.FlipV = velocity.y < 0;
        }
    }

}

public static class GameMath
{
    public static T Clamped<T>(this T value, T min, T max) where T : IComparable<T>
    {
        if (value == null) throw new ArgumentNullException(nameof(value), "is null.");
        if (min == null) throw new ArgumentNullException(nameof(min), "is null.");
        if (max == null) throw new ArgumentNullException(nameof(max), "is null.");
        //If min <= max, clamp
        if (min.CompareTo(max) <= 0) return value.CompareTo(min) < 0 ? min : value.CompareTo(max) > 0 ? max : value;
        //If min > max, clamp on swapped min and max
        return value.CompareTo(max) < 0 ? max : value.CompareTo(min) > 0 ? min : value;
    }
}