using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State {


    public PatrolState(StateController stateController) : base(stateController) { }
    float timer;
    float timeLimit = 3;
    public override void CheckTransitions()
    {
        if (stateController.CheckIfInRange("Player"))
        {
            stateController.SetState(new ChaseState(stateController));
        }
        if (timer > timeLimit)
        {
            stateController.SetState(new MakeNavPoints(stateController));
        }
    }
    public override void Act()
    {
        timer += Time.deltaTime;
        if(stateController.destination == null || stateController.ai.DestinationReached())
        {
            stateController.destination = stateController.GetNextNavPoint();
            stateController.ai.SetTarget(stateController.destination);
        }
    }
    public override void OnStateEnter()
    {
        timer = 0;
        stateController.destination = stateController.GetNextNavPoint();
        if (stateController.ai.agent != null)
        {
            stateController.ai.agent.speed = 1f;
        }
        stateController.ai.SetTarget(stateController.destination);
        stateController.ChangeColor(Color.blue);
    }

}
