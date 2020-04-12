using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

internal static class Victims
{

    private static Dictionary<string, string> victims = new Dictionary<string, string> {
            {"steml", "U59F531H7"},
            {"maber", "U1D7UQT6V"},
            {"ivapo", "UR3C5Q1ST"},
            {"flole", "UD3H5EV40"},
            {"matha", "U0C3HCB7C"},
        };

    internal static string GetVictim()
    {
        var i = Math.Abs(GetRandom()) % victims.Count;
        return victims.Values.ToArray()[i];
    }

    private static int GetRandom()
    {
        using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
        {
            byte[] rno = new byte[5];
            rg.GetBytes(rno);
            return BitConverter.ToInt32(rno, 0);
        }
    }

}
