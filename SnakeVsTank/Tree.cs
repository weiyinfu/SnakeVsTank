using System;
using System.Drawing;
class Tree:OneGridSprite
{
    public Snake owner; 
    int blood = 2;
    public Tree(int x,int y,Snake owner)
    {
        this.x = x;
        this.y = y;
        this.owner = owner;
        Global.map[x, y] = this; 
        Global.draw(Resource.getTreeByName(owner.name), x, y);
        timer.Interval =2000;
        timer.Tick += tick;
        timer.Start();
    }
    void tick(object o,EventArgs e)
    { 
        int xx = Global.random.Next(x - 2, x + 3);
        int yy = Global.random.Next(y - 2, y + 3);
        if (Global.canGo(xx, yy)&&Global.random.Next(0,100)<10)
        {
            new Food(xx, yy, owner);
        }
    }
    public override void bulleted()
    {
        blood--;
        if (blood == 0)
        {
            die();
        }
    }
}