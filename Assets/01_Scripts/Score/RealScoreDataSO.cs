using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SO/RealScoreData", fileName = "RealScoreData")]
public class RealScoreDataSO : ScriptableObject
{
    [Header("Personal")]
    // public int[] cntScoreList = new int[6];
    public int aces = 0;
    public int deuces = 0;
    public int threes = 0;
    public int fours = 0;
    public int fives = 0;
    public int sixes = 0;

    public int subtotal = 0; // ���� aces ~ sixes ���� ���� (63�� �̻��� �� ���ʽ� 35�� �߰�)
    public int choice = 0;     // (�ƹ� ���� ����) ���� �� �� ����

    [Header("Same")]
    public int fourOfKind = 0; // (������ �ֻ��� 4�� ���� ��) ���� �� �� ����
    public int fullHouse = 0;  // (������ �ֻ��� 3�� + 2�� ���� ��) ���� �� �� ����
    public int yacht = 0; // (������ �ֻ��� 5��) ���� 50��

    [Header("Row")]
    public int smallStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 4ȸ�� ��) ���� 15��
    public int largeStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 5ȸ�� ��) ���� 30��

    private int allTotal = 0;

    public int GetAllTotal()
    {
        allTotal = aces + deuces + threes + fours + fives + sixes + subtotal + choice + fourOfKind + fullHouse + yacht + smallStraight + largeStraight; ;
        return allTotal;
    }

    public void ResetProperty()
    {
        // Array.Clear(cntScoreList, 0, cntScoreList.Length);

        aces = 0;
        deuces = 0;
        threes = 0;
        fours = 0;
        fives = 0;
        sixes = 0;

        subtotal = 0; // ���� ���� ����
        choice = 0;

        fourOfKind = 0;
        fullHouse = 0;
        yacht = 0;

        smallStraight = 0;
        largeStraight = 0;
    }

   
}
