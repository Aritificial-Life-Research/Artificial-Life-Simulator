﻿//using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// API for alterning ecosystem.
/// </summary>
public class EcosystemEditor : IEcosystemEditor
{
    /// <summary>
    /// Ecosystem to be created.
    /// </summary>
    public Ecosystem ecosystem;
    /// <summary>
    /// Creates Lands to add to EcoSystem resourceOptions.
    /// </summary>
    public LandResourceEditor lrc;
    /// <summary>
    /// Edits map of lands.
    /// </summary>
    public MapEditor mapEditor;
    /// <summary>
    /// For generating founder creatures.
    /// </summary>
    public CreatureEditor creatureCreator;
    /// <summary>
    /// Creatures used to generate populations.
    /// </summary>
    public Dictionary<string, Creature> founderCreatures;
    /// <summary>
    /// Users founder and map to populate species.
    /// </summary>
    public SpeciesPopulator speciesPopulator;
    /// <summary>
    /// Stores populations of creatures.
    /// </summary>
    public Dictionary<string, List<Creature>> populations = new Dictionary<string, List<Creature>>();
    /// <summary>
    /// Tenative list of resource options.
    /// </summary>
    public Dictionary<string, ResourceStore> tentativeResourceOptions;
    /// <summary>
    /// Tentative map to be saved to ecosystem map when saveMap is called.
    /// </summary>
    public List<List<Land>> tentativeMap = new List<List<Land>>();

    // population created by SpeciesPopulator
    private Population currentPopulation;

    public EcosystemEditor(Ecosystem _ecosystem)
    {
        //Debug.Log("ecosystem created");
        ecosystem = _ecosystem;
        tentativeResourceOptions = ecosystem.resourceOptions;
        founderCreatures = ecosystem.species;

    }

    public void setAbilityPointsPerCreature(int abilityPoints)
    {
        ecosystem.abilityPointsPerCreature = abilityPoints;
        Debug.Log("ability points set to: " + abilityPoints);
    }

    public void setCommBits(int numBits)
    {
        ecosystem.commBits = numBits;
        Debug.Log("comm bits set to " + numBits);
    }

    public void setDistinctPhenotypeNum(int numPhenotypes)
    {
        ecosystem.distictPhenotypes = numPhenotypes;
        Debug.Log("distinct phenotypes set to " + numPhenotypes);
    }



    /// <param name="resourceName">Name of resource: used as key in dictionary.</param>
    public void addResource(string resourceName)
    {
        lrc = new LandResourceEditor(new ResourceStore(resourceName));
    }

    /// <summary>
    /// Saves a newly created resource to resourceOptions.
    /// </summary>
    public void saveResource()
    {
        tentativeResourceOptions.Add(lrc.resourceStore.name, lrc.resourceStore);
    }

    /// <summary>
    /// Saves created resourceOptions to ecosystems actual resource options.
    /// </summary>
    public void saveResourceOptions()
    {
        ecosystem.resourceOptions = tentativeResourceOptions;
    }

    public void setTimeUnitsPerTurn(int timeUnits)
    {
        ecosystem.timeUnitsPerTurn = timeUnits;
        Debug.Log("time units per turn set to " + timeUnits);
    }

    public void addToFounders()
    {
        founderCreatures.Add(creatureCreator.creature.species, creatureCreator.creature);
    }

    /// <summary>
    /// Saves founders to ecosystem's species dictionary.
    /// </summary>
    public void saveFoundersToSpecies()
    {
        ecosystem.species = founderCreatures;
    }

    public SpeciesPopulator populateSpecies(string founderSpecies)
    {
        speciesPopulator = new SpeciesPopulator(founderCreatures[founderSpecies], tentativeMap);
        return speciesPopulator;
    }

    /// <summary>
    /// Saves population creatured by speciesPopulator to currentPopulation.
    /// </summary>
    public void saveCurrentPopulation()
    {
        currentPopulation = speciesPopulator.population;
    }

    /// <summary>
    /// Saves currentPopulation to ecosystem.populations
    /// </summary>
    public void addCurrentPopulationToEcosystem()
    {
        ecosystem.populations.Add(currentPopulation.founder.species, currentPopulation);
    }

    public void saveEditedMap()
    {
        tentativeMap = mapEditor.map;
    }

    /// <summary>
    /// Saves tentative map to actual ecosystem map.
    /// </summary>
    public void saveMap()
    {
        ecosystem.map = tentativeMap;
        Debug.Log("map size: " + tentativeMap.Count);
    }

    /// <summary>
    /// Creature new creature
    /// </summary>
    public CreatureEditor addCreature()
    {
        creatureCreator = new CreatureEditor(new Creature(ecosystem.abilityPointsPerCreature), this);
        return creatureCreator;
    }

    /// <summary>
    /// adds a population to the tentative map.
    /// </summary>
    public void addCurrentPopulationToMap()
    {
        for (int i = 0; i < currentPopulation.creatures.Count; i++)
        {
            // place each creature on its location on the map
            currentPopulation.creatures[i].map = tentativeMap;
            tentativeMap[currentPopulation.creatures[i].position[0]][currentPopulation.creatures[i].position[1]].creatureOn = currentPopulation.creatures[i];
        }
    }

    /// <summary>
    /// sets name of dictionary to use when loading
    /// </summary>
    public void setName(string name)
    {
        ecosystem.name = name;
        Debug.Log("Ecosystem name set to " + name);
    }
}