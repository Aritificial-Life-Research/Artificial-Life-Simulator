﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// API for Network objects. Stored by EcosystemCreator.
/// </summary>
public class NetworkCreator
{
    public Network network;
    public NodeCreator nodeCreator;
    public CreatureCreator parentCreatureCreator;

    public NetworkCreator(Network net, CreatureCreator parentCreatureCreator)
    {
        network = net;
        this.parentCreatureCreator = parentCreatureCreator;
        initializeNetwork();
    }

    public void initializeNetwork()
    {
        // add two empty lists to the network (inputs and outputs)
        network.net.Add(new List<Node>());
        network.net.Add(new List<Node>());
        // bias node added to input nodes
        BiasNode bn = new BiasNode();
        bn.setBias(1);
        network.net[0].Add(bn);

    }

    public void setName(string name)
    {
        network.name = name;
    }

    public void setInLayer(int layer)
    {
        network.inLayer = layer;
    }

    public void saveNode()
    {
        //Debug.Log(network);
        //Debug.Log(network.net);
        //Debug.Log(nodeCreator);
        //Debug.Log(network.net[nodeCreator.nodeLayer]);
        //Debug.Log(nodeCreator.getCreatedNode());
        network.net[nodeCreator.nodeLayer].Add(nodeCreator.getCreatedNode());
    }

    public NodeCreator addNode(int layer)
    {
        //Debug.Log("called addNode");
        nodeCreator = new NodeCreator(layer, this);
        return nodeCreator;
    }
}