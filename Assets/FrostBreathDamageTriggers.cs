using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBreathDamageTriggers : MonoBehaviour
{

    protected ParticleSystem ps;
    protected void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    // Start is called before the first frame update
    protected void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, enter);
        int numIn = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
        int numOut = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Outside, enter);
        Debug.Log(numEnter + numExit + numIn + numOut);
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            Collider[] cols = Physics.OverlapSphere(p.position, 1);
            Debug.Log("hit");
            foreach (Collider col in cols)
            {
                if (col.tag == "Monster")
                {
                    int val = Mathf.CeilToInt(0);
                    col.GetComponent<AIController>().takeDamage(val);
                }
            }
        }
    }
}
