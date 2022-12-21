using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Main algorithmic logic layer of Dijkstra.
/// <summary>
public class Dijkstra
{
    public SingleGraph Graph { set; get; }
    public double Min_Distance { set; get; }
    public double[,] Matrix { set; get; }

    /********************************************************************************/
     public class InnerCycle_1
    {
            int cur_scan_index;
            double cur_scan_distance;            
            bool cur_vis_state;
            // <int>[k] <double>[min_dis]
            Tuple<int,double> cur_min_straight;
            bool is_matched;
            public InnerCycle_1(int csi, double csd,int cmsi,double cmsd,bool cvs)
            {
                Cur_scan_index = csi;
                Cur_scan_distance = csd;                
                Cur_vis_state = cvs;
                Cur_min_straight = new Tuple<int, double>(cmsi, cmsd);
                if(!cvs && csd < cmsd)
                {
                    Is_matched = true;
                }
                else
                {
                    Is_matched = false;
                }
            
            }

            override
            public String ToString()
            {
                return "<" + cur_scan_index + ", " + cur_scan_distance + ", " + cur_vis_state + "> <" + cur_min_straight.Item1 + ", " + cur_min_straight.Item2 + "> " + is_matched;
            }

            public int Cur_scan_index { get => cur_scan_index; set => cur_scan_index = value; }
            public double Cur_scan_distance { get => cur_scan_distance; set => cur_scan_distance = value; }            
            public bool Cur_vis_state { get => cur_vis_state; set => cur_vis_state = value; }
            public Tuple<int, double> Cur_min_straight { get => cur_min_straight; set => cur_min_straight = value; }
            public bool Is_matched { get => is_matched; set => is_matched = value; }
        }

        public class InnerCycle_2
        {
            int cur_scan_index;
            double cur_scan_distance;            
            bool cur_vis_state;
            // <int>[k] <double>[dis[k]] <double>[matrix[k,j]]
            Tuple<int, double,double> cur_possible_curve;
            bool is_matched;

            public InnerCycle_2(int csi,double csd,int cpci,double cpcd1,double cpcd2,bool cvs)
            {
                Cur_scan_index = csi;
                Cur_scan_distance = csd;
                Cur_vis_state = cvs;
                Cur_possible_curve = new Tuple<int, double,double>(cpci, cpcd1, cpcd2);
                if (!cvs && csd  > cpcd1 + cpcd2)
                {
                    Is_matched = true;
                }
                else
                {
                    Is_matched = false;
                }
            }

        override
        public String ToString()
        {
            return "<" + Cur_scan_index + ", " + Cur_scan_distance + ", " + Cur_vis_state + "> <" + Cur_possible_curve.Item1 + ", " + Cur_possible_curve.Item2 +", "+Cur_possible_curve.Item3+ "> " + Is_matched;
         }
        public int Cur_scan_index { get => cur_scan_index; set => cur_scan_index = value; }
        //public bool Is_matched1 { get => Is_matched; set => Is_matched = value; }
        public double Cur_scan_distance { get => cur_scan_distance; set => cur_scan_distance = value; }
        public bool Cur_vis_state { get => cur_vis_state; set => cur_vis_state = value; }
        public Tuple<int, double, double> Cur_possible_curve { get => cur_possible_curve; set => cur_possible_curve = value; }
        public bool Is_matched { get => is_matched; set => is_matched = value; }
   
    }
    public bool[,] Record_vis { get; set; } // vnum
    public double[,] Record_dis { get; set; } // vnum
    public InnerCycle_1[,] Record_ic_1 { get; set; } // vnum
    public InnerCycle_2[,] Record_ic_2 { get; set; } // vnum
    /********************************************************************************/
    public Dijkstra(SingleGraph Graph)
    {
        this.Graph = Graph;
        Matrix = new double[Graph.VertexNum + 1, Graph.EdgeNum + 1];
        Record_vis = new bool[Graph.VertexNum +1,Graph.VertexNum + 1];
        Record_dis = new double[Graph.VertexNum + 1, Graph.EdgeNum + 1];
        Record_ic_1 = new InnerCycle_1[Graph.VertexNum + 1, Graph.EdgeNum + 1];
        Record_ic_2 = new InnerCycle_2[Graph.VertexNum + 1, Graph.EdgeNum + 1];
        InitMatrix();
        SetMatrix();
    }

    public void InitMatrix()
    {
        for(int i = 1; i <= Graph.VertexNum; i++)
        {
            for(int j = 1; j <= Graph.VertexNum; j++)
            {
                Matrix[i,j] = Double.PositiveInfinity;
            }
        }
    }
    public void SetMatrix()
    {
       foreach(var edge in Graph.EdgeDct)
        {
            Matrix[edge.Value.Item1, edge.Value.Item2] = edge.Value.Item3;
        }
    }

    public void AddRecord(int index,bool[] vis , double[] dis)
    {
        int i = index;
        for(int j = 1; j <= Graph.VertexNum; j++)
        {
            Record_vis[i,j] = vis[j];
            Record_dis[i,j] = dis[j];
        }
    }

    public void StartDijkstra()
    {
        bool[] vis = new bool[Graph.VertexNum + 1];
        double[] dis = new double[Graph.VertexNum + 1];

        for(int i = 1; i <= Graph.VertexNum; i++)
        {
            vis[i] = false;
            dis[i] = Matrix[1, i];
        }
        vis[1] = true;
        dis[1] = 0;
        /********************************************************************************/
        AddRecord(1, vis, dis);
        /********************************************************************************/
        for (int i = 2; i <= Graph.VertexNum; i++)
        {
            int k = 0;
            double min_dis = double.PositiveInfinity;
            // Inner cycle 1, to find the k (refer to the slack point) 
            // together with min_dis (refer to the min distance after the i-th outer cycle, start from index 1, end at index 2~vertex-1) 
            for(int j = 1; j <= Graph.VertexNum; j++)
            {                
                Record_ic_1[i, j] = new InnerCycle_1(j, dis[j], k, min_dis, vis[j]); //
                if(!vis[j]&&dis[j] < min_dis)
                {
                    min_dis = dis[j];
                    k = j;
                }

            }
            // In that case, no edge straightly related to start point 1, min distance is infinity.
            if(min_dis == Double.PositiveInfinity)
            {
                break;
            }
            vis[k] = true;
            // Inner cycle 2, to update all the min distance based on the found k.
            // See also in inner cycle 1. 
            for(int j = 1; j <= Graph.VertexNum; j++)
            {
                Record_ic_2[i, j] = new InnerCycle_2(j,dis[j],k,dis[k],Matrix[k,j],vis[j]); //
                if (!vis[j] && dis[j] > dis[k] + Matrix[k,j])
                {
                    dis[j] = dis[k] + Matrix[k,j];
                }
            }
            /********************************************************************************/
            AddRecord(i, vis, dis);
            /********************************************************************************/
        }
        Min_Distance = dis[Graph.VertexNum];
    }

    public void PrintRecords()
    {
        for (int i = 1; i <= Graph.VertexNum; i++)
        {      
            string str_dis = "";
            string str_vis = "";
            for (int j = 1; j <= Graph.VertexNum; j++)
            {
                str_dis += Record_dis[i, j]+" ";
                str_vis += Record_vis[i, j]+" ";
                if (i < Graph.VertexNum)
                {
                    Debug.Log(Record_ic_1[i+1, j].ToString());
                }
            }
            for(int j = 1; j <= Graph.VertexNum; j++)
            {
                if (i < Graph.VertexNum)
                {
                    Debug.Log(Record_ic_2[i + 1, j].ToString());
                }
            }
        
            Debug.Log(str_dis);
            Debug.Log(str_vis);
        }
    }
}
