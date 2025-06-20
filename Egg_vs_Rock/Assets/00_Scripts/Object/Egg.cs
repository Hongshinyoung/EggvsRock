using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Egg : MonoBehaviour
{
    [SerializeField] private GameObject target; // Rock
    [SerializeField] private float moveDuration = 5f;
    //[SerializeField] private float jumpPower = 3f; // 점프 힘
    [SerializeField] private float jumpDistanceThreshold = 30f; // 점프 시작 거리

    [Header("Stats")]
    [SerializeField] private string id;
    [SerializeField] private int hp;
    [SerializeField] private int damage;

    [Header("Effet")]
    [SerializeField] private ParticleSystem eggRunEffect; // 이펙트
    [SerializeField] private ParticleSystem eggDestroyEffect; // Egg 파괴 이펙트


    private Rigidbody rb; // Rigidbody 참조

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>(); // Rigidbody 가져오기
        eggRunEffect?.gameObject.SetActive(false); // 이펙트 비활성화
        eggDestroyEffect?.gameObject.SetActive(false); // 파괴 이펙트 비활성화
    }

    public void InitStat()
    {
        if (DataManager.EggStat.TryGetValue(id, out EggStat eggData))
        {
            hp = eggData.Hp;
            damage = eggData.Damage;
        }
        else
        {
            Debug.LogError($"Egg data with ID {id} not found!");
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
        eggRunEffect?.gameObject.SetActive(true); // 이펙트 활성화

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
        if (rb == null)
        {
            Debug.Log("Ridgidbody is null, cannot jump.");
            return;
        }
        // 타겟 방향으로 impulse 적용
        Vector3 jumpDirection = (target.transform.position - transform.position).normalized;
        rb.AddForce(new Vector3(jumpDirection.x, 1, jumpDirection.z * 8), ForceMode.Impulse); // 비율 적절히 조정

        Debug.Log("Jumping towards the target!");
    }

    private IEnumerator WaitAndRetryMoveToRock()
    {
        yield return new WaitUntil(() => target != null);
        MoveToRock();
    }

    public int GetDamage()
    {
        return damage;
    }

    public void BreakEgg()
    {
        eggDestroyEffect?.gameObject.SetActive(true);
        Destroy(gameObject, 1);
    }
}