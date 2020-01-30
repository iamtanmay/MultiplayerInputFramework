using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerFramework
{
    //NetworkInput will pack local input into efficient byte[] to reduce bandwidth usage
    public struct NetworkInput
    {
        public byte[] bytes;

        public void ToByte(BitArray bits)
        {
            // Make sure we have enough space allocated even when number of bits is not a multiple of 8
            bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);
        }

        public BitArray ToBits()
        {
            return new BitArray(bytes);
        }
    }

    //Raw Player Input
    public class PlayerInput
    {
        public float[] axis;
        public bool[] actions;

        public PlayerInput()
        {
            //x1,y1,x2,y2,scroll
            axis = new float[5];

            //Fire1, Fire2, Fire3/Melee, Jump, Crouch, Run, Action, MainMenu, Inventory
            actions = new bool[9];
        }
    }
}
