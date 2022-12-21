using System;
using UnityEngine;

/// <summary>
/// This class provide infos of each edge for the graph construction.
/// <summary>

public class SingleEdge
{
    #region properity
    public int ID { get; set; }
    public int From { get; set; }
    public int To { get; set; }
    public double Weight { get; set; }
    //public Tuple<int,int> Int2Int { get; set; }
    //public Tuple<int,double> Int2Double { get; set; }
    public Tuple<int,int,double> Tuple3 { get; set; }
    #endregion

    public SingleEdge(int ID,int From,int To, double Weight)
    {
        this.ID = ID;
        this.From = From;
        this.To = To;
        this.Weight = Weight;
        //this.Int2Int = Tuple.Create(From, To);
        //this.Int2Double = Tuple.Create(ID, Weight);
        this.Tuple3 = Tuple.Create(From, To, Weight);
    }

    public void PrintInfos()
    {
        string infos = string.Format("ID {0} From {1} To {2} Weight {3}", ID,From, To, Weight);
        Debug.Log(infos);
    }
}
