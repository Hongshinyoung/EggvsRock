using System;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Egg"))
        {
            GameManager.Instance.EndGame();
            Debug.Log("Egg가 Rock에 충돌했습니다. 게임 끝. 결과 창을 출력합니다.");
        }
    }
}
