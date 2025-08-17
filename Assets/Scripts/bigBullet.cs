using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigBullet : weaponManager
{
    
    public override void establishStats()
    {
            shootingSpeed = 5f;
            projectileSpeed = 5f;
            projectileSize = 2f;
            projectileDamage = 20;
    }

    void start()
    {
        establishStats();
    }
    
    void update()
    {
        shootWeapon();
    }


}

