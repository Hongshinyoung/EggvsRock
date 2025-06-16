using UnityEngine;
using DG.Tweening;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject target; // egg
    [SerializeField] private float pathDuration = 5f;
    [SerializeField] private float randomRange = 2f;
    private Vector3[] pathPoints;

    private void Awake()
    {
        if (target != null)
        {
            CreateDronePath();
        }
    }

    private void CreateDronePath()
    {
        // Ÿ�� �������� �̵� ��� ����
        pathPoints = new Vector3[]
        {
            target.transform.position + new Vector3(0, 5, -10) + GetRandomOffset(), // ���� �� ����
            target.transform.position + new Vector3(10, 3, 0) + GetRandomOffset(),  // ������ ����
            target.transform.position + new Vector3(0, 2, 10) + GetRandomOffset(),  // ���� �Ʒ� ����
            target.transform.position + new Vector3(-10, 4, 0) + GetRandomOffset(), // ���� ����
            target.transform.position + new Vector3(0, 5, -10) + GetRandomOffset()  // ���� ��ġ�� ����
        };

        // ��θ� ���� �̵�
        transform.DOPath(pathPoints, pathDuration, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .OnUpdate(() =>
            {
                // ī�޶� Ÿ���� �׻� �ٶ󺸵��� ����
                transform.LookAt(target.transform);
            })
            .OnComplete(() => Debug.Log("Drone path completed!"));
    }

    // ���� ������ ����
    private Vector3 GetRandomOffset()
    {
        return new Vector3(
            Random.Range(-randomRange, randomRange), 
            Random.Range(-randomRange, randomRange), 
            Random.Range(-randomRange, randomRange)
        );
    }
}