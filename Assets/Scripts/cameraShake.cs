using System.Collections;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake Instance;
    rigCamera rC;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        rC = GetComponent<rigCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shakeStart(float range,float time)
    {
        StopAllCoroutines();
        StartCoroutine(Shake(range,time));
    }

    public IEnumerator Shake(float range, float time)
    {
        rC.enabled = false;

        Vector3 prePos = transform.position;
        while(time>=0)
        {
            time -= Time.deltaTime;
            rC.enabled = false;
            if (time <=0)
            {
                break;
            }

            Vector3 Pos = transform.position;
            Pos.x += Random.Range(-range,range);
            Pos.y += Random.Range(-range, range);
            transform.position = Pos;
            yield return null;
            rC.enabled = true;
        }

        transform.position = prePos;
        rC.enabled = true;
    }
}
