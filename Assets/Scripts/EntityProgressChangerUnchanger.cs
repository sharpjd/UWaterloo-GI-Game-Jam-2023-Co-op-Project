using UnityEngine;


/// <summary>
/// Note: it is easier to instantiate this with <see cref="instantiateEntityProgressChangerUnchanger(EnemyEntity, float, float, float)"/>
/// Keep in mind that stacking instances of this will also cause misbehavior.
/// </summary>
class EntityProgressChangerUnchanger : MonoBehaviour
{
    [SerializeField]
    float startTime;
    [SerializeField]
    float originalProgressPerSecond;
    [SerializeField]
    float progressChangeFactor;
    [SerializeField]
    float durationSeconds;

    public EnemyEntity EnemyEntityToSlow;

    private void Awake()
    {
        enabled = false;   
    }

    private void Start()
    {

        originalProgressPerSecond = EnemyEntityToSlow.ProgressPerSecond;

        EnemyEntityToSlow.ProgressPerSecond = originalProgressPerSecond * progressChangeFactor;

        startTime = Time.time;
    }

    private void Update()
    {
        if(Time.time - startTime > durationSeconds) {

            EnemyEntityToSlow.ProgressPerSecond = originalProgressPerSecond;
            Destroy(this);
        }
    }

    public static EntityProgressChangerUnchanger instantiateEntityProgressChangerUnchanger(EnemyEntity enemyEntity, float progressChangeFactor, float durationSeconds)
    {
        EntityProgressChangerUnchanger entityProgressChangerUnchanger = enemyEntity.gameObject.AddComponent<EntityProgressChangerUnchanger>();
        entityProgressChangerUnchanger.startTime = Time.time;
        entityProgressChangerUnchanger.progressChangeFactor = progressChangeFactor;
        entityProgressChangerUnchanger.durationSeconds = durationSeconds;
        entityProgressChangerUnchanger.EnemyEntityToSlow = enemyEntity;

        entityProgressChangerUnchanger.enabled = true;


        return entityProgressChangerUnchanger;
    }

}
