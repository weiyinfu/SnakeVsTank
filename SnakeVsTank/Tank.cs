using System; 
using System.Drawing;
class Tank:OneGridSprite
{ 
    //四个方向的坦克
    static Bitmap[] tanks = new Bitmap[4];
    //坦克死去动画
    static Bitmap[] tankDied;
    
    static Tank()
    {
        for (int i = 0; i < 4; i++)
        {
            tanks[i] = new Bitmap(Resource.getTank());
            tanks[i].MakeTransparent(Color.White);
        }
        //tanks[0].RotateFlip(RotateFlipType.Rotate180FlipNone);
        tanks[1].RotateFlip(RotateFlipType.Rotate180FlipNone);
        tanks[2].RotateFlip(RotateFlipType.Rotate270FlipNone); 
        tanks[3].RotateFlip(RotateFlipType.Rotate90FlipNone);
        tankDied = new Bitmap[Resource.tankdied.Length];
        for (int i = 0; i < Resource.tankdied.Length; i++)
        {
            tankDied[i] =Resource.tankdied[i];
            tankDied[i].MakeTransparent(Color.White);
        }
    }
    int speed;//坦克速度
    int blood=1;//坦克血量
    int direction;//坦克方向
    public Tank(int x,int y,int speed)
    {
        this.x = x;
        this.y = y;
        this.speed = speed;
        this.direction=Global.random.Next(0,4);
        Global.draw(tanks[direction], x, y);
        Global.map [x,y]= this;
        timer.Interval = 1000 - speed;
        timer.Tick += tick;
        timer.Start();
    } 
    void tick(object o, EventArgs e)
    {
        int xx = x + Global.dir[direction, 0];
        int yy = y + Global.dir[direction, 1];
        if (!Global.canGo(xx,yy))
        {
            bool hasRoad = false;
            #region 判断是否有路可走
            for (int i = 0; i < 4; i++)
            {
                xx = x + Global.dir[i, 0];
                yy = y + Global.dir[i, 1];
                if (Global.canGo(xx, yy))
                { 
                     hasRoad = true;
                     break; 
                }
            }
            #endregion
            if (!hasRoad)//无路可走，坦克憋死
            {
                new Animator(tankDied,x, y);
                die();
            }
            else//有路可走，随机变向
            {
                direction = Global.random.Next(0, 4);
                Global.clear(x, y);
                Global.draw(tanks[direction], x, y);
                Global.map[x, y] = this;
            }
        }
        else//继续往前走
        {
            go(xx, yy);
        }
    }
    void go(int xx, int yy)
    {
        Global.clear(x, y);
        x = xx;
        y = yy;
        Global.draw(Tank.tanks[direction], x, y);
        Global.map[x, y] = this;
        if (seeEnermy()) shoot();
    }
    //判断有没有发现敌人
    bool seeEnermy()
    {
        int xx, yy;
        for (xx = x, yy = y; Global.valid(xx, yy); xx += Global.dir[direction, 0], yy += Global.dir[direction, 1])
        { 
            if (Global.map[xx, yy] == null) continue;
            if (!(Global.map[xx, yy] is Tank) && !(Global.map[xx, yy] is TankSource))
            {
                return true;
            }
        }
        return false;
    }
    void shoot()
    {
        int xx = x + Global.dir[direction, 0];
        int yy = y + Global.dir[direction, 1];
        if (Global.canGo(xx, yy)&&Global.random.Next(0,100)<20)//不要总是发炮
        new Bullet(xx, yy, direction);
    }
    public override  void bulleted()
    {
        blood--;
        if (blood == 0)
        {
            die(); 
        }
    } 
}
