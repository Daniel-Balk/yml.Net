using System.Collections.Generic;
using yml.Net;

namespace yml.Net_Test;

public class MainClass
{
    public string MyString { get; set; } = "A string";

    public List<string> MyStrings { get; set; } = new List<string>()
    {
        "Hello",
        "World"
    };

    [YmlProperty("mint")]
    public int MyInteger { get; set; } = 10324;

    public Subclass MySubclass { get; set; } = new Subclass();

    [YmlIgnore]
    public string Ignored { get; set; } = "This isn't here";
}