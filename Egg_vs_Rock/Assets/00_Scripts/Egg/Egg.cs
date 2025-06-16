using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject target; // Rock
    [SerializeField] private float moveDuration = 5f;
    [SerializeField] private float jumpPower = 0.07f; // 점프 힘
    [SerializeField] private float jumpDistanceThreshold = 20f; // 점프 시작 거리
    private Rigidbody rb; // Rigidbody 참조

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>(); // Rigidbody 가져오기
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }
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
        Tweener moveTween = null;
        moveTween = transform.DOMove(target.transform.position, moveDuration)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                // 타겟과의 거리가 특정 거리 이하일 때 점프 실행
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance <= jumpDistanceThreshold)
                {
                    JumpToTarget();
                }
            })
            .OnComplete(() => Debug.Log("Reached the target!"));
    }

    private void JumpToTarget()
    {
        if (rb == null) return;

        // 타겟 방향으로 impulse 적용
        Vector3 jumpDirection = (target.transform.position - transform.position).normalized;
        rb.AddForce(new Vector3(jumpDirection.x, 1, jumpDirection.z) * jumpPower, ForceMode.Impulse);

        Debug.Log("Jumping towards the target!");
    }

    private IEnumerator WaitAndRetryMoveToRock()
    {
        yield return new WaitUntil(() => target != null);
        MoveToRock();
    }
}