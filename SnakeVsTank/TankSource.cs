using System; 
using System.Drawing;
class TankSource:OneGridSprite
{
    static int size=8;
    int blood = 5;//血量
    public TankSource(int x, int y)
    {
        this.x = x;
        this.y = y;
        Global.map[x, y] = this;
        Bitmap bit=Resource.getTanksource();
        bit.MakeTransparent(Color.White);
        Global.draw(bit,x,y);
        timer.Interval = 4000;
        timer.Tick += tick;
        timer.Start();
    }
    void tick(object o, EventArgs e)
    { 
        int xx = Global.random.Next(x-2,x+3);
        int yy = Global.random.Next(y - 2, y + 3);
        if (Global.canGo(xx,yy)&&Global.random.Next(0,100)<10)
        {
            new Tank(xx,yy, 200);
        } 
    }
    override public void bulleted()
    {
        blood--;
        if (blood == 0)
        {
            die();
            size--;
            if (size == 0)
                Global.gameOver("坦克之源已经被全部消灭，我军取得全面胜利");
        }
    }
}