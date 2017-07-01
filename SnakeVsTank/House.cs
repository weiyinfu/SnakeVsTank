using System;
using System.Drawing;
class House : OneGridSprite {
    object owner;
    public int blood = 5;//房子可以承受5颗子弹
    public House(int x, int y, Snake owner) {
        this.owner = owner;
        this.x = x;
        this.y = y;
        Global.map[x, y] = this;
        Bitmap bit = Resource.getHouseByName(owner.name);
        bit.MakeTransparent(Color.White);
        Global.draw(bit, x, y);
    }
    public override void bulleted() {
        blood--;
        if (blood == 0) {
            die();
        }
    }
}