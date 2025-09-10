namespace Tests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;
using static System.Runtime.InteropServices.JavaScript.JSType;

public static class SecretHandshake
{
    public static string[] Commands(int commandValue)
    {
        LinkedList<string> coms = new();
        if (commandValue < 1 || commandValue > 31) throw new ArgumentOutOfRangeException();
        if (commandValue % 2 == 1)
            coms.AddLast("wink");
        commandValue /= 2;
        if (commandValue % 2 == 1)
            coms.AddLast("double blink");
        commandValue /= 2;
        if (commandValue % 2 == 1)
            coms.AddLast("close your eyes");
        commandValue /= 2;
        if (commandValue % 2 == 1)
            coms.AddLast("junp");
        commandValue /= 2;
        if (commandValue % 2 == 1)
            coms.Reverse();
        return coms.ToArray();
    }
}

public static class TelemetryBuffer
{
    public static byte[] ToBuffer(long reading)
    {
        byte[] res = new byte[9];
        int numberOfBytes = 8;
        if (reading >= 4_294_967_296)
        {
            //long
            res[0] = 256 - 8;
        }
        else if (reading >= 2_147_483_648)
        {
            //uint
            res[0] = 4;
            numberOfBytes = 4;
        }
        else if (reading >= 65_536)
        {
            //int
            res[0] = 256 - 4;
            numberOfBytes = 4;
        }
        else if (reading >= 0)
        {
            //ushort
            res[0] = 2;
            numberOfBytes = 2;
        }
        else if (reading >= -32_768)
        {
            //short
            res[0] = 256 - 2;
            numberOfBytes = 2;
        }
        else if (reading >= -2_147_483_648)
        {
            //int
            res[0] = 256 - 4;
            numberOfBytes = 4;
        }
        else
        {
            //long
            res[0] = 256 - 8;
        }

        for (int i = 0; i < numberOfBytes; i++)
        {
            res[i + 1] = (byte)reading;
            reading >>>= 8;
        }
        return res;
    }

    public static long FromBuffer(byte[] buffer)
    {
        bool isNeg = (buffer[1] & 0b10000000) == 0b10000000;
        Console.WriteLine(isNeg);
        int numberOfBytes = 0;
        switch (buffer[0])
        {
            case 256 - 8:
                ;//long
                numberOfBytes = 8;
                break;
            case 4:
                ;//uint
                numberOfBytes = 4;
                isNeg = false;
                break;
            case 256 - 4:
                numberOfBytes = 4;
                ;//int
                break;
            case 2:
                numberOfBytes = 2;
                isNeg = false;
                ;//ushort
                break;
            case 256 - 2:
                numberOfBytes = 2;
                ;//short
                break;
            default:
                return 0;
        }
        long res = 0;
        for (int i = 0; i < 8; i++)
        {
            if (i < numberOfBytes)
            {
                res += (long)(buffer[i + 1]) << (i * 8);
            }
            else if (isNeg)
            {
                res += (long)(0b11111111) << (i * 8);
            }
        }
        return res;
    }
}


public class Other
{
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        int count = dominoes.Count();
        if (count == 0) return true;
        int[,] doms = new int[2, count];
        void fillArr()
        {
            int i = 0;
            foreach (var item in dominoes)
            {
                doms[0, i] = item.Item1;
                doms[1, i] = item.Item2;
                i++;
            }
        }
        fillArr();

        (int firstPart, int SecondPart) firstDominoe = (doms[0, 0], doms[1, 0]);
        doms[0, 0] = -1;
        doms[1, 0] = -1;
        int prevSecondPart = firstDominoe.SecondPart;

        if (count == 1) return firstDominoe.firstPart == firstDominoe.SecondPart;

        (int, bool) tryGetDominoe(int num)
        {
            for (int i = 1; i < count; i++)
            {
                if (doms[0, i] == num)
                {
                    var res = (doms[1, i], true);
                    doms[0, i] = -1;
                    doms[1, i] = -1;
                    return res;
                }
                if (doms[1, i] == num)
                {
                    var res = (doms[0, i], true);
                    doms[0, i] = -1;
                    doms[1, i] = -1;
                    return res;
                }
            }
            return (-1, false);
        }

        (int value, bool isFound) dominoeSecondPart = (-1, false);

        for (int i = 1; i < count; i++)
        {
            dominoeSecondPart = tryGetDominoe(prevSecondPart);
            if (dominoeSecondPart.isFound)
            {
                prevSecondPart = dominoeSecondPart.value;
            }
            else
            {
                return false;
            }
        }

        return prevSecondPart == firstDominoe.firstPart;
    }
}