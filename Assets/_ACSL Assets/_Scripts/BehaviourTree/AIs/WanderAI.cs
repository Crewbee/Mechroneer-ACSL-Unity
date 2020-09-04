using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAI : BehaviorTree
{
    [Header("WanderAI Key Values")]
    public float Speed = 5.0f;
    public float TurnSpeed = 2.0f;
    public float Accuracy = 1.5f;
    public float TimeToWait = 1.5f;
    public float JumpStrength = 5.0f;

    public GameObject TargetObject;
    

    //Number of idle actions performable by the dog.
    public int NumOfIdleActions = 3;

    void Start()
    {

        // Create Nodes.
        Selector TreeRoot = new Selector();
        Selector ActionRoot = new Selector();

        //Wanders
        Sequence Wander = new Sequence();
        //Goes into sitting animation, stands back up after a delay and returns to Wander
        Sequence Sit = new Sequence();
        //Goes from a sitting animation into a lying down animation, reverses after a delay and returns to Wander
        Sequence LieDown = new Sequence();
        //Plays bark animation with sound and returns to Wander
        //Sequence Bark = new Sequence();

        Bark PlayBark = new Bark();
        GetRandomNum GetNum = new GetRandomNum();
        MoveToRandom MoveToRNG = new MoveToRandom();
        MoveToTarget MoveToPoint = new MoveToTarget();
        JumpToRandom JumpToRNG = new JumpToRandom();

        Wait Delay = new Wait();

        //Random Idle Action
        CheckKeyDeco BarkDeco = new CheckKeyDeco(PlayBark, 2);
        CheckKeyDeco MovePointDeco = new CheckKeyDeco(MoveToPoint, 1);
        CheckKeyDeco JumpRNGDeco = new CheckKeyDeco(JumpToRNG, 0);


        // Create blackboard keys and initialize them with values.
        SetValue("Speed", Speed);
        SetValue("TurnSpeed", TurnSpeed);
        SetValue("Accuracy", Accuracy);
        SetValue("JumpStrength", JumpStrength);
        SetValue("Target", TargetObject.transform.position);

        SetValue("NumOfActions", NumOfIdleActions);

        // Set node parameters - connect them to the blackboard.
        //SetValue("TimeToPause", TimeToWait);
        //Delay.TimeToWaitKey = "TimeToPause";

        // Connect Nodes.
        ActionRoot.children.Add(BarkDeco);
        ActionRoot.children.Add(JumpRNGDeco);
        ActionRoot.children.Add(MovePointDeco);
        Wander.children.Add(GetNum);
        Wander.children.Add(MoveToRNG);
        Wander.children.Add(ActionRoot);
        Wander.children.Add(Delay);
        TreeRoot.children.Add(Wander);

        Wander.tree = this;
        TreeRoot.tree = this;
        MoveToPoint.tree = this;
        MoveToRNG.tree = this;
        Delay.tree = this;

        root = TreeRoot;
    }

    // Used to change values during runtime via inspector.
    public override void Update()
    {   
        SetValue("Speed", Speed);
        SetValue("TurnSpeed", TurnSpeed);
        SetValue("Accuracy", Accuracy);
        SetValue("TimeToPause", TimeToWait);
        SetValue("JumpStrength", JumpStrength);
        SetValue("Target", TargetObject.transform.position);
        base.Update();
    }
}
