using UnityEngine;

public interface ISpecialAttack
{
    void Activated(bool isActivated);

    void CalculateDamage();

    void AttackSpeedCalc();

}
