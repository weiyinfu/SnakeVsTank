using System;
using System.Drawing;
using System.Windows.Forms;
//这个类只是画画，只改变显示，不改变数据
class Animator
{
    int x, y;
    int which = 0;
    Timer timer = new Timer();
    Bitmap[] file;
    public Animator(Bitmap[] file ,int x, int y)
    {
        this.x = x;
        this.y = y;
        this.file = file;
        Global.draw(file[0], x, y);
        timer.Interval = 400;
        timer.Tick += tick;
        timer.Start();
    }
    void tick(object o, EventArgs e)
    {
        which++;
        if (Global.map[x, y] != null || which == file.Length)
        {
            Global.draw(Global.ground[x, y], x, y);
            die();
            return;
        }
        Global.draw(Global.ground[x, y], x, y);
        Global.draw(file[which], x, y); 
    }
    void die()
    {
        timer.Dispose();
    }
}
