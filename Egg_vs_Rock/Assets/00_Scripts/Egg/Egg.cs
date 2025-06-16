using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject target; // Rock
    [SerializeField] private float moveDuration = 5f;
    [SerializeField] private float jumpPower = 2f;
    [SerializeField] private int jumpCount = 1;

    private void Awake()
    {
        //MoveToRock();
    }

    public void MoveToRock()
    {
        if (target == null)
        {
            Debug.Log("Target is null");
            StartCoroutine(WaitAndRetryMoveToRock());
            return;
        }

        // DOTween을 사용하여 타겟으로 이동
        transform.DOMove(target.transform.position, moveDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                // 타겟과의 거리가 4m 이하일 때 점프 실행
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= 4f)
                {
                    JumpToTarget();
                }
            })
            .OnComplete(() => Debug.Log("Reached the target!"));
    }

    private void JumpToTarget()
    {
        // DOTween을 사용하여 점프 애니메이션 실행
        transform.DOJump(target.transform.position, jumpPower, jumpCount, moveDuration / 2)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Debug.Log("Collision with the target!"));
    }

    private IEnumerator WaitAndRetryMoveToRock()
    {
        yield return new WaitUntil(() => target != null);
        MoveToRock();
    }
}