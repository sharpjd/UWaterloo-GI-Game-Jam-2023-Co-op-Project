using Assets;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IEntityExtension
{

    GameObject projectileToInstantiate;

    [SerializeField]
    Entity parentEntity;
    public Entity ParentEntity
    {
        get
        {
            return parentEntity;
        }
        set
        {
            parentEntity = value;
            parentEntity.AddEntityExtension(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
