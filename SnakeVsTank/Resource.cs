using System.Drawing;
using System.Collections;
/**负责加载资源文件
 */
static class Resource {
    static Bitmap[] lifeSource = new Bitmap[2];
    static Bitmap[] tree = new Bitmap[2];
    static Bitmap[] house = new Bitmap[2];
    static Bitmap[] food = new Bitmap[2];
    static Bitmap[] tank = new Bitmap[2];
    public static Bitmap[] bullet = new Bitmap[7];
    public static Bitmap[] tanksource = new Bitmap[2];
    public static Bitmap snakebody;
    public static Bitmap snakehead;
    public static Bitmap[] snakedied = bullet;
    public static Bitmap[] ground = new Bitmap[3];
    public static Bitmap[] tankdied = bullet;
    static Resource() {
        lifeSource[0] = new Bitmap(Bitmap.FromFile("res\\lifesource0.jpg"));
        lifeSource[1] = new Bitmap(Bitmap.FromFile("res\\lifesource1.jpg"));
        tree[0] = new Bitmap(Bitmap.FromFile("res\\tree0.jpg"));
        tree[1] = new Bitmap(Bitmap.FromFile("res\\tree1.jpg"));
        house[0]=new Bitmap(Bitmap.FromFile("res\\house0.jpg"));
        house[1]=new Bitmap(Bitmap.FromFile("res\\house1.jpg"));
        food[0]=new Bitmap(Bitmap.FromFile("res\\food0.jpg"));
        food[1]=new Bitmap(Bitmap.FromFile("res\\food1.jpg"));
        tank[0]=new Bitmap(Bitmap.FromFile("res\\tank0.jpg"));
        tank[1]=new Bitmap(Bitmap.FromFile("res\\tank1.jpg"));
        for(int i=0;i<bullet.Length;i++){
            bullet[i]=new Bitmap(Bitmap.FromFile("res\\bullet"+i+".jpg"));
        }
        for(int i=0;i<tanksource.Length;i++){
            tanksource[i]=new Bitmap(Bitmap.FromFile("res\\tanksource"+i+".jpg"));
        }
        for(int i=0;i<ground.Length;i++){
            ground[i]=new Bitmap(Bitmap.FromFile("res\\ground"+i+".jpg"));
        }
        snakebody = new Bitmap(Bitmap.FromFile("res\\snakebody.jpg"));
        snakehead = new Bitmap(Bitmap.FromFile("res\\snakehead.jpg"));
    }
    static int toIndex(string who) {
        return who == "weidiao" ? 0 : 1;
    }
    public static Bitmap getTreeByName(string who) {
        return tree[toIndex(who)];
    }
    public static Bitmap getHouseByName(string who) {
        return house[toIndex(who)];
    }
    public static Bitmap getLifeSourceByname(string who) {
        return lifeSource[toIndex(who)];
    }
    public static Bitmap getFoodByname(string who) {
        return food[toIndex(who)];
    }
    public static Bitmap getTank() {
        return tank[0];
    }
    public static Bitmap getBullet(int index) {
        return bullet[index];
    }
    public static Bitmap getTanksource() {
        return tanksource[0];
    }
    public static Bitmap getGround() {
        return ground[0];
    }
}