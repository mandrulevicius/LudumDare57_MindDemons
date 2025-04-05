using System;
using System.Collections.Generic;
using UnityEngine;

public struct Interaction
{
    public Stats Stats;
    public Stats OtherStats;
    public float Damage;
    public Vector2 VelocityAfter;

    public Interaction(Stats stats, Stats otherStats, float damage, Vector2 velocityAfter)
    {
        Stats = stats;
        OtherStats = otherStats;
        Damage = damage;
        VelocityAfter = velocityAfter;
    }
}

public static class Interactions
{
    static readonly List<Interaction> History = new();
    public static event Action<Interaction> OnInteraction;
    
    public static void Add(Stats stats, Stats otherStats, float damage, Vector2 velocityAfter)
    {
        Interaction interaction = new(stats, otherStats, damage, velocityAfter);
        History.Add(interaction);
        OnInteraction?.Invoke(interaction);
        string message = $"{interaction.Stats.name} received {interaction.Damage:F1} damage " +
                         $"(Vel. {interaction.Stats.velocity.magnitude:F1} -> {interaction.VelocityAfter.magnitude:F1})" +
                         $"from {interaction.OtherStats.name} (vel. {interaction.OtherStats.velocity.magnitude:F1}).";
        Debug.Log(message);
    }
}