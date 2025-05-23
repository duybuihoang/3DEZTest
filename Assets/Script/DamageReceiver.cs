using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        if (!this.IsDestroyed())
        {
            this.currentHP -= amount;
            if (transform.parent.tag == "Player")
                GameManager.Instance.UpdatePlayerSlider(currentHP / maxHP);

            justGotDamage = true;
            this.attackAnimation = attackAnim;
            this.IsDead();
        }
    }

    public bool IsDead()
    {
        return this.isDead = this.currentHP <= 0;
    }

}