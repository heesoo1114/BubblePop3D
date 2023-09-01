using System.Collections;
using UnityEngine;
using System;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instacne = null;

    private DiceContoller[] _diceController = new DiceContoller[5];
    private DiceContoller _selectController = null;

    private int[] diceInfo = new int[5];
    public int[] DiceInfo => diceInfo;

    private bool isRollDone;
     
    // Select
    private bool isSelecting;
    private bool isChanging;
    private int selectedIndex = 0;
    private int initIndex = 0;

    public Action rollDoneEvent;
    public Action outOfDiceList;

    private void Awake()
    {
        if (Instacne == null) Instacne = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            _diceController[i] = transform.GetChild(i).GetComponent<DiceContoller>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AllDiceRoll();
        }

        if (isRollDone && !isChanging)
        {
            float inputH = Input.GetAxisRaw("Horizontal");

            if (inputH != 0)
            {
                ChangeSelectDice(inputH);
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isSelecting)
            {
                _selectController.SetDiceKeepOrOut();
            }
        }
    }

    #region Select

    public void StartSelectDice()
    {
        isSelecting = true;

        _selectController = _diceController[initIndex];
        ChangeSelectDice();
    }

    // �ֻ��� ���� �غ� ������ �� ����
    public void EndSelectDice()
    {
        if (_selectController != null)
        {
            _selectController.SelectScaleAnimStop();
            _selectController = null;
            selectedIndex = initIndex;
        }
        isSelecting = false;
    }

    public void ChangeSelectDice(float axis = 0)
    {
        Debug.Log(selectedIndex + (int)axis);
        StartCoroutine(Col_ChangeSelectDice(selectedIndex + (int)axis));
    }

    private IEnumerator Col_ChangeSelectDice(int currentIndex)
    {
        if (currentIndex < 0 || currentIndex >= _diceController.Length)
        {
            Debug.Log("���� ���� ����");
            outOfDiceList?.Invoke();
            yield break;
        }

        isChanging = true;

        _selectController.SelectScaleAnimStop(); // �ٲٱ� ���� �ִϸ��̼� ����
        _selectController = _diceController[currentIndex]; // �ٲ���
        _selectController.SelectScaleAnimPlay(); // �ٲ� �ֻ��� �ִϸ��̼� ����
        
        selectedIndex = currentIndex;

        yield return new WaitForSeconds(_selectController.SelectAnimSpeed);
        isChanging = false;
    }

    #endregion

    #region Check

    // ��� ���̽����� �� �� �غ� �Ǿ�����
    private bool AllDiceReady()
    {
        foreach (DiceContoller _controller in _diceController)
        {
            if (!_controller.IsReady())
            {
                return false;
            }
        }
        return true;
    }

    // �ֻ��� �� �ϳ��� �̻��� ������ ���
    public bool IsCorrectRoll()
    {
        foreach (DiceContoller _controller in _diceController)
        {
            if (_controller.DiceNum == 0)
            {
                return false;
            }
        }
        return true;
    }

    // ��� ���̽��� ŵ�Ͽ�����
    public bool AllDiceKeep()
    {
        foreach (DiceContoller _controller in _diceController)
        {
            if (!_controller.IsKeep())
            {
                return false;
            }
        }
        return true;
    }

    #endregion

    #region Flow

    public void AllDiceRoll()
    {
        if (!AllDiceReady()) return;
        if (AllDiceKeep()) return;

        EndSelectDice();

        isRollDone = false;
        foreach (DiceContoller _controller in _diceController)
        {
            _controller.RollCube();
        }

        StartCoroutine(LaterRollCheck());
    }

    private IEnumerator LaterRollCheck()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            if (AllDiceReady())
            {
                AllDiceReset();

                if (!IsCorrectRoll())
                {
                    Debug.Log("is not correct");
                    yield return new WaitForSeconds(_diceController[0].MoveAnimSpeed);
                    AllDiceRoll();
                }
                else
                {
                    Debug.Log("is correct");
                    RollDone();
                }

                yield break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    // ���� �� �������� �Ǿ��� �� ����Ǵ� �Լ�
    private void RollDone()
    {
        isRollDone = true;

        StartSelectDice();
        SetDiceNumInfo();

        rollDoneEvent?.Invoke();

        Array.Clear(diceInfo, 0, diceInfo.Length);
    }
    
    public void AllDiceReset()
    {
        if (!AllDiceReady()) return;

        foreach (DiceContoller _controller in _diceController)
        {
            _controller.ResetCube();
        }
    }

    private void SetDiceNumInfo()
    {
        for (int i = 0; i < _diceController.Length; i++)
        {
            diceInfo[i] = _diceController[i].DiceNum;
        }
    }

    #endregion
}
