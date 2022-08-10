using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotRandom : MonoBehaviour
{
    public Shot [] canones;
    public float LastShotTime = 0;
    public float intervalo = 1;

    // Start is called before the first frame update
    private void Awake() 
    {
        canones = GetComponentsInChildren<Shot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeSinceLevelLoad - LastShotTime > intervalo)
        {
            canones[Random.Range(0, canones.Length)].Spawn();
            LastShotTime = Time.timeSinceLevelLoad;
        }
    }
}
