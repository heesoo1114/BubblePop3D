using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCountSystem : MonoBehaviour
{
    [SerializeField] private DiceManager _diceManager;
    [SerializeField] private ScoreDataSO scoreData;

    private List<int> numList = new List<int>();
    private List<int> numCntList = new List<int>();

    private int sum = 0;
    private int bonusScore = 35;
    private int ssFixedScore = 15;
    private int lsFixedScore = 30;
    private int yachtFixedScore = 50;

    private void Start()
    {
        _diceManager.rollDoneEvent += SetAndCount;
    }

    private void SetAndCount()
    {
        StartCoroutine(Col_SetAndCount());
    }

    private IEnumerator Col_SetAndCount()
    {
        scoreData.subtotal = 0;
        scoreData.choice = 0;
        scoreData.fourOfKind = 0;
        scoreData.fullHouse = 0;
        yield return SetNumInfo();
        ScoreAllTypeCount();
    }

    // ���� �ֻ��� ���� ���� ����
    private IEnumerator SetNumInfo()
    {
        numList.Clear();
        numCntList.Clear();

        // List�� �迭�� ���� ����
        for (int i = 0; i < _diceManager.DiceInfo.Length; i++)
        {
            numList.Add(_diceManager.DiceInfo[i]);
            numCntList.Add(0);
        }

        // �ֻ��� ���� ���� �� ���� �ִ��� üũ (SetNumInfo���� ����)
        // �ֻ��� �� ��
        int n = 0;
        for (int i = 0; i < numList.Count; i++)
        {
            n = numList[i];
            // Debug.Log(n - 1);
            numCntList[n - 1]++; 
            sum += n;
        }

        yield return null;
    }

    #region CountScore

    private void ResetScoreInfO()
    {
        // not yet
    }

    private void ScoreAllTypeCount()
    {
        PersonalScoreTypeCount();
        SameScoreTypeCount();
        RowTypeScoreCount();
    }

    private void PersonalScoreTypeCount()
    {
        int num = 0;
        for (int i = 0; i < numCntList.Count; i++)
        {
            num = numCntList[i];
            scoreData.cntScoreList[i] = num * (i + 1);
        }

        // subtotal �κ� ���� �ʿ�
        for (int i = 0; i < scoreData.cntScoreList.Length; i++)
        {
            scoreData.subtotal += scoreData.cntScoreList[i];
        }
    }

    private void SameScoreTypeCount()
    {
        scoreData.choice = sum;

        int max = 0;
        int min = 5;
        foreach (int num in numCntList)
        {
            if (num >= max) max = num;
            if (num <= min) min = num;            
        }

        if (max >= 4)
        {
            scoreData.fourOfKind = sum;

            if (max == 5)
            {
                scoreData.yacht = yachtFixedScore;
            }
        }
        else
        {
            if (min == 2)
            {
                scoreData.fullHouse = sum;
            }
        }
    }

    private void RowTypeScoreCount()
    {
        int cnt = 0;
        int temp = numCntList[0];

        for (int i = 0; i < numCntList.Count; i++)
        {
            if (numCntList[i] == temp)
            {
                temp = numCntList[i];
                cnt++;
            }
        }

        Debug.Log(cnt);

        if (cnt >= 4)
        {
            scoreData.smallStraight = ssFixedScore;

            if (cnt == 5)
            {
                scoreData.largeStraight = lsFixedScore;
            }
        }
    }

    #endregion

}
