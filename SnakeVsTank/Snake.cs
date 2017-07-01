using System;
using System.Drawing;
using System.Windows.Forms;
class Snake {
    public static Bitmap bodyBmp = Resource.snakebody;
    public static Bitmap[] headBmp;
    public static Bitmap[] snakeDied;
    static Snake() {
        #region 身体图片
        bodyBmp.MakeTransparent(Color.White);
        #endregion
        #region 头部图片
        headBmp = new Bitmap[4];//4个方向
        for (int i = 0; i < 4; i++) {
            headBmp[i] = new Bitmap(Resource.snakehead);
            headBmp[i].MakeTransparent(Color.White);
        }
        headBmp[1].RotateFlip(RotateFlipType.RotateNoneFlipY);
        headBmp[2].RotateFlip(RotateFlipType.Rotate90FlipNone);
        headBmp[3].RotateFlip(RotateFlipType.Rotate90FlipNone);
        headBmp[2].RotateFlip(RotateFlipType.Rotate180FlipNone);
        #endregion
        #region 死亡图片
        snakeDied = new Bitmap[Resource.snakedied.Length];
        for (int i = 0; i < Resource.snakedied.Length; i++) {
            snakeDied[i] = Resource.snakedied[i];
            snakeDied[i].MakeTransparent(Color.White);
        }
        #endregion
    }
    int size = 1;//当前长度
    int[,] body = new int[324, 2];//身体坐标
    int direction;//当前运动方向
    int speed = 500;//速度
    public string name;//姓名
    Timer timer = new Timer();
    LifeSource life;
    public Snake(int x, int y, string name) {
        this.name = name;
        newLife();
        if (name == "weidiao") weidiao();
        else jiege();
    }
    void jiege() {
        life = new LifeSource(17, 17, this);
        new Tree(17, 13, this);
        new Tree(13, 17, this);
        new House(17, 10, this);
        new House(10, 17, this);
        new House(14, 14, this);
        new House(13, 13, this);
        new Tree(15, 15, this);
    }
    void weidiao() {
        life = new LifeSource(0, 0, this);
        new Tree(0, 3, this);
        new Tree(3, 0, this);
        new House(0, 6, this);
        new House(6, 0, this);
        new House(2, 2, this);
        new House(3, 3, this);
        new Tree(2, 2, this);
    }
    public void newLife() {
        int x, y;
        if (name == "weidiao") {
            x = y = 0;
        } else {
            x = y = 17;
        }
        int xx = 0, yy = 0;
        int i;
        for (i = 0; i < 4; i++) {
            xx = x + Global.dir[i, 0];
            yy = y + Global.dir[i, 1];
            if (Global.canGo(xx, yy)) break;
        }
        if (i == 4) {
            Global.gameOver(name + " died");
            return;
        } else {
            size = 1;
            body[0, 0] = xx;
            body[0, 1] = yy;
            direction = Global.random.Next(0, 4);
            Global.map[xx, yy] = this;
            Global.draw(headBmp[direction], xx, yy);
            timer = new Timer();
            timer.Interval = 1000 - speed;
            timer.Tick += tick;
            timer.Start();
        }
    }
    void move(int hx, int hy) {
        for (int i = size - 1; i > 0; i--) {
            body[i, 0] = body[i - 1, 0];
            body[i, 1] = body[i - 1, 1];
        }
        body[0, 0] = hx;
        body[0, 1] = hy;
        Global.map[hx, hy] = this;
    }
    void eatFood(int hx, int hy) {
        body[size, 0] = body[size - 1, 0];
        body[size, 1] = body[size - 1, 1];
        move(hx, hy);
        size++;
    }
    void clear() {
        for (int i = 0; i < size; i++)
            Global.clear(body[i, 0], body[i, 1]);
    }
    void draw() {
        Global.draw(headBmp[direction], body[0, 0], body[0, 1]);
        Global.map[body[0, 0], body[0, 1]] = this;
        for (int i = 1; i < size; i++) {
            Global.draw(bodyBmp, body[i, 0], body[i, 1]);
            Global.map[body[i, 0], body[i, 1]] = this;
        }
    }
    void tick(object o, EventArgs e) {
        int hx = body[0, 0] + Global.dir[direction, 0];
        int hy = body[0, 1] + Global.dir[direction, 1];
        if (!Global.valid(hx, hy))
            return;
        if (Global.map[hx, hy] == null) {
            clear();
            move(hx, hy);
        }
        if (Global.map[hx, hy] is Food && (Global.map[hx, hy] as Food).owner == this) {
            clear();
            eatFood(hx, hy);
        }
        draw();
    }
    public void key(int which) {
        lock (this) {
            if (direction + which == 1 || direction + which == 5) {
                return;
            }
            if (direction == which) {
                newBullet();
                return;
            }
            direction = which;
            Global.draw(Global.ground[body[0, 0], body[0, 1]], body[0, 0], body[0, 1]);
            Global.draw(headBmp[direction], body[0, 0], body[0, 1]);
        }
    }
    public void bulleted() {
        lock (this) {
            if (size > 1) {
                size--;
                Global.clear(body[size, 0], body[size, 1]);
            } else {
                die();
                life.bulleted();
                newLife();
            }
        }
    }
    public void die() {
        new Animator(snakeDied, body[0, 0], body[0, 1]);
        timer.Dispose();
        for (int i = 0; i < size; i++) {
            Global.clear(body[i, 0], body[i, 1]);
        }
        size = 0;
    }
    void newBullet() {
        if (size < 2) return;
        int x = body[0, 0] + Global.dir[direction, 0];
        int y = body[0, 1] + Global.dir[direction, 1];
        if (!Global.canGo(x, y)) return;
        else new Bullet(x, y, direction);
        size--;
        Global.clear(body[size, 0], body[size, 1]);
    }
    public void newTree() {
        lock (this) {
            if (size < 2) return;
            int x = body[0, 0] + Global.dir[direction, 0];
            int y = body[0, 1] + Global.dir[direction, 1];
            if (!Global.canGo(x, y)) return;
            new Tree(x, y, this);
            size--;
            Global.clear(body[size, 0], body[size, 1]);
        }
    }
    public void newHouse() {
        lock (this) {
            if (size < 2) return;
            int x = body[0, 0] + Global.dir[direction, 0];
            int y = body[0, 1] + Global.dir[direction, 1];
            if (!Global.canGo(x, y)) return;
            new House(x, y, this);
            size--;
            Global.clear(body[size, 0], body[size, 1]);
        }
    }
    public void pause() {
        timer.Stop();
    }
    public void continueGame() {
        timer.Start();
    }
}