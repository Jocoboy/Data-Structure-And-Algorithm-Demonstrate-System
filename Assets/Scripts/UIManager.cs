using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This Script is used to manager configurations in setting scene.
/// See also in Utilites.cs
/// <summary>

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputVertexNum;
    [SerializeField]
    private TMP_InputField inputEdgeNum;
    [SerializeField]
    private TMP_InputField inputFrom;
    [SerializeField]
    private TMP_InputField inputTo;
    [SerializeField]
    private TMP_InputField inputWeight;
    [SerializeField]
    private Button FurtherConfirmBtn01;
    [SerializeField]
    private Button FurtherConfirmBtn02;
    [SerializeField]
    private Button FurtherConfirmBtn03;
    [SerializeField]
    private GameObject Item01;
    [SerializeField]
    private GameObject Item02;
    [SerializeField]
    private GameObject Item03;
    [SerializeField]
    private TMP_Dropdown dpn;
    [SerializeField]
    private TMP_Dropdown dpType;
    [SerializeField]
    private TextMeshProUGUI MsgContent;
    public void Awake()
    {
        inputVertexNum = GameObject.Find("Canvas/Background/Item01/Value").GetComponent<TMP_InputField>();
        inputEdgeNum = GameObject.Find("Canvas/Background/Item02/Value").GetComponent<TMP_InputField>();
        inputFrom = GameObject.Find("Canvas/Background/Item03/I-From/InputField (TMP)").GetComponent<TMP_InputField>();
        inputTo = GameObject.Find("Canvas/Background/Item03/I-To/InputField (TMP)").GetComponent<TMP_InputField>();
        inputWeight = GameObject.Find("Canvas/Background/Item03/I-Weight/InputField (TMP)").GetComponent<TMP_InputField>();
        FurtherConfirmBtn01 = GameObject.Find("Canvas/Background/FurtherConfirmBtn01").GetComponent<Button>();
        FurtherConfirmBtn02 = GameObject.Find("Canvas/Background/FurtherConfirmBtn02").GetComponent<Button>();
        FurtherConfirmBtn03 = GameObject.Find("Canvas/Background/Item03/FurtherConfirmBtn03").GetComponent<Button>();
        Item01 = GameObject.Find("Canvas/Background/Item01");
        Item02 = GameObject.Find("Canvas/Background/Item02");
        Item03 = GameObject.Find("Canvas/Background/Item03");
        dpn = GameObject.Find("Canvas/Background/Item03/Dropdown").GetComponent<TMP_Dropdown>();
        dpType = GameObject.Find("Canvas/Background/GraphTypeDp").GetComponent<TMP_Dropdown>();
        MsgContent = GameObject.Find("Canvas/Background/MsgDialog/Content").GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        Utilities.StageID = Utilities.StageOptions.PRESET_STAG_ONE;
        MsgContent.text = "无";
        Item03.SetActive(false);
        BindingEvent();
    }

    private void BindingEvent()
    {
        FurtherConfirmBtn01.onClick.AddListener(() =>
        {
            if (Utilities.StageID == Utilities.StageOptions.PRESET_STAG_ONE)
            {
                if (int.TryParse(inputVertexNum.text, out int vertexNum))
                {
                    Utilities.VertexNum = vertexNum;
                }
                else
                {
                    Utilities.VertexNum = -1;
                }
                if (int.TryParse(inputEdgeNum.text, out int edgeNum))
                {
                    Utilities.EdgeNum = edgeNum;
                }
                else
                {
                    Utilities.EdgeNum = -1;
                }
                if(
                    Utilities.VertexNum == -1|| Utilities.EdgeNum == -1||
                    (dpType.GetComponentInChildren<TextMeshProUGUI>().text.ToString()=="无向图" &&Utilities.EdgeNum > Utilities.VertexNum * (Utilities.VertexNum - 1) )||
                    (dpType.GetComponentInChildren<TextMeshProUGUI>().text.ToString() == "有向图" && Utilities.EdgeNum > Utilities.VertexNum * (Utilities.VertexNum - 1)/2)
                )
                {
                    MsgContent.text = "顶点个数、边数数据非法，请重新输入";
                    return;
                }
                //Utilities.InitAll();
                //Utilities.graph.PrintInfos();
                Item01.SetActive(false);
                Item02.SetActive(false);
                Item03.SetActive(true);
                for(int i = 0; i < Utilities.EdgeNum; i++)
                {
                    TMP_Dropdown.OptionData op = new TMP_Dropdown.OptionData("第" + i + "条边");
                    dpn.options.Add(op);
                }
                dpn.GetComponentInChildren<TextMeshProUGUI>().text = "第0条边";
                FurtherConfirmBtn01.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "确认并开始";
                FurtherConfirmBtn02.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "取消并返回";
                SingleGraph.Type type = (dpType.GetComponentInChildren<TextMeshProUGUI>().text.ToString() == "有向图" ? SingleGraph.Type.DIRECTED_GRAPH : SingleGraph.Type.UNDIRECTED_GRAPH);
                Utilities.graph = new SingleGraph(Utilities.VertexNum, Utilities.EdgeNum,type);
                Utilities.StageID = Utilities.StageOptions.PRESET_STAG_TWO;

                MsgContent.text = "当前" + MyTool.GetEnumDescription(Utilities.graph.GraphType)+"顶点数为" + Utilities.VertexNum + ",边数为" + Utilities.EdgeNum;
                dpType.interactable = false;
            }
            else if (Utilities.StageID == Utilities.StageOptions.PRESET_STAG_TWO)
            {
                //Utilities.graph.PrintInfos();
                //if (Utilities.graph.IsFull)
                //{
                    SceneManager.LoadScene("DemoScene");
                    Utilities.CalculateMinDistance();
                //}
                //else
                //{
                //    MsgContent.text = "尚有边未被添加，请严格按照预设边数依次输入";
                //}
            }
        });

        FurtherConfirmBtn02.onClick.AddListener(() =>
        {
            if (Utilities.StageID == Utilities.StageOptions.PRESET_STAG_ONE)
            {
                inputVertexNum.text = "请输入顶点个数...";
                inputEdgeNum.text = "请输入边数...";
            }
            else if(Utilities.StageID == Utilities.StageOptions.PRESET_STAG_TWO)
            {
                dpn.ClearOptions();
                inputFrom.text = "请输入起点...";
                inputTo.text = "请输入终点...";
                inputWeight.text = "请输入权重..";
                Item01.SetActive(true);
                Item02.SetActive(true);
                Item03.SetActive(false);
                FurtherConfirmBtn01.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "确认并继续";
                FurtherConfirmBtn02.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "取消并清空";
                inputVertexNum.text = "请输入顶点个数...";
                inputEdgeNum.text = "请输入边数...";
                Utilities.StageID = Utilities.StageOptions.PRESET_STAG_ONE;
                MsgContent.text = "无";
                dpType.interactable = true;
            }
        });

        FurtherConfirmBtn03.onClick.AddListener(() =>
        {
            int.TryParse(dpn.GetComponentInChildren<TextMeshProUGUI>().text[1]+"", out int EdgeID);
            if (int.TryParse(inputFrom.text, out int from))
            {
                // Do nothing
            }
            else
            {
                from = -1;
            }
            if (int.TryParse(inputTo.text, out int to))
            {
                // Do nothing
            }
            else
            {
                to = -1;
            }
            if (int.TryParse(inputWeight.text, out int weight))
            {
                // Do nothing
            }
            else
            {
                weight = -1;
            }
            if (from != -1 && to != -1 && weight != -1)
            {
                string res = MyTool.GetEnumDescription(Utilities.graph.AddOrUpdateEdge(EdgeID,from, to, weight));
                if (res == MyTool.GetEnumDescription(SingleGraph.Operations.NONE_ONE)|| res == MyTool.GetEnumDescription(SingleGraph.Operations.NONE_TWO))
                {
                    MsgContent.text = res;
                }
                else
                {
                    MsgContent.text = res+"第" + EdgeID + "条边"+ ",起点为" + from + ",终点为" + to + ",权重为" + weight;
                }
                //print(from + " " + to + "  " + weight);
            }
        });
    }
}
