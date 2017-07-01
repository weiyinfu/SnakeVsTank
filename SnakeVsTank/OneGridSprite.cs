using System;
using System.Windows.Forms;
using System.Drawing;
//一切单格物体的基类，如果没有timer，就不开启timer就可以了
abstract class OneGridSprite {
    protected int x, y;
    protected Timer timer = new Timer();
    public void die() {
        timer.Dispose();
        Global.clear(x, y);
    }
    public void pause() {
        timer.Stop();
    }
    public void continueGame() {
        timer.Start();
    }
    public abstract void bulleted();
}