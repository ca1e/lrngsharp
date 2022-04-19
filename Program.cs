// See https://aka.ms/new-console-template for more information

using Gen3RNG;

Console.WriteLine("Hello, World!");

var fs = Gen3RngUtil.findEmeraldFrame(new IVs{hp = 10,
atk = 10,
def = 20, spa = 31,
spd = 31,spe = 30}, 0, 99);

foreach(var f in fs)
{
    Console.WriteLine(f);
}