using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boomerang : Bullet
{
    public float curveStrength; 
    public override void Update()
    {
        transform.Rotate(0, 10, 0);
        StartCoroutine(Curve(target_position));
        if(Vector3.Distance(transform.position, target_position) < 0.1f)
        {
            gameObject.SetActive(false);
        }
        existenceTime -= Time.deltaTime;
        if (existenceTime <= 0.1f)
        {
            gameObject.SetActive(false);
            existenceTime = 2f;
        }
    }
    IEnumerator Curve(Vector3 position)
    {
        //bezier curve move along the x-z plane
        Vector3[] positions = new Vector3[3];
        positions[0] = transform.position;
        positions[1] = transform.position + transform.forward * curveStrength;
        positions[2] = position;
        float time = 0f;
        while(time < 1f)
        {
            time += Time.deltaTime * speed;
            transform.position = BezierCurve(time, positions);
            yield return null;
            if (Vector3.Distance(transform.position, position) < 0.1f)
            {
                break;
            }
        }
        
    }
    Vector3 BezierCurve(float t, Vector3[] positions)
    {
        Vector3 a = Vector3.Lerp(positions[0], positions[1], t);
        Vector3 b = Vector3.Lerp(positions[1], positions[2], t);
        return Vector3.Lerp(a, b, t);
    }
}
