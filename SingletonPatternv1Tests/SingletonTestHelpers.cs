﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SingletonPatternv1Tests
{
    public static class SingletonTestHelpers
    {
        public static void Reset(Type type)
        {
            FieldInfo info = type.GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
            info.SetValue(null, null);
        }
        
        public static T GetPrivateStaticInstance<T>() where T: class
        {
            Type type = typeof(T);
            FieldInfo info = type.GetField("_instance", BindingFlags.NonPublic | BindingFlags.Static);
            return info.GetValue(null) as T;
        }
    }
}
