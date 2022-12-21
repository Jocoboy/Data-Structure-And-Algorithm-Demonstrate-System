using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This script is used to monitor UI events (belong to UICamera).
/// <summary>

public class UIControls : MonoBehaviour
{
    private Transform mainCameraTf;
    private Transform mainCameraTf_copy;
    private Button btnBack;
    private Button btnResetCamera;
    private Button btnLookAtEdges;
    private Button btnLaunch;
    private Button btnPrevious;
    private Button btnNext;
    private Button btnAuto;
    private Button btnKill;
    [SerializeField]
    private TextMeshProUGUI[] tmps;
    private bool isAtEdges = false;
    private bool isFar = false;
    private int index_i;
    private int index_j;
    private int ic_id;
    private void Awake()
    {
        mainCameraTf = GameObject.Find("Main Camera").GetComponent<Transform>();
        mainCameraTf_copy = mainCameraTf;
        btnBack = GameObject.Find("Canvas(UI Camera)/BtnBack").GetComponent<Button>();
        btnResetCamera = GameObject.Find("Canvas(UI Camera)/BtnResetCamera").GetComponent<Button>();
        btnLookAtEdges = GameObject.Find("Canvas(UI Camera)/BtnLookAtEdges").GetComponent<Button>();
        btnLaunch = GameObject.Find("Canvas(UI Camera)/BtnLaunch").GetComponent<Button>();
        btnPrevious = GameObject.Find("Canvas(UI Camera)/BtnPrevious").GetComponent<Button>();
        btnNext = GameObject.Find("Canvas(UI Camera)/BtnNext").GetComponent<Button>();
        btnAuto = GameObject.Find("Canvas(UI Camera)/BtnAuto").GetComponent<Button>();
        btnKill = GameObject.Find("Canvas(UI Camera)/BtnKill").GetComponent<Button>();
        tmps = GameObject.Find("Canvas(UI Camera)/TxtWrapper").GetComponentsInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        BindingEvents();
        btnPrevious.interactable = false;
        btnNext.interactable = false;       
        btnAuto.interactable = false;
        btnKill.interactable = false;
    }

    private void BindingEvents()
    {
        btnBack.onClick.AddListener(() =>{
            SceneManager.LoadScene("SettingScene");
        });

        btnResetCamera.onClick.AddListener(() =>
        {
            //Camera.main.transform.SetPositionAndRotation(mainCameraTf_copy.position,mainCameraTf_copy.rotation);
            //Camera.main.transform.SetPositionAndRotation(new Vector3(0,10.5f,-60),Quaternion.identity);
            if (!isFar)
            {
                Vector3 currentPos = mainCameraTf.position;
                Vector3 targetPos = currentPos + Vector3.up * 30;
                StartCoroutine(MoveObject(currentPos, targetPos, 1.5f, mainCameraTf));
                btnResetCamera.GetComponentInChildren<TextMeshProUGUI>().text = "拉近镜头";
                btnLookAtEdges.interactable = false;
            }
            else
            {
                Vector3 currentPos = mainCameraTf.position;
                Vector3 targetPos = currentPos - Vector3.up * 30;
                StartCoroutine(MoveObject(currentPos, targetPos, 1.5f, mainCameraTf));
                btnResetCamera.GetComponentInChildren<TextMeshProUGUI>().text = "拉远镜头";
                btnLookAtEdges.interactable = true;
            }
            isFar = !isFar;
        });

        btnLookAtEdges.onClick.AddListener(() =>
        {
            if (!isAtEdges)
            {
                //btnResetCamera.onClick.Invoke();
                //mainCameraTf.SetPositionAndRotation(new Vector3(0, 40, -60),mainCameraTf.rotation);
                //mainCameraTf.Translate(Vector3.up * 30, Space.World);
                Vector3 currentPos = mainCameraTf.position;
                Vector3 targetPos = currentPos+Vector3.forward*60 + Vector3.up * 30;
                StartCoroutine(MoveObject(currentPos, targetPos, 1.5f,mainCameraTf));
                btnLookAtEdges.GetComponentInChildren<TextMeshProUGUI>().text = "返回主场景";
                btnResetCamera.interactable = false;
            }
            else
            {
                Vector3 currentPos = mainCameraTf.position;
                Vector3 targetPos = currentPos - Vector3.forward * 60 - Vector3.up * 30;
                StartCoroutine(MoveObject(currentPos, targetPos, 1.5f,mainCameraTf));
                btnLookAtEdges.GetComponentInChildren<TextMeshProUGUI>().text = "查看边集信息";
                btnResetCamera.interactable = true;
                //btnResetCamera.onClick.Invoke();
            }   
            isAtEdges = !isAtEdges;
        });

        btnLaunch.onClick.AddListener(() =>
        {
            index_i = 2;
            index_j = 1;
            ic_id = 1;
            Dijkstra.InnerCycle_1 ic1_21 = Utilities.newDijkstra.Record_ic_1[index_i, index_j];
            tmps[0].text = "正在寻找松弛点，\n"
                                    + "当前遍历到" + ic1_21.Cur_scan_index + "号点,\n" 
                                    +"该点是否已被作为松弛点："+ ic1_21.Cur_vis_state+",\n"
                                    +"1号点到该点的距离为"+ ic1_21.Cur_scan_distance+",\n"
                                    +"当前既定松弛点为"+ ic1_21.Cur_min_straight.Item1+"号点,\n"
                                    +"其对应距离为"+ ic1_21.Cur_min_straight.Item2+",\n"
                                    +"是否满足松弛点更新条件："+ic1_21.Is_matched;
            //Dijkstra.InnerCycle_2 ic2_21 = Utilities.newDijkstra.Record_ic_2[2, 1];
            //tmps[1].text = "正在更新1号点到所有点的距离，\n"
            //                        + "当前遍历到" + ic2_21.Cur_scan_index + "号点,\n"
            //                        + "该点是否已被作为松弛点：" + ic2_21.Cur_vis_state + ",\n"
            //                        + "1号点到该点的距离为" + ic2_21.Cur_scan_distance + ",\n"
            //                        + "当前既定松弛点为"+ic2_21.Cur_possible_curve.Item1+",\n"
            //                        +"1号点到既定松弛点的距离为"+ic2_21.Cur_possible_curve.Item2+",\n"
            //                        +"既定松弛点到该点的直接距离为"+ic2_21.Cur_possible_curve.Item3+",\n"
            //                        + "是否满足更新条件："+ic2_21.Is_matched;
            tmps[1].text = "等待中......";
            tmps[2].text = "变量监控";

            string str_dis = "1号点到各点的距离：";
            for(int j = 1; j <= Utilities.VertexNum; j++)
            {
                str_dis += Utilities.newDijkstra.Record_dis[index_i - 1, j] + ",";
            }
            tmps[3].text = str_dis.Substring(0,str_dis.Length-1);

            string str_vis = "各顶点作为松弛点的访问状态：";
            for (int j = 1; j <= Utilities.VertexNum; j++)
            {
                str_vis += Utilities.newDijkstra.Record_vis[index_i - 1, j]  + ",";
            }
            tmps[4].text = str_vis.Substring(0, str_vis.Length - 1);

            btnLaunch.interactable = false;
            //btnPrevious.interactable = true;
            btnNext.interactable = true;
            btnAuto.interactable = true;
            btnKill.interactable = true;
            //Debug.Log("new lanuch");
            //print("on lanuch :" + index_i + "," + index_j);
        });

        btnPrevious.onClick.AddListener(() =>
        {
            btnNext.interactable = true;
            btnAuto.interactable = true;
            ClickBtnPrevious();
            //print("on pre :" + index_i + "," + index_j);
        });

        btnNext.onClick.AddListener(() => {
            btnPrevious.interactable = true;
            ClickBtnNext();
            //print("on nxt :" + index_i + "," + index_j);
        });

        btnAuto.onClick.AddListener(() =>
        {
            StartCoroutine(WaitToClick(1));
        });

        btnKill.onClick.AddListener(() =>
        {
            foreach(var tmp in tmps)
            {
                tmp.text = "";
            }
            btnLaunch.interactable = true;
            btnPrevious.interactable = false;
            btnNext.interactable = false;
            btnAuto.interactable = false;
            btnKill.interactable = false;
        });
    }
    
    private void ClickBtnPrevious()
    {
       
        if(ic_id == 1&&  index_i == 2 && index_j <= 1)
        {
            btnPrevious.interactable = false;
            tmps[0].text = "等待中......";
            //Debug.Log("started.");
            return;
        } 
        index_j--;

        if (ic_id == 1)
        {
            if (index_j >= 1)
            {
                Dijkstra.InnerCycle_1 ic1 = Utilities.newDijkstra.Record_ic_1[index_i, index_j];
                tmps[0].text = "正在寻找松弛点，\n"
                                                + "当前遍历到" + ic1.Cur_scan_index + "号点,\n"
                                                + "该点是否已被作为松弛点：" + ic1.Cur_vis_state + ",\n"
                                                + "1号点到该点的距离为" + ic1.Cur_scan_distance + ",\n"
                                                + "当前既定松弛点为" + ic1.Cur_min_straight.Item1 + "号点,\n"
                                                + "其对应距离为" + ic1.Cur_min_straight.Item2 + ",\n"
                                                + "是否满足松弛点更新条件：" + ic1.Is_matched;
            }
            else
            {
                ic_id = 2;
                index_j = Utilities.graph.VertexNum;
                index_i--;
                tmps[0].text = "等待中......";
            }
        }
        else // ic_id = 2
        {
            if (index_j >=1)
            {
                Dijkstra.InnerCycle_2 ic2 = Utilities.newDijkstra.Record_ic_2[index_i, index_j];
                tmps[1].text = "正在更新1号点到所有点的距离，\n"
                                                     + "当前遍历到" + ic2.Cur_scan_index + "号点,\n"
                                                     + "该点是否已被作为松弛点：" + ic2.Cur_vis_state + ",\n"
                                                     + "1号点到该点的距离为" + ic2.Cur_scan_distance + ",\n"
                                                     + "当前既定松弛点为" + ic2.Cur_possible_curve.Item1 + ",\n"
                                                     + "1号点到既定松弛点的距离为" + ic2.Cur_possible_curve.Item2 + ",\n"
                                                     + "既定松弛点到该点的直接距离为" + ic2.Cur_possible_curve.Item3 + ",\n"
                                                     + "是否满足更新条件：" + ic2.Is_matched;
            }
            else
            {
                ic_id = 1;
                index_j = Utilities.graph.VertexNum;
                Dijkstra.InnerCycle_1 ic1 = Utilities.newDijkstra.Record_ic_1[index_i, index_j];
                tmps[0].text = "正在寻找松弛点，\n"
                                                + "当前遍历到" + ic1.Cur_scan_index + "号点,\n"
                                                + "该点是否已被作为松弛点：" + ic1.Cur_vis_state + ",\n"
                                                + "1号点到该点的距离为" + ic1.Cur_scan_distance + ",\n"
                                                + "当前既定松弛点为" + ic1.Cur_min_straight.Item1 + "号点,\n"
                                                + "其对应距离为" + ic1.Cur_min_straight.Item2 + ",\n"
                                                + "是否满足松弛点更新条件：" + ic1.Is_matched;
                tmps[1].text = "等待中......";

                string str_dis = "1号点到各点的距离：";
                for (int j = 1; j <= Utilities.VertexNum; j++)
                {
                    str_dis += Utilities.newDijkstra.Record_dis[index_i - 1, j] + ",";
                }
                tmps[3].text = str_dis.Substring(0, str_dis.Length - 1);

                string str_vis = "各顶点作为松弛点的访问状态：";
                for (int j = 1; j <= Utilities.VertexNum; j++)
                {
                    str_vis += Utilities.newDijkstra.Record_vis[index_i - 1, j] + ",";
                }
                tmps[4].text = str_vis.Substring(0, str_vis.Length - 1);
            }
        }
    }

