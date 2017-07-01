using System; 
using System.Drawing;
class Bullet:OneGridSprite
{ 
    public static Bitmap[] bulletShot =Resource.bullet;
    static Bitmap bullet;
    //static Bullet 用于初始化子弹的动态位图
    static Bullet()
    {
        bullet=Resource.getBullet(0);
        bullet.MakeTransparent(Color.White); 
    }
    int speed = 700;
    int direction; 
    public Bullet(int x, int y, int dir)
    {
        this.x = x;
        this.y = y;
        this.direction = dir;
        Global.draw(bullet, x, y);
        Global.map[x, y] = this;
        timer.Interval = 1000 - speed;
        timer.Tick += tick;
        timer.Start();
    }
    void tick(object o, EventArgs e)
    {
        int xx = x + Global.dir[direction, 0];
        int yy = y + Global.dir[direction, 1];
        if (!Global.valid(xx, yy))
        {
            die();
            return;
        }
        if (Global.map[xx, yy] != null)
        {
            if (Global.map[xx, yy] is Snake)
            {
                bulleted();
                die();
                (Global.map[xx, yy] as Snake).bulleted();
                return;
            }
            if (Global.map[xx, yy] is OneGridSprite)
            {
                bulleted();
                die();
                (Global.map[xx, yy] as OneGridSprite).bulleted(); 
                return;
            }
        }
        Global.clear(x, y);
        x = xx;
        y = yy; 
        Global.map[x, y] = this;
        Global.draw(bullet, x, y);
    }
    public override void bulleted()
    {
        new Animator(bulletShot,x, y); 
    } 
}