using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    public List<Choice> Choices { get; set; }
    public Choice PreviousChoice { get; set; }
    public bool Unlocked { get; set; } = false;

    public Path(bool unlocked, List<Choice> choices = null)
    {
        Unlocked = unlocked;
        Choices = choices ?? new List<Choice>();
        PreviousChoice = choices != null ? choices.Where(x => x.Chosen).FirstOrDefault() : Choice.None();
    }
    public static Path Head() => new Path(true, null);
    public static Path CreatePath(bool unlocked, List<Choice> choices) => new Path(unlocked, choices);
    public void TryAddChoice(Choice choice)
    {
        if (Choices.Contains(choice)) return;
        
        Choices.Add(choice);
    }
}
