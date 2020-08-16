using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string AIState = "Idle";
    public float aiSenseRadius;
    public Vector3 targetPosition;
    public float health;
    public float healthCutoff;
    public float moveSpeed;
    public float healingRate;
    public float maxHealth;
    public float hearingDistance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AIState == "Idle")
        {
            Idle();
            //Check for transition
            if (Vector3.Distance(transform.position, targetPosition) < aiSenseRadius)
            {
                ChangeState("Seek");
            }
        }
        else if (AIState == "Seek")
        {
            // Do the State behavior
            Seek();
            // Check for transtions
            if (health < healthCutoff)
            {
                ChangeState("Rest");
            }
            else if (Vector3.Distance(transform.position, targetPosition) >= aiSenseRadius)
            {
                ChangeState("Idle");
            }
        }
        else if (AIState == "Rest")
        {
            Rest();
            if (health >= healthCutoff)
            {
                ChangeState("Idle");
            }
        }
        else
        {
            Debug.Log("AI state has not been found");
        }
    }
    void Idle()
    {

    }
    void Seek()
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        transform.position += vectorToTarget.normalized * moveSpeed * Time.deltaTime;

    }
    void Rest()
    {
        health += healingRate * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }
    public void ChangeState(string newState)
    {
        AIState = newState;
    }
    private bool CanHear(GameObject target)
    {
        // Get the target's noise maker
        NoiseMaker targetNoiseMaker = target.GetComponent<NoiseMaker>();
        //if they don't have a noise maker. we can't hear them.
        if (targetNoiseMaker == null)
        {
            return false;
        }
        //If the distance between us and the target is less than the sum of the noise distance and hearing distance, we can hear i
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if((targetNoiseMaker.volumeDistance + hearingDistance) > distanceToTarget) { return false; }
        return false;
    }
}
