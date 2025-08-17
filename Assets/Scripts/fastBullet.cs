using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastBullet : weaponManager
{
    
    public override void establishStats()
    {
            shootingSpeed = 0.5f;
            projectileSpeed = 30f;
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
