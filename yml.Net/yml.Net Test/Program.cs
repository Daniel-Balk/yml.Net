using System;
using yml.Net;
using yml.Net_Test;

var obj = new MainClass();

Console.WriteLine(YmlConvert.Serialize(obj));