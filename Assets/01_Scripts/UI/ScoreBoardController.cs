using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ScoreBoardController : MonoBehaviour
{
    private FakeScoreDataSO fakeScoreData;
    private RealScoreDataSO realScoreData;

    [SerializeField] private List<ScoreText> scoreKindList;
    [SerializeField] private ScoreText subtotalText;

    private bool isScoreBoardOn = false;
    private bool isNeedRoll = false;

    private ScoreText _selectScoreKind;

    // select
    private bool isChanging;
    private int selectedIndex = 0;
    private int initIndex = 0;

    [Header("Anim")]
    [SerializeField] private float EndFontSize;
    [SerializeField] private float scaleAnimSpeed = 1f;
    private float startFontSize;

    private PanelMoveAnimation _panelMoveAnim;

    private void Awake()
    {
        fakeScoreData = Resources.Load<FakeScoreDataSO>("FakeScoreData");
        realScoreData = Resources.Load<RealScoreDataSO>("RealScoreData");
        _panelMoveAnim = GetComponent<PanelMoveAnimation>();
    }

    private void Start()
    {
        DiceManager.Instacne.outOfDiceList += StartSelectScoreKind;
        DiceManager.Instacne.roolStartEvent += () => isNeedRoll = false;

        GameManager.Instance.GameDoneEvent += InitScoreBoard;

        startFontSize = scoreKindList[0].TextMeshPro.fontSize;
    }

    private void Update()
    {
        if (isScoreBoardOn && !isChanging)
        {
            float inputV = Input.GetAxisRaw("Vertical");
            float inputH = Input.GetAxisRaw("Horizontal");

            if (inputH > 0)
            {
                Debug.Log("�ٽ� �ֻ��� ��������");
                EndSelectScorekind();
            }

            if (inputV != 0)
            {
                ChangeScoreKind(-inputV);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (_selectScoreKind != null)
                {
                    SetRealScore();
                }
            }
        }
    }

    // GameDoneEvent
    private void InitScoreBoard()
    {
        isScoreBoardOn = false;
        isNeedRoll = false;

        foreach (var scoreKind in scoreKindList)
        {
            scoreKind.TextMeshPro.text = "00";
        }

        Debug.Log(realScoreData.GetAllTotal());

        fakeScoreData.ResetValue();
        realScoreData.ResetValue();
    }

    #region Select

    public void StartSelectScoreKind()
    {
        if (isNeedRoll)
        {
            DiceManager.Instacne.StartSelectDice();
            return;
        }

        isScoreBoardOn = true;

        ShowYouCanPutScore();

        _selectScoreKind = scoreKindList[initIndex];
        ChangeScoreKind();
    }

    public void ShowYouCanPutScore()
    {
        // ���⼭ ���� �� �ִ� ������ �־ �ؽ�Ʈ�� �����ֱ�
        foreach (var scoreKind in scoreKindList)
        {
            if (scoreKind.IsCanPut)
            {
                string name = scoreKind.gameObject.name;
                int temp = fakeScoreData.GetIWantProperty(name);

                scoreKind.TextMeshPro.text = temp.ToString();

                // Debug.Log($"{name} + {temp}");
            }
        }

        subtotalText.TextMeshPro.text = realScoreData.GetSubTotal().ToString();
    }

    // ���� ������ �Ǵ� ����
    public void SetRealScore()
    {
        // _selectScoreKind.IsCanPut �̰� �� �׻� false��
        if (!_selectScoreKind.IsCanPut) return;

        isNeedRoll = true;
        
        GameManager.Instance.TurnCnt++;
        DiceManager.Instacne.AllDiceInit();
        DiceManager.Instacne.IsCanRoll = true;  

        string name = _selectScoreKind.gameObject.name;
        int num = fakeScoreData.GetIWantProperty(name);
        realScoreData.SetIWantProperty(name, num);

        _selectScoreKind.SetScoreText(num);

        EndSelectScorekind();
    }

    // �ֻ��� ���� �غ� ������ �� ����
    public void EndSelectScorekind()
    {
        if (_selectScoreKind != null)
        {
            _selectScoreKind.StartScaleAnim(startFontSize, scaleAnimSpeed);
            _selectScoreKind = null;
            selectedIndex = initIndex;
        }
        isScoreBoardOn = false;
        DiceManager.Instacne.StartSelectDice();
    }

    private void ChangeScoreKind(float axis = 0)
    {
        int index = selectedIndex + (int)axis;
        StartCoroutine(Col_ChangeScoreKind(index));
    }

    private IEnumerator Col_ChangeScoreKind(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= scoreKindList.Count) yield break;

        isChanging = true;

        _selectScoreKind.StartScaleAnim(startFontSize, scaleAnimSpeed);
        _selectScoreKind = scoreKindList[currentIndex];
        _selectScoreKind.StartScaleAnim(EndFontSize, scaleAnimSpeed);

        selectedIndex = currentIndex;

        yield return new WaitForSeconds(scaleAnimSpeed);
        isChanging = false;
    }

    #endregion

    #region Anim

    private void ShowOffPanel()
    {
        _panelMoveAnim.PlayMoveAnim();
    }

    private void ShowPanel()
    {
        _panelMoveAnim.ResetMoveAnim();
    }

    #endregion
}
