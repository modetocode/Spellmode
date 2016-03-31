using UnityEngine;

public class BulletComponent : MonoBehaviour {

    public Bullet Bullet { get; private set; }

    public void Initialize(Bullet bullet) {
        this.Bullet = bullet;
        this.Bullet.Destroyed += Destroy;
        //TODO calculate the direction of the bullet
        //Calculate the rotation angle in order to rotate the sprite so it represents the direction of the bullet
        //var angle = Mathf.Atan2(this.Bullet.Direction.y, this.Bullet.Direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Destroy(Bullet obj) {
        this.Bullet.Destroyed -= Destroy;
        Destroy(this.gameObject);
    }

    void Update() {
        if (this.Bullet == null) {
            return;
        }

        this.gameObject.transform.position = this.Bullet.CurrentPosition;
        //TODO rotate the sprite to it is facing the right direction
        //Vector3 lookPosition = this.Bullet.CurrentPosition;
        //lookPosition += new Vector3(this.Bullet.Direction.y, 0f, this.Bullet.Direction.x);
        //this.gameObject.transform.LookAt(new Vector3(this.Bullet.CurrentPosition.x, this.Bullet.CurrentPosition.y, );
    }
}