using System.Collections;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public class HeroSlomo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject circleSprite;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f; // 범위
    [SerializeField] private float attackPeriod = 4f;   // 공격 주기
    [SerializeField] private float freezeTime = 1f;     // 느려지게 하는 시간
    [SerializeField] private float frozenFactor = 0.5f;  // 느려지는 강도

    private float timeUntilFire;

    private void Start()
    {
        circleSprite.transform.localScale = new Vector3(targetingRange * 2, targetingRange * 2, 1f);
        circleSprite.gameObject.SetActive(false);
    }

    private void Update()
    {
        timeUntilFire += Time.deltaTime;

        if (timeUntilFire >= attackPeriod)
        {
            FreezeEnemies();
        }
    }

    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            circleSprite.gameObject.SetActive(true);
            for (int i=0; i<hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(frozenFactor);

                StartCoroutine(ResetEnemySpeed(em));
            }
            timeUntilFire = 0f;
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime);

        circleSprite.gameObject.SetActive(false);
        em.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
