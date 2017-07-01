using System;
using System.Windows.Forms;
using System.Drawing;
class Food:OneGridSprite
{ 
    public object owner;
    public Food(int x,int y,Snake owner)
    {
        this.x = x;
        this.y = y;
        this.owner = owner;
        Global.map[x, y] = this; 
        Bitmap bit=Resource.getFoodByname(owner.name);
        bit.MakeTransparent(Color.Black);
        Global.draw(bit, x, y);
    }
    override public void bulleted()
    {
        die();
    }
}