using System;
using System.Diagnostics;
using yml.Net;
using yml.Net_Test;

var obj = new MainClass();
var s = YmlConvert.Serialize(obj);

Console.WriteLine(s);

var yml = @"MyString: Str1
MyStrings:
- Str2
- Str3
mint: 69
MySubclass:
    MySubSubClass:
        MyString: Str4
    Data: !!binary |
        RXsMhDV7Ig==";

var o = YmlConvert.Deserialize<MainClass>(yml);

Debugger.Break();