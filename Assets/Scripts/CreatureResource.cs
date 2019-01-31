﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Resource stored by a creature, and how that resource effects the creature.
/// </summary>


public class CreatureResource
{
    /// <summary>
    /// Threshold below which resource causes health damage.
    /// </summary>
    public int deficiencyThreshold;
    /// <summary>
    /// Amount of health drain from deficiency in one time step.
    /// </summary>
    public int deficiencyHealthDrain;
    /// <summary>
    /// Amount of the resource currently stored.
    /// </summary>
    public int currentLevel;
    /// <summary>
    /// Amount of health gained when resource is adequate.
    /// </summary>
    public int healthGain;
    /// <summary>
    /// Threshold above which health is gained.
    /// </summary>
    public int healthGainThreshold;
    public int maxLevel;
    public int baseUsage;
    public string name;

    /// <summary>
    /// Update creature health according to resource level.
    /// </summary>
    public void healthUpdate(Creature creature)
    {
        if (currentLevel < deficiencyThreshold)
        {
            creature.health -= deficiencyHealthDrain;
        }
        if (currentLevel > healthGainThreshold)
        {
            if (creature.health + healthGain > creature.maxHealth)
            {
                creature.health = creature.maxHealth;
            }
            else
            {
                creature.health += healthGain;
            }
        }
    }

    public CreatureResource getShallowCopy()
    {
        return (CreatureResource)this.MemberwiseClone();
    }
}