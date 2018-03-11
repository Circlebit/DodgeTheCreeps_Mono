using Godot;
using System;

public class Main : Node
{
    // Member variables here, example:
    // private int a = 2;
    // private string b = "textvar";
    [Export]
    private PackedScene Mob;

    public int score;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here
        _NewGame();
        GetNode("StartTimer").Connect("timeout",this,nameof(_OnStartTimerTimeout));
        GetNode("ScoreTimer").Connect("timeout",this,nameof(_OnScoreTimerTimeout));
        GetNode("MobTimer").Connect("timeout",this,nameof(_OnMobTimerTimeout));
        GetNode("Mob").Connect("hit",this,nameof(_GameOver));
        Console.WriteLine("READY");
    }

    public void _GameOver()
    {
        ((Timer)GetNode("ScoreTimer")).Stop();
        ((Timer)GetNode("MobTimer")).Stop();
    }

    public void _NewGame()
    {
        score = 0;
        //GetNode("Player").start)
        //var player = ((Player)(GetNode("Player"));
        ((Timer)GetNode("StartTimer")).Start();
    }


    public void _OnStartTimerTimeout()
    {
        Console.WriteLine("##### _OnStartTimerTimeout #####");
        ((Timer)GetNode("MobTimer")).Start();
        ((Timer)GetNode("ScoreTimer")).Start();
    }

    public void _OnScoreTimerTimeout()
    {
        Console.WriteLine("##### _OnScoreTimerTimeout #####");
        score += 1;
    }

    public void _OnMobTimerTimeout()
    {
        Console.WriteLine("##### _OnMobTimerTimeout #####");
        
        ((PathFollow2D)GetNode("MobPath/MobSpawnLocation")).SetOffset(0.5f); 
    }


   public override void _Process(float delta)
   {
       // Called every frame. Delta is time since last frame.
       // Update game logic here.
       Console.WriteLine("Start: " + ((Timer)GetNode("StartTimer")).TimeLeft.ToString() +
                         "\t\tMob in:" + ((Timer)GetNode("MobTimer")).TimeLeft.ToString() +
                         "\t\tScoreTimer: " + ((Timer)GetNode("ScoreTimer")).TimeLeft.ToString() + 
                         "\t\tScore: " + score.ToString());
   }
}
