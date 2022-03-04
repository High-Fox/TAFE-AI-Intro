using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public enum State
    {
        Attack,
        Defend,
        Flee,
        PickBerries
    }

    public AIMovement movement;
    public State currentState;

    private void Start()
    {
        movement = GetComponent<AIMovement>();
        NextState();
    }

    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defend:
                StartCoroutine(DefendState());
                break;
            case State.Flee:
                StartCoroutine(FleeState());
                break;
            case State.PickBerries:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    private IEnumerator AttackState()
    {
        Debug.Log("Start Attack");
        while (currentState == State.Attack)
        {
            if (movement.ShouldAttack())
            {
                movement.MoveTowards(movement.player);
            }
            else
            {
                currentState = State.PickBerries;
                movement.SetPointToNearest();
            }

            yield return null;
        }
        Debug.Log("End Attack");
        NextState();
    }

    private IEnumerator DefendState()
    {
        Debug.Log("Start Defend");
        while (currentState == State.Defend)
        {
            for (int i = 0; i < 4; i++)
            {
                movement.MakeRandomPoint();
                yield return new WaitForSeconds(1);
            }
            movement.pointIndex = 0;
            currentState = State.PickBerries;

            yield return null;
        }
        Debug.Log("End Defend");
        NextState();
    }

    private IEnumerator FleeState()
    {
        Debug.Log("Start Flee");
        while (currentState == State.Flee)
        {
            yield return null;
        }
        Debug.Log("End Flee");
        NextState();
    }

    private IEnumerator BerryPickingState()
    {
        Debug.Log("Start PickBerries");
        while (currentState == State.PickBerries)
        {
            if (movement.ShouldAttack())
            {
                currentState = State.Attack;
            }

            if (movement.MoveToPoints())
            {
                yield return new WaitForSeconds(2);
                movement.DestroyCurrentPoint();
                if (movement.points.Count == 0)
                {
                    currentState = State.Defend;
                }
            }

            yield return null;
        }
        Debug.Log("End PickBerries");
        NextState();
    }
}
