using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 initialPos;
    public bool hasLanded;
    public bool hasThrown;
    public int diceValue;
    public DiceSide[] dsObjects;
    public Vector2 torqueValue;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        initialPos = transform.position;
        dsObjects = GetComponentsInChildren<DiceSide>();
        rb.useGravity = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
            RollDice();

        if (rb.IsSleeping())
            CheckDiceState();
    }

    private void CheckDiceState()
    {
        if (!hasLanded && hasThrown)
        {
            hasLanded = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            return;
        }

        if (hasLanded && diceValue == 0)
        {
            RollAgain();
            return;
        }
    }

    private void RollDice()
    {
        if (hasThrown && hasLanded)
        {
            ResetDice();
            return;
        }
        Roll();
    }

    private void RollAgain()
    {
        ResetDice();
        Roll();
    }

    private void Roll()
    {
        hasThrown = true;
        rb.useGravity = true;
        rb.AddTorque(RandomTorqueAmount(torqueValue),
            RandomTorqueAmount(torqueValue), RandomTorqueAmount(torqueValue));
    }

    private void ResetDice()
    {
        transform.position = initialPos;
        hasThrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    private float RandomTorqueAmount(Vector2 torque)
    {
        return Random.Range(torque.x, torque.y);
    }
}