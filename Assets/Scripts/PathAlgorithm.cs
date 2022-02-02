using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathAlgorithm
{
    private static Dictionary<Point, Node> nodes;

    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static Stack<Node> GetPath()
    {
        if(nodes == null)
        {
            CreateNodes();
        }

        Stack<Node> finalPath = new Stack<Node>();

        
        finalPath.Push(nodes[new Point(11,6)]);
        finalPath.Push(nodes[new Point(10,6)]);
        finalPath.Push(nodes[new Point(9,6)]);
        finalPath.Push(nodes[new Point(8,6)]);
        finalPath.Push(nodes[new Point(8,5)]);
        finalPath.Push(nodes[new Point(7,5)]);
        finalPath.Push(nodes[new Point(6,5)]);
        finalPath.Push(nodes[new Point(5,5)]);
        finalPath.Push(nodes[new Point(5,4)]);
        finalPath.Push(nodes[new Point(4,4)]);
        finalPath.Push(nodes[new Point(4,3)]);
        finalPath.Push(nodes[new Point(3,3)]);
        finalPath.Push(nodes[new Point(3,2)]);
        finalPath.Push(nodes[new Point(2,2)]);
        finalPath.Push(nodes[new Point(2,1)]);


        return finalPath;
    }
}
