using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour {
    [System.Serializable]
    public class Pool
    {
        [SerializeField] private string tag;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private int size;

        public string Tag()
        {
            return tag;
        }

        public GameObject ProjectilePrefab()
        {
            return projectilePrefab;
        }

        public int Size()
        {
            return size;
        }
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDict;

    public static ProjectilePooler Instance;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> projectilePool = new Queue<GameObject>();

            for (int i = 0; i < pool.Size(); i++)
            {
                GameObject projectile = Instantiate(pool.ProjectilePrefab());
                projectile.SetActive(false);
                projectile.transform.SetParent(transform);
                projectilePool.Enqueue(projectile);
            }

            poolDict.Add(pool.Tag(), projectilePool);
        }
	}

    public GameObject SpawnProjectile(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDict.ContainsKey(tag))
        {
            return null;
        }

        GameObject projectile = poolDict[tag].Dequeue();

        projectile.SetActive(true);
        projectile.transform.position = position;
        projectile.transform.rotation = rotation;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        float speed = projectile.GetComponent<Projectile>().data.Speed();
        rb.AddForce(rb.transform.forward * speed);

        poolDict[tag].Enqueue(projectile);

        return projectile;
    }
}
