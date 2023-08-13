using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ScoreData", fileName = "ScoreData")]
public class ScoreDataSO : ScriptableObject
{
    // Count
    // public int aces = 0;
    // public int deuces = 0;
    // public int threes = 0;
    // public int fours = 0;
    // public int fives = 0;
    // public int sixes = 0;
    [Header("Personal")]
    public int[] cntScoreList = new int[6];
    public int subtotal = 0; // ���� aces ~ sixes ���� ���� (63�� �̻��� �� ���ʽ� 35�� �߰�)
    public int choice = 0;     // (�ƹ� ���� ����) ���� �� �� ����

    // Sum
    [Header("Same")]
    public int fourOfKind = 0; // (������ �ֻ��� 4�� ���� ��) ���� �� �� ����
    public int fullHouse = 0;  // (������ �ֻ��� 3�� + 2�� ���� ��) ���� �� �� ����
    public int yacht = 0; // (������ �ֻ��� 5��) ���� 50��

    // Fixed
    [Header("Row")]
    public int smallStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 4ȸ�� ��) ���� 15��
    public int largeStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 5ȸ�� ��) ���� 30��
}
