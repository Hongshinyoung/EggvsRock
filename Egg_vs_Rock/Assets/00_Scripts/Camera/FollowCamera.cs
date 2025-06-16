using UnityEngine;
using DG.Tweening;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject target; // egg
    [SerializeField] private float pathDuration = 5f;
    [SerializeField] private float randomRange = 2f;
    private Vector3[] pathPoints;
    private bool followTarget = false;

    private void Awake()
    {
        if (target != null)
        {
            CreateDronePath();
        }
    }

    private void CreateDronePath()
    {
        // 타겟 기준으로 이동 경로 설정
        pathPoints = new Vector3[]
        {
            target.transform.position + new Vector3(0, 5, -10) + GetRandomOffset(), // 뒤쪽 위 시점
            target.transform.position + new Vector3(10, 3, 0) + GetRandomOffset(),  // 오른쪽 시점
            target.transform.position + new Vector3(0, 2, 10) + GetRandomOffset(),  // 앞쪽 아래 시점
            target.transform.position + new Vector3(-10, 4, 0) + GetRandomOffset(), // 왼쪽 시점
            target.transform.position + new Vector3(0, 5, -10) + GetRandomOffset()  // 원래 위치로 복귀
        };

        // 경로를 따라 이동
        transform.DOPath(pathPoints, pathDuration, PathType.CatmullRom)
            .SetEase(Ease.InOutSine)
            .OnUpdate(() =>
            {
                // 카메라가 타겟을 항상 바라보도록 설정
                transform.LookAt(target.transform);
            })
            .OnComplete(() =>
            {
                followTarget = true; // 드론 연출이 끝난 후 타겟을 따라가기 시작
                target.GetComponent<Egg>().MoveToRock(); // 타겟이 Rock으로 이동하도록 호출
            });
    }

    private void LateUpdate()
    {
        if (followTarget && target != null)
        {
            // 카메라가 타겟을 따라가며 일정한 오프셋 유지
            Vector3 offset = new Vector3(0, 5, -10); // 타겟과의 거리 유지
            transform.position = target.transform.position + offset;
            transform.LookAt(target.transform); // 타겟을 계속 바라봄
        }
    }

    // 랜덤 오프셋 생성
    private Vector3 GetRandomOffset()
    {
        return new Vector3(
            Random.Range(-randomRange, randomRange),
            Random.Range(-randomRange, randomRange),
            Random.Range(-randomRange, randomRange)
        );
    }
}