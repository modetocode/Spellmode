using System.Collections.Generic;

/// <summary>
/// Resonsible for ticking all of the bullets that are currenly in the scene. Also when the run finishes it cleans all the remaining bullets.
/// </summary>
public class BulletManager : ITickable {

    private IList<Bullet> bullets;

    public BulletManager() {
        this.bullets = new List<Bullet>();
    }

    public void AddBullet(Bullet bullet) {
        //TODO arg check
        bullet.Destroyed += RemoveBullet;
        this.bullets.Add(bullet);
    }

    private void RemoveBullet(Bullet bullet) {
        bullet.Destroyed -= RemoveBullet;
        this.bullets.Remove(bullet);
    }

    public void Tick(float deltaTime) {
        for (int i = 0; i < bullets.Count; i++) {
            this.bullets[i].Tick(deltaTime);
        }
    }

    public void OnTickingFinished() {
        for (int i = 0; i < bullets.Count; i++) {
            this.bullets[i].Destroy();
        }

        this.bullets = new List<Bullet>();
    }
}
