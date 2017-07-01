using System; 
using System.Drawing;
class LifeSource:OneGridSprite
{ 
    int blood = 3;
    Snake owner;
    public LifeSource(int x,int y ,Snake owner)
    {
        this.x = x;
        this.y = y;
        this.owner = owner;
        Global.map[x, y] = this;
        Bitmap bit = Resource.getLifeSourceByname(owner.name);
        bit.MakeTransparent(Color.Black);
        Global.draw(bit, x, y);
    } 
    public override void bulleted()
    {
        blood--;
        if (blood == 0)
        {
            Global.gameOver(owner.name+"   的生命之源  died");
        }
    }
}