using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyForDisplay :Character 
{
    public GameObject hand;

    public override void WearHat(int index)
    {
        base.WearHat(index);

    }
    public void WearWeapon(int index)
    {
        Instantiate(Wardrobe.instance.Get_Weapons(index), hand.transform.position, Quaternion.identity);
    }
    public void TestOutWeapon()
    {
        ChangeAnim("attack");
    }
}
