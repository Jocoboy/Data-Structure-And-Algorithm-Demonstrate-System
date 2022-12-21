using System.ComponentModel;
using UnityEngine;

/// <summary>
/// This static class is used to save configurations from UI Manger and handle some key infos for further process.
/// <summary>

public static class Utilities
{

    public static int VertexNum;
    public static int EdgeNum;
    public static SingleGraph graph;
    public static Dijkstra newDijkstra;


    public enum StageOptions
    {
        NONE,
        [Description("设置顶点个数和边数")]
        PRESET_STAG_ONE,
        [Description("设置边集")]
        PRESET_STAG_TWO
    }

    public static StageOptions StageID;

    public static void CalculateMinDistance()
    {
        /******************test module 1×***********************/
        //graph = new SingleGraph(6, 9);
        //graph.EdgeDct.Add(0, new System.Tuple<int, int, double>(1, 2, 1));
        //graph.EdgeDct.Add(1, new System.Tuple<int, int, double>(1, 3, 12));
        //graph.EdgeDct.Add(2, new System.Tuple<int, int, double>(2, 3, 9));
        //graph.EdgeDct.Add(3, new System.Tuple<int, int, double>(2, 4, 3));
        //graph.EdgeDct.Add(4, new System.Tuple<int, int, double>(3, 5, 5));
        //graph.EdgeDct.Add(5, new System.Tuple<int, int, double>(4, 3, 4));
        //graph.EdgeDct.Add(6, new System.Tuple<int, int, double>(4, 5, 13));
        //graph.EdgeDct.Add(7, new System.Tuple<int, int, double>(4, 6, 15));
        //graph.EdgeDct.Add(8, new System.Tuple<int, int, double>(5, 6, 4));
        /******************test module 1×***********************/

        /******************test module 2×***********************/
        //graph = new SingleGraph(7, 12);
        //graph = new SingleGraph(7, 24);
        //graph.EdgeDct.Add(0, new System.Tuple<int, int, double>(1, 2, 3));
        //graph.EdgeDct.Add(1, new System.Tuple<int, int, double>(1, 3, 4));
        //graph.EdgeDct.Add(2, new System.Tuple<int, int, double>(2, 3, 5));
        //graph.EdgeDct.Add(3, new System.Tuple<int, int, double>(2, 4, 6));
        //graph.EdgeDct.Add(4, new System.Tuple<int, int, double>(2, 5, 10));
        //graph.EdgeDct.Add(5, new System.Tuple<int, int, double>(3, 4, 2));
        //graph.EdgeDct.Add(6, new System.Tuple<int, int, double>(4, 5, 7));
        //graph.EdgeDct.Add(7, new System.Tuple<int, int, double>(4, 6, 16));
        //graph.EdgeDct.Add(8, new System.Tuple<int, int, double>(4, 7, 9));
        //graph.EdgeDct.Add(9, new System.Tuple<int, int, double>(5, 6, 12));
        //graph.EdgeDct.Add(10, new System.Tuple<int, int, double>(6, 7, 14));
        //graph.EdgeDct.Add(11, new System.Tuple<int, int, double>(7, 3, 8));

        //graph.EdgeDct.Add(12, new System.Tuple<int, int, double>(2, 1, 3));
        //graph.EdgeDct.Add(13, new System.Tuple<int, int, double>(3, 1, 4));
        //graph.EdgeDct.Add(14, new System.Tuple<int, int, double>(3, 2, 5));
        //graph.EdgeDct.Add(15, new System.Tuple<int, int, double>(4, 2, 6));
        //graph.EdgeDct.Add(16, new System.Tuple<int, int, double>(5, 2, 10));
        //graph.EdgeDct.Add(17, new System.Tuple<int, int, double>(4, 3, 2));
        //graph.EdgeDct.Add(18, new System.Tuple<int, int, double>(5, 4, 7));
        //graph.EdgeDct.Add(19, new System.Tuple<int, int, double>(6, 4, 16));
        //graph.EdgeDct.Add(20, new System.Tuple<int, int, double>(7, 4, 9));
        //graph.EdgeDct.Add(21, new System.Tuple<int, int, double>(6, 5, 12));
        //graph.EdgeDct.Add(22, new System.Tuple<int, int, double>(7, 6, 14));
        //graph.EdgeDct.Add(23, new System.Tuple<int, int, double>(3, 7, 8));
        /**************************************************/

        newDijkstra = new Dijkstra(graph);
        newDijkstra.StartDijkstra();
        //Debug.Log("最短路径长度为"+newDijkstra.Min_Distance);
        //newDijkstra.PrintRecords();
}
}
