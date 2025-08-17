using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mediumBullet : weaponManager
{
    
    public override void establishStats()
    {
            shootingSpeed = 0.1f;
            projectileSpeed = 5f;
            projectileSize = 0.1f;
            projectileDamage = 1;
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

