﻿namespace Homework4
{
    [Serializable]
    public class F { 
        public int i1, i2, i3, i4, i5;
        public F()
        {
        }
        public static F Get() => new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };

    }
}
