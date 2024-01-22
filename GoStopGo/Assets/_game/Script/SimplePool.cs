using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    public List<GameObject> list_Pooled_Bullet;
    public List<GameObject> list_Pooled_Boomerang;
    public List<ParticleSystem> list_Pooled_Particles;
    public GameObject prefab_Bullet;
    public GameObject prefab_Bot;
    public ParticleSystem prefab_Particle;
    public GameObject prefab_Boomerang;
    public Transform pool_parent;

    public static SimplePool instance;

    void Start()
    {
        instance = this;
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(prefab_Bullet, pool_parent);
            GameObject obj1 = Instantiate(prefab_Boomerang, pool_parent);
            obj.SetActive(false);
            obj1.SetActive(false);
            list_Pooled_Bullet.Add(obj);
            list_Pooled_Boomerang.Add(obj1);
        }
        for (int i = 0; i < 5; i++)
        {
            ParticleSystem obj = Instantiate(prefab_Particle, pool_parent);
            //set play stop
            list_Pooled_Particles.Add(obj);
        }
        for(int i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(prefab_Bot,Get_Random_Position(),Quaternion.identity,pool_parent);
            //obj.SetActive(false);
            list_Pooled_Bullet.Add(obj);
        }
    }
    public Vector3 Get_Random_Position()
    {
        return new Vector3(Random.Range(-80, 80), 1, Random.Range(-80, 80));
    }
    public GameObject Get_Pooled_Bullet()
    {
        return list_Pooled_Bullet.Find(x => x.activeInHierarchy == false);
    }
    public ParticleSystem Get_Pooled_Particle()
    {
        return list_Pooled_Particles.Find(x => x.isPlaying == false);
    }
    public GameObject Get_Pooled_Boomerang()
    {
        return list_Pooled_Boomerang.Find(x => x.activeInHierarchy == false);
    }
    
}
