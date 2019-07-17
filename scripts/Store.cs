using Godot;
using System;

public class Store : Node {
  public enum SceneType { Menu, Game, Scoreboard, Winner }

  public static Node instance;
  public static int player1Score = 0;
  public static int player2Score = 0;
  public static int totalRounds = 5;

  // DATA METHODS
  public static string Score() {
    return player1Score + " - " + player2Score;
  }

  public static int Winner() {
    if (totalRounds % 2 == 0) totalRounds += 1;
    
    if (player1Score + player2Score < totalRounds) return 0;
    if (player1Score > player2Score) return 1;
    if (player1Score < player2Score) return 2;
    return 0;
  }

  public static void ScoreReset() {
    player1Score = player2Score = 0;
  }

  // LIFECYCLE METHODS
  public override void _Ready() {
    Store.instance = this;
  }

  public override void _Process(float delta) {
    // Godot.GD.Print(Godot.Engine.GetFramesPerSecond() + " FPS");
  }

  // ROUTING HANDLERS
  public static PackedScene Scene(SceneType scene_type) {
    return (PackedScene)GD.Load("res://scenes/" + scene_type.ToString() + ".tscn");
  }

  public static String ScenePath(SceneType scene_type) {
    return "res://scenes/" + scene_type.ToString() + ".tscn";
  }

  public static void GotoScene(SceneType scene_type) {
    instance.CallDeferred(nameof(DeferredGotoScene), scene_type);
  }

  public void DeferredGotoScene(SceneType scene_type) {
    Node current_scene = CurrentScene();
    current_scene.Free();
    PackedScene next_scene = Scene(scene_type);
    current_scene = next_scene.Instance();
    this.AddChild(current_scene);
  }

  public Node CurrentScene() {
    return this.GetChild(this.GetChildCount() - 1);
  }
}
