using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    public float maxHP = 10f;
    public float currentHP = 10f;

    protected bool isDead = false;

    private bool justGotDamage = false;
    public bool JustGotDamage { get => justGotDamage; set => justGotDamage = value; }

    private string attackAnimation;
    public string AttackAnimation{ get => attackAnimation; }


    public void Deduct(float amount, string attackAnim)
    {
        this.currentHP -= amount;
        Debug.Log("deduct HP to: " + this.currentHP);

        justGotDamage = true;
        this.attackAnimation = attackAnim;
        this.IsDead();
    }

    public bool IsDead()
    {
        return this.isDead = this.currentHP <= 0;
    }

}