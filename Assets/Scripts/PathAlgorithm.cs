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

        Stack<Node> st =  new Stack<Node>();
        Stack<Node> finalPath = new Stack<Node>();

        
        st.Push(nodes[new Point(0,2)]);
        st.Push(nodes[new Point(1,2)]);
        st.Push(nodes[new Point(2,2)]);
        st.Push(nodes[new Point(2,3)]);
        st.Push(nodes[new Point(2,4)]);
        st.Push(nodes[new Point(3,4)]);
        st.Push(nodes[new Point(3,5)]);
        st.Push(nodes[new Point(4,5)]);
        st.Push(nodes[new Point(5,5)]);
        st.Push(nodes[new Point(5,6)]);
        st.Push(nodes[new Point(5,7)]);
        st.Push(nodes[new Point(6,7)]);
        st.Push(nodes[new Point(6,8)]);
        st.Push(nodes[new Point(7,8)]);
        st.Push(nodes[new Point(8,8)]);
        st.Push(nodes[new Point(9,8)]);
        st.Push(nodes[new Point(9,7)]);
        st.Push(nodes[new Point(9,6)]);
        st.Push(nodes[new Point(9,5)]);
        st.Push(nodes[new Point(8,5)]);
        st.Push(nodes[new Point(7,5)]);
        st.Push(nodes[new Point(7,4)]);
        st.Push(nodes[new Point(7,3)]);
        st.Push(nodes[new Point(8,3)]);
        st.Push(nodes[new Point(9,3)]);
        st.Push(nodes[new Point(10,3)]);
        st.Push(nodes[new Point(11,3)]);
        st.Push(nodes[new Point(12,3)]);
        st.Push(nodes[new Point(12,4)]);
        st.Push(nodes[new Point(12,5)]);
        st.Push(nodes[new Point(12,6)]);
        st.Push(nodes[new Point(13,6)]);
        st.Push(nodes[new Point(13,7)]);
        st.Push(nodes[new Point(13,8)]);
        st.Push(nodes[new Point(14,8)]);
        st.Push(nodes[new Point(15,8)]);
        st.Push(nodes[new Point(15,7)]);
        st.Push(nodes[new Point(15,6)]);
        st.Push(nodes[new Point(16,6)]);
        st.Push(nodes[new Point(17,6)]);
        st.Push(nodes[new Point(18,6)]);

        while (st.Count != 0) {
            finalPath.Push(st.Pop());
        }

        return finalPath;
    }
}
