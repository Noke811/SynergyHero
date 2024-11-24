using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    [SerializeField] private Tower[] towers;

    private int selectedHero = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedHero()
    {
        return towers[selectedHero];
    }

    public void SetRandomHero()
    {
        int index = Random.Range(0, towers.Length);
        selectedHero = index;
    }
}
