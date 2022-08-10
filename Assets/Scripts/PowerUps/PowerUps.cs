using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PowerUps : NetworkBehaviour
{

    [Header ("Properties")]
    public float delay;
    public float normalMass;
    public float powerMass;

    //public MeshRenderer meshRenderer;
    public GameObject powerUp;

    [Networked] private TickTimer timer { get; set; }
    [Networked] private TickTimer timer2 { get; set; }

    GameObject playerObj;


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.GetComponentInParent<NetworkPlayer>() != null)
        {
            playerObj = other.gameObject;
            timer = TickTimer.CreateFromSeconds(Runner, delay);
            timer2 = TickTimer.CreateFromSeconds(Runner, delay * 2);
            powerUp.SetActive(false);
            //meshRenderer.enabled = false;
            playerObj.GetComponentInParent<Rigidbody>().mass = powerMass;
            playerObj.transform.parent.localScale = new Vector3(1.6f, 1.6f, 1.6f);
            powerUp.GetComponentInParent<Collider>().enabled = false;

        }
    }

    public override void FixedUpdateNetwork()
    {
        if (timer.Expired(Runner))
        {
            playerObj.transform.parent.localScale = new Vector3(1, 1, 1);
            playerObj.GetComponentInParent<Rigidbody>().mass = normalMass;
        }

        if (timer2.Expired(Runner))
        {
            powerUp.SetActive(true);
            powerUp.GetComponentInParent<Collider>().enabled = true;
            //meshRenderer.enabled = true;
        }
    }

    /*public IEnumerator PowerUpCD(float delay, GameObject obj)
    {

        meshRenderer.enabled = false;
        obj.GetComponentInParent<Rigidbody>().mass = powerMass;
        obj.transform.parent.localScale += new Vector3(0.6f, 0.6f, 0.6f);

        yield return new WaitForSeconds(delay);

        obj.transform.parent.localScale -= new Vector3(0.6f, 0.6f, 0.6f);
        obj.GetComponentInParent<Rigidbody>().mass = normalMass;

        yield return new WaitForSeconds(delay * 2);
        
        meshRenderer.enabled = true;
    }*/

    /*[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_PowerUp(RpcInfo info = default)
    {
        StartCoroutine(PowerUpCD(delay, playerObj));
    }*/
}
