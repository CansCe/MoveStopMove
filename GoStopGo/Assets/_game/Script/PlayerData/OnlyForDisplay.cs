using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyForDisplay :Character 
{
    public GameObject hand;
    string default_anim="idle";
    public int current_hat;
    public int current_weapon;
    public override void WearHat(int index)
    {
        ChangeAnim("Idle");
        base.WearHat(index);
        current_hat = index;
    }
    public void WearWeapon(int index)
    {
        Instantiate(Wardrobe.instance.Get_Weapons(index), hand.transform.position, Quaternion.identity);
        current_weapon = index;
    }
    public void TestOutWeapon()
    {
        ChangeAnim("attack");
        StartCoroutine(WaitForAtack());
        ChangeAnim(default_anim);
    }
    IEnumerator WaitForAtack()
    {
        yield return new WaitForSeconds(1);
    }
}
