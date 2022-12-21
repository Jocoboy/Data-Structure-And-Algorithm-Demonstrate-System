using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// This class contain the infos that Dijkstra Algoritm needed.
/// Note that this is a Directed Weighted Graph.If EdgeNum of a graph is equal to VertexNum*(VertexNum-1), that means it is a Directed Complete Graph.
/// <summary>

public class SingleGraph
{
    #region properity
    public int VertexNum { get; set; }
    public int EdgeNum { get; set; }
    public Type GraphType { get; set; }
    public bool IsFull { get { return EdgeDct.Count == EdgeNum; } }

    public enum Type
    {
        [Description("有向图")]
        DIRECTED_GRAPH,
        [Description("无向图")]
        UNDIRECTED_GRAPH
    }

    public enum Operations
    {
        [Description("无效操作,边的起点、终点、权值数据非法，请重新输入")]
        NONE_ONE,
        [Description("无效操作,边的ID所映射的起点、终点不一致，请重新输入")]
        NONE_TWO,
        //[Description("无效操作,边的起点、终点与对应ID已绑定，无法更改，请重新输入")]
        //NONE_Three,
        [Description("加入")]
        ADD,
        [Description("更新")]
        UPDATE
    }

    // As you can see, Edge (A,B) is considered as different from Edge(B,A).
    //public Dictionary<Tuple<int, int>, Tuple<int, double>> EdgeDct { get; set; }

    public Dictionary<int,Tuple<int,int,double>> EdgeDct { get; set; }
    #endregion

    public SingleGraph(int VertexNum, int EdgeNum,Type GraphType = Type.DIRECTED_GRAPH)
    {
        this.VertexNum = VertexNum;
        this.EdgeNum = EdgeNum;
        this.GraphType = GraphType;
        this.EdgeDct = new Dictionary<int, Tuple<int,int, double>>();
    }

    public Operations AddOrUpdateEdge(int ID,int from, int to, int weight)
    {
        if(from> VertexNum || to >VertexNum || from <= 0 || to <= 0 || weight < 0 ||from == to)
        {
            return Operations.NONE_ONE;
        }
        SingleEdge se = new SingleEdge(ID,from, to, weight);
        if (!EdgeDct.ContainsKey(se.ID))
        {
            EdgeDct.Add(se.ID, se.Tuple3);
            return Operations.ADD;
        }
        else
        {
            if (EdgeDct[ID].Item1!= from || EdgeDct[ID].Item2!=to)
            {
                return Operations.NONE_TWO;
            }
            else
            {
                EdgeDct[se.ID] = se.Tuple3;
                return Operations.UPDATE;
            }
        }
    }

    public void PrintInfos()
    {
        string infos = string.Format("VertexNum {0} EdgeNum {1}", VertexNum, EdgeNum);
        Debug.Log(infos);
        foreach (var edge in EdgeDct)
        {
            string edgeInfos = string.Format("ID {0} From {1} To {2} Weight {3}", edge.Key, edge.Value.Item1, edge.Value.Item2,edge.Value.Item3);
            Debug.Log(edgeInfos);
        }
    }
}
