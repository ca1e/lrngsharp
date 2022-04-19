namespace Gen3RNG
{

public record IVs{
    public uint hp;
    public uint atk;
    public uint def;
    public uint spa;
    public uint spd;
    public uint spe;
}
public record Frame
{
    public uint seed;
    public int number;
    public uint rngValue;
    public uint pid;
    public IVs ivs{get;set;} = new IVs();
}

public static class FrameUtil
{

public static IVs dvsToIVs(uint dvUpper, uint dvLower)
{
    return new IVs{
        hp = dvLower & 0x1f,
        atk = (dvLower & 0x3E0) >> 5,
        def = (dvLower & 0x7C00) >> 10,
        spe = dvUpper & 0x1f,
        spa =  (dvUpper & 0x3E0) >> 5,
		spd = (dvUpper & 0x7C00) >> 10
    };
}
}

}