    private void ClickBtnNext()
    {
       
        if (index_i > Utilities.graph.VertexNum)
        {
            btnNext.interactable = false;
            btnAuto.interactable = false;
            //btnKill.interactable = true;
            //Debug.Log("ended");
            return;
        }
        index_j++;

        if (ic_id == 1)
        {
            if (index_j <= Utilities.graph.VertexNum)
            {
                Dijkstra.InnerCycle_1 ic1 = Utilities.newDijkstra.Record_ic_1[index_i, index_j];
                tmps[0].text = "正在寻找松弛点，\n"
                                                + "当前遍历到" + ic1.Cur_scan_index + "号点,\n"
                                                + "该点是否已被作为松弛点：" + ic1.Cur_vis_state + ",\n"
                                                + "1号点到该点的距离为" + ic1.Cur_scan_distance + ",\n"
                                                + "当前既定松弛点为" + ic1.Cur_min_straight.Item1 + "号点,\n"
                                                + "其对应距离为" + ic1.Cur_min_straight.Item2 + ",\n"
                                                + "是否满足松弛点更新条件：" + ic1.Is_matched;
            }
            else
            {
                ic_id = 2;
                index_j = 1;
                tmps[0].text = "等待中......";
            }
        }
        else // ic_id = 2
        {
            if (index_j <= Utilities.graph.VertexNum)
            {
                Dijkstra.InnerCycle_2 ic2 = Utilities.newDijkstra.Record_ic_2[index_i, index_j];
                tmps[1].text = "正在更新1号点到所有点的距离，\n"
                                                     + "当前遍历到" + ic2.Cur_scan_index + "号点,\n"
                                                     + "该点是否已被作为松弛点：" + ic2.Cur_vis_state + ",\n"
                                                     + "1号点到该点的距离为" + ic2.Cur_scan_distance + ",\n"
                                                     + "当前既定松弛点为" + ic2.Cur_possible_curve.Item1 + ",\n"
                                                     + "1号点到既定松弛点的距离为" + ic2.Cur_possible_curve.Item2 + ",\n"
                                                     + "既定松弛点到该点的直接距离为" + ic2.Cur_possible_curve.Item3 + ",\n"
                                                     + "是否满足更新条件：" + ic2.Is_matched;
            }
            else
            {
                ic_id = 1;
                index_j = 0;
                index_i++;
                tmps[1].text = "等待中......";

                string str_dis = "1号点到各点的距离：";
                for (int j = 1; j <= Utilities.VertexNum; j++)
                {
                    str_dis += Utilities.newDijkstra.Record_dis[index_i - 1, j] + ",";
                }
                tmps[3].text = str_dis.Substring(0, str_dis.Length - 1);

                string str_vis = "各顶点作为松弛点的访问状态：";
                for (int j = 1; j <= Utilities.VertexNum; j++)
                {
                    str_vis += Utilities.newDijkstra.Record_vis[index_i - 1, j] + ",";
                }
                tmps[4].text = str_vis.Substring(0, str_vis.Length - 1);
            }
        }

    }

    private IEnumerator WaitToClick(float t)
    {
        while (btnAuto.interactable == true)
        {
            ClickBtnNext();
            yield return new WaitForSeconds(t);
        }
        yield return null;
    }

    private IEnumerator MoveObject(Vector3 startPos, Vector3 endPos, float time,Transform tf)
    {
        var dur = 0.0f;
        while (dur <= time)
        {
            dur += Time.deltaTime;
            tf.position = Vector3.Lerp(startPos, endPos, dur / time); // [a,b,t] => a+(b-a)*t
            yield return null;
        }
    }

}
