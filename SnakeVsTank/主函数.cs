using System;
using System.Windows.Forms;
using System.Drawing;
class Window : Form {
    public Window() {
        ClientSize = new Size(720, 720);
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        Location = new Point(100, 0);
        KeyDown += key;
        Paint += paint;
    }
    void paint(object o, PaintEventArgs e) {
        #region 第一部分，初始化   ground
        for (int x = 0; x < Global.width; x++)
            for (int y = 0; y < Global.height; y++) {
                Global.ground[x, y] = new Bitmap(Resource.getGround());
            }
        #endregion
        #region 第二部分，画出绿草地
        Bitmap bit = new Bitmap(720, 720);
        for (int x = 0; x < 18; x++) {
            for (int y = 0; y < 18; y++) {
                Graphics.FromImage(bit).DrawImage(Global.ground[x, y], y * 40, x * 40, 40, 40);
            }
        }
        e.Graphics.DrawImage(bit, 0, 0);
        #endregion
        Global.gameStart();
    }
    void key(object o, KeyEventArgs e) {
        switch (e.KeyCode) {
            case Keys.Up: Global.s1.key(0); break;
            case Keys.Down: Global.s1.key(1); break;
            case Keys.Left: Global.s1.key(2); break;
            case Keys.Right: Global.s1.key(3); break;
            case Keys.G: Global.s1.newTree(); break;
            case Keys.H: Global.s1.newHouse(); break;

            case Keys.W: Global.s2.key(0); break;
            case Keys.S: Global.s2.key(1); break;
            case Keys.A: Global.s2.key(2); break;
            case Keys.D: Global.s2.key(3); break;
            case Keys.N: Global.s2.newTree(); break;
            case Keys.M: Global.s2.newHouse(); break;

            case Keys.Escape: Global.pause(); break;
            case Keys.Enter: Global.continueGame(); break;
        }
    }
}
class Global {
    public const int width = 18, height = 18;
    public static Window f = new Window();
    public static object[,] map = new object[width, height];
    public static Bitmap[,] ground = new Bitmap[width, height];
    static public Snake s1;
    static public Snake s2;
    static void Main() {
        Application.Run(f);
    }
    public static void pause() {
        s1.pause();
        //s2.die();
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++) {
                if (map[i, j] is OneGridSprite) (map[i, j] as OneGridSprite).pause();
            }
    }
    public static void continueGame() {
        s1.continueGame();
        //s2.die();
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++) {
                if (map[i, j] is OneGridSprite) (map[i, j] as OneGridSprite).continueGame();
            }
    }
    #region 小工具，各种小函数，方便使用
    public static void clear(int x, int y) {
        map[x, y] = null;
        f.Invoke(new Action(delegate {
            f.CreateGraphics().DrawImage(ground[x, y], y * 40, x * 40, 40, 40);
        }));
    }
    public static void draw(Bitmap bitmap, int x, int y) {
        f.Invoke(new Action(delegate {
            f.CreateGraphics().DrawImage(bitmap, y * 40, x * 40, 40, 40);
        }));
    }
    //下、上、右、左
    static public int[,] dir = new int[4, 2] { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };
    static public bool valid(int x, int y) {
        return x >= 0 && y >= 0 && x < 18 && y < 18;
    }
    static public bool canGo(int x, int y) {
        if (valid(x, y)) {
            if (map[x, y] == null) return true;
            else return false;
        }
        return false;
    }
    public static Random random = new Random();
    #endregion
    public static void gameStart() {
        s1 = new Snake(0, 0, "weidiao");
        // s2 = new Snake(17, 17, "jiege");
        new TankSource(8, 9);
        new TankSource(8, 8);
        new TankSource(9, 9);
        new TankSource(9, 8);
        new TankSource(17, 0);
        new TankSource(0, 17);
        new TankSource(12, 5);
        new TankSource(5, 12);
    }
    public static void gameOver(string result) {
        s1.die();
        //s2.die();
        for (int i = 0; i < 18; i++)
            for (int j = 0; j < 18; j++) {
                if (map[i, j] is OneGridSprite) (map[i, j] as OneGridSprite).die();
            }
        MessageBox.Show(result, "对局结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //Application.Exit();
        gameStart();
    }
}

