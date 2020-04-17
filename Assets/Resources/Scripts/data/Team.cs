using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{

    public Sensyu[] players = new Sensyu[9];

    public string teamName;

}

public class Tigers : Team
{

    public Tigers()
    {
        teamName = "ｻｰﾊﾞﾙｽﾞ";

        Sensyu sensyu = new Sensyu(4, 9, "いしおか");
        players[0] = sensyu;
        sensyu = new Sensyu(1, 8, "あかし");
        players[1] = sensyu;
        sensyu = new Sensyu(7, 8, "かなもと");
        players[2] = sensyu;
        sensyu = new Sensyu(6, 7, "ひらら");
        players[3] = sensyu;
        sensyu = new Sensyu(10, 7, "ありうす");
        players[4] = sensyu;
        sensyu = new Sensyu(4, 8, "さかおか");
        players[5] = sensyu;
        sensyu = new Sensyu(4, 9, "やのう");
        players[6] = sensyu;
        sensyu = new Sensyu(4, 7, "ふじもり");
        players[7] = sensyu;
        sensyu = new Sensyu(1, 1, "おがわ");
        players[8] = sensyu;
    }
}

public class Giants : Team
{
    public Giants()
    {
        Sensyu sensyu = new Sensyu(4, 9, "さいとう");
        players[0] = sensyu;
        sensyu = new Sensyu(1, 8, "におか");
        players[1] = sensyu;
        sensyu = new Sensyu(7, 8, "たかはし");
        players[2] = sensyu;
        sensyu = new Sensyu(6, 7, "ぺたじー");
        players[3] = sensyu;
        sensyu = new Sensyu(10, 7, "きよはら");
        players[4] = sensyu;
        sensyu = new Sensyu(4, 8, "えとう");
        players[5] = sensyu;
        sensyu = new Sensyu(4, 9, "あべ");
        players[6] = sensyu;
        sensyu = new Sensyu(4, 7, "にし");
        players[7] = sensyu;
        sensyu = new Sensyu(1, 1, "うえはら");
        players[8] = sensyu;
    }
}

public class Sensyu
{
    int power;//ホームラン数わる３
    int tech;//打率わる40
    string name;
    public Sensyu(int power, int tech, string name)
    {
        this.power = power;
        this.tech = tech;
        this.name = name;
    }
}
