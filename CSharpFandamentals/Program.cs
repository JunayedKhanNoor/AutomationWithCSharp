using System;

namespace CSharpFandamentals
{
 class Program : Vehicle
{
    public void getData()
    {
        Console.WriteLine("I am From a Method");
    } 
     static void Main(string[] args)
    {
        Program p = new Program();
        p.getData();
        p.honk();
        Console.WriteLine(p.brand);
        Console.WriteLine("Hello, World! Junayed");
        int a = 12;
        Console.WriteLine("The Number is:"+ a);
        string name = "Junayed";
        Console.WriteLine("String Variable:"+ name);
        Console.WriteLine($"Modern Way {name}");
        Program2 p2 = new Program2();
        p2.getModel();
    }
}
}