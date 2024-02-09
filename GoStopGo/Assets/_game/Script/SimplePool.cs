using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SimplePool : MonoBehaviour
{
    public Transform pool_parent;
    public GameObject prefab_Bot;
    public GameObject prefab_Bullet;
    public GameObject prefab_Boomerang;
    public ParticleSystem prefab_Particle;
    public List<GameObject> list_Pooled_Bots;
    public List<GameObject> list_Pooled_Bullet;
    public List<GameObject> list_Pooled_Boomerang;
    public List<ParticleSystem> list_Pooled_Particles;

    public static SimplePool instance;
    public void Start()
    {
        instance = this;
        SpawnAndPoolBots(10);
        SpawnAndPoolBullet(10);
        SpawnAndPoolParticle(10);
    }
    public static void Execute()
    {
        instance = FindObjectOfType<SimplePool>();
    }
    public Vector3 Get_Random_Position()
    {
        return new Vector3(Random.Range(-80, 80), 1, Random.Range(-80, 80));
    }
    public GameObject Get_Pooled_Bullet()
    {
        return list_Pooled_Bullet.Find(x => x.activeInHierarchy == false);
    }
    public GameObject Get_Pooled_Boomerang()
    {
        return list_Pooled_Boomerang.Find(x => x.activeInHierarchy == false);
    }
    public ParticleSystem Get_Pooled_Particle()
    {
        return list_Pooled_Particles.Find(x => x.isPlaying == false);
    }
    public void SpawnAndPoolBots(int number_of_bot)
    {
        for (int i = 0; i < number_of_bot; i++)
        {
            GameObject obj = Instantiate(prefab_Bot, Get_Random_Position(), Quaternion.identity, pool_parent);
            obj.SetActive(false);
            list_Pooled_Bots.Add(obj);
        }
    }
    public void SpawnAndPoolBullet(int number_of_bullet)
    {
        for (int i = 0; i < number_of_bullet; i++)
        {
            GameObject obj = Instantiate(prefab_Bullet, pool_parent);
            GameObject obj1 = Instantiate(prefab_Boomerang, pool_parent);
            obj.SetActive(false);
            obj1.SetActive(false);
            list_Pooled_Bullet.Add(obj);
            list_Pooled_Boomerang.Add(obj1);
        }
    }
    public void SpawnAndPoolParticle(int number_of_particle)
    {
        for (int i = 0; i < number_of_particle; i++)
        {
            ParticleSystem obj = Instantiate(prefab_Particle, pool_parent);
            obj.Stop();
            list_Pooled_Particles.Add(obj);
        }
    }
    public void Level_Start()
    {
        foreach (GameObject bot in list_Pooled_Bots)
        {
            bot.SetActive(true);
        }
    }
    public void Reset_Pool()
    {
        foreach (GameObject bot in list_Pooled_Bots)
        {
            bot.SetActive(false);
        }
        foreach (GameObject bullet in list_Pooled_Bullet)
        {
            bullet.SetActive(false);
        }
        foreach (GameObject boomerang in list_Pooled_Boomerang)
        {
            boomerang.SetActive(false);
        }
        foreach (ParticleSystem particle in list_Pooled_Particles)
        {
            particle.Stop();
        }
    }
    public void Pause_State()
    {
        foreach(GameObject bot in list_Pooled_Bots)
        {
            bot.GetComponent<Bot>().Paused();
        }
    }
    public void Resume_State()
    {
        foreach(GameObject bot in list_Pooled_Bots)
        {
            bot.GetComponent<Bot>().Resume();
        }
    }
}
