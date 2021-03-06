﻿// Eco-Simulator
// Copyright (c) 2019 Brett Layman
// This file is subject to the terms and conditions defined in 'LICENSE.txt', which is part of this source code repository.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// API for Comm Nodes
/// </summary>
public class CommNodeEditor : NodeEditorInterface
{
    public CommInputNode commNode;
    public int nodeLayer;
    private NetworkEditor parentNetCreator;

    public CommNodeEditor(CommInputNode _commNode, int _nodeLayer)
    {
        commNode = _commNode;
        nodeLayer = _nodeLayer;
    }

    public Node getNode()
    {
        return commNode;
    }

    void setBitIndex(int bitIndex)
    {
        commNode.bitIndex = bitIndex;
    }

    void setCommProperty(string property)
    {
        commNode.commProperty = property;
    }

}