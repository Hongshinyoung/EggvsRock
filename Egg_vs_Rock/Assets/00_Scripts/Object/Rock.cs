using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private string id;
    [SerializeField] private int hp;
    [SerializeField] private int damage;

    public void InitStat()
    {
        if(DataManager.RockStat.TryGetValue(id, out RockStat rockData))
        {
            hp = rockData.Hp;
            damage = rockData.Damage;
        }
        else
        {
            Debug.LogError($"Rock data with ID {id} not found!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            Invoke(nameof(DelayShowResult), 2f); // 충돌 후 결과 창을 지연시켜서 보여줌
            Debug.Log("Egg가 Rock에 충돌했습니다. 게임 끝. 결과 창을 출력합니다.");
            // 충돌 시 파괴 이펙트 재생

            var egg = collision.gameObject.GetComponentInParent<Egg>();
            int damage = egg.GetDamage();

            if (hp <= damage)
            {
                Destroy(gameObject); // Rock 파괴
                Debug.Log("Rock 파괴됨");
                // Rock 파괴 이펙트 재생
            }
            else
            {
                Destroy(collision.gameObject); // Egg 파괴
            }

            //if (damage >= hp)
            //{
            //    Destroy(gameObject); // Rock 파괴
            //    Debug.Log("Rock 파괴됨");
            //}
            //else
            //{
            //    hp = Mathf.Max(0, hp - damage);
            //    if(hp == 0)
            //    {
            //        Destroy(collision.gameObject); // Egg 파괴
            //        // Egg 파괴 이펙트 재생
            //    }

            //    Debug.Log($"Rock의 남은 HP: {hp}");
            //}
        }
    }

    private void DelayShowResult()
    {
        GameManager.Instance.EndGame();
    }
}
