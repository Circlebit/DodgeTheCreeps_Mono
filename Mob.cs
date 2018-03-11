using Godot;
using System;

public class Mob : RigidBody2D
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    [Export]
    private int MIN_SPEED = 150;

    [Export]
    private int MAX_SPEED = 250;

    string[] mob_types = new string[] {"walk", "swim", "fly"};
    static Random rnd = new Random();


    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        int randomIndex = rnd.Next(mob_types.Length);
        ((AnimatedSprite)GetNode("AnimatedSprite")).Animation = mob_types[randomIndex];
        RigidBody2D mob = new Mob();
        AddChild(mob);

        mob.Position = new Vector2(15,15);
        mob.SetLinearVelocity(new Vector2(30,30));
    }

    public void _OnVisibilityScreenExited()
    {
        QueueFree();
    }

//    public override void _Process(float delta)
//    {
//        // Called every frame. Delta is time since last frame.
//        // Update game logic here.
//        
//    }
}
