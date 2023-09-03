using System.Collections;
using UnityEngine;

enum RotateDir
{
    Right = -1,
    Left = 1
}

public class ObstacleRotation : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private RotateDir dir = RotateDir.Right;
    [SerializeField] private bool immediatelyStart = true;

    [Header("RotateValue")]
    [SerializeField] private float rotateSpeed;

    public bool IsCanRotate { get; set; } = true;

    private void Start()
    {
        if (immediatelyStart) ActRotateRoop();
    }

    // ȸ�� ���� �ڷ�ƾ ����
    public void ActRotateRoop()
    {
        StartCoroutine(RotateRoop());
    }

    // ȸ�� ����
    private IEnumerator RotateRoop()
    {
        while (IsCanRotate)
        {
            transform.rotation *= Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotateSpeed * Time.deltaTime * (float)dir);
            yield return null;
        }
    }
}
