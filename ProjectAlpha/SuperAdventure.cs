using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

public class SuperAdventure
{
    public string CurrentMonster;
    public string ThePlayer;
    public SuperAdventure(string currentMonster, string thePlayer)
    {
        this.CurrentMonster = currentMonster;
        this.ThePlayer = thePlayer;
    }
}