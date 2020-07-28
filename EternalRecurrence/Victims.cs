using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

internal static class Victims
{

    private static Dictionary<string, string> victims = new Dictionary<string, string> {
            {"steml", "U59F531H7"},
            {"ivapo", "UR3C5Q1ST"},
            {"flole", "UD3H5EV40"},
            {"krust", "U0C9MR0P9"},
            {"maber", "U1D7UQT6V"}
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
