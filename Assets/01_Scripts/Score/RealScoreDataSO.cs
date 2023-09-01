using UnityEngine;

[CreateAssetMenu(menuName = "SO/RealScoreData", fileName = "RealScoreData")]
public class RealScoreDataSO : ScriptableObject
{
    [Header("Personal")]
    public int Aces = 0;
    public int Deuces = 0;
    public int Threes = 0;
    public int Fours = 0;
    public int Fives = 0;
    public int Sixes = 0;

    public int Subtotal = 0; // ���� aces ~ sixes ���� ���� (63�� �̻��� �� ���ʽ� 35�� �߰�)
    public int Choice = 0;     // (�ƹ� ���� ����) ���� �� �� ����

    [Header("Same")]
    public int FourOfKind = 0; // (������ �ֻ��� 4�� ���� ��) ���� �� �� ����
    public int FullHouse = 0;  // (������ �ֻ��� 3�� + 2�� ���� ��) ���� �� �� ����
    public int Yacht = 0; // (������ �ֻ��� 5��) ���� 50��

    [Header("Row")]
    public int SmallStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 4ȸ�� ��) ���� 15��
    public int LargeStraight = 0; // (�ֻ��� ���� ���ӵ� Ƚ���� 5ȸ�� ��) ���� 30��

    public int GetSubTotal() => Aces + Deuces + Threes + Fours + Fives + Sixes;

    public int GetAllTotal() => Subtotal + Choice + FourOfKind + FullHouse + Yacht + SmallStraight + LargeStraight;

    public int GetIWantProperty(string name)
    {
        var temp = GetType().GetField(name).GetValue(this);
        return (int)temp;
    }

    public void SetIWantProperty(string name, int value)
    {
        var temp = GetType().GetField(name);
        temp.SetValue(this, value);
    }

    public void ResetValue()
    {
        Aces = 0;
        Deuces = 0;
        Threes = 0;
        Fours = 0;
        Fives = 0;
        Sixes = 0;

        Subtotal = 0; 
        Choice = 0;     

        FourOfKind = 0; 
        FullHouse = 0;  
        Yacht = 0; 

        SmallStraight = 0; 
        LargeStraight = 0; 
    }
}
