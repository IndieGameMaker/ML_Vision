using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class AgentVision : Agent
{
    private Transform tr;
    private Rigidbody rb;

    public float moveSpeed = 2.0f;
    public float turnSpeed = 300.0f;
    public float range = 50.0f;
    public StageManager stageManager;

    public override void Initialize()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector3.zero;
        
        float x = Random.Range(-range * 0.5f, range * 0.5f);
        float z = Random.Range(-range * 0.5f, range * 0.5f);
        tr.localPosition = new Vector3(x, 0.5f, z);
        tr.localRotation = Quaternion.Euler(0, Random.Range(0,360), 0);  
        
        stageManager.MakeStage();      
    }

    public override void CollectObservations(MLAgents.Sensors.VectorSensor sensor)
    {}

    public override void OnActionReceived(float[] vectorAction)
    {
        Vector3 dir = Vector3.zero;
        Vector3 rot = Vector3.zero;

        //전진 후진 처리
        switch ((int)vectorAction[0])
        {
            case 1: dir = transform.forward; break;
            case 2: dir = -transform.forward; break;
        }

        //좌우 회전 처리
        switch ((int)vectorAction[1])
        {
            case 1: rot = -transform.up; break;
            case 2: rot = transform.up; break;
        }

        rb.AddForce(dir * moveSpeed, ForceMode.VelocityChange);
        tr.Rotate(rot, Time.fixedDeltaTime * turnSpeed);      
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0f; //W, S (전/후)
        actionsOut[1] = 0f; //A, D (왼쪽/오른쪽 회전)

        if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[1] = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[1] = 2f;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("GOOD"))
        {
            SetReward(+1.0f);
            Destroy(coll.gameObject);
        }
        if (coll.collider.CompareTag("BAD"))
        {
            SetReward(-1.0f);
            Destroy(coll.gameObject);
            EndEpisode();
        }
    }

}
