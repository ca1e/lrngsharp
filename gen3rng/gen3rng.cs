namespace Gen3RNG
{

using  FrameData = IVs;

public static class Gen3RngUtil
{
    
public static IEnumerable<Frame> findEmeraldFrame(FrameData dat, int min, int max)
{
    return findFrameGen3(0, dat, min, max);
}

private static IEnumerable<Frame> findRSDryFrame(FrameData dat, int min, int max)
{
    return findFrameGen3(0x05A0, dat, min, max);
}

private static IEnumerable<Frame> findFrameGen3(uint seed, FrameData dat, int min, int max)
{
    var rng = new LCRNG{add = 0x6073, mul = 0x41c64e6d, seed = seed, shift = 16};
    return frameSearcherMethod1(dat, min, max, 0, rng);
}

private static IEnumerable<Frame> frameSearcherMethod1(FrameData dat, int min, int max, int num, LCRNG rng)
{
    if (min == 0 && max == -1)
    {
        yield return new Frame();
        yield break;
    }

    if(min == 0)
    {
        var rng2 = LCRNGUtil.lcrngNext(rng);
        var rng3 = LCRNGUtil.lcrngNext(rng2);
        var rng4 = LCRNGUtil.lcrngNext(rng3);
        //Console.WriteLine($"{rng}, {rng2}");

        var pidLower = LCRNGUtil.lcrngVal(rng);
        var pidUpper = LCRNGUtil.lcrngVal(rng2);
        var dvLower = LCRNGUtil.lcrngVal(rng3);
        var dvUpper = LCRNGUtil.lcrngVal(rng4);
        var pid = LCRNGUtil.combineRNG(pidUpper, pidLower, 16);
        var ivs = FrameUtil.dvsToIVs(dvUpper, dvLower);
        if(dat == ivs)
        {
            yield return new Frame{seed = 0, number = num, rngValue = dvUpper, pid= pid,ivs =  ivs};
        }
        foreach(var f in frameSearcherMethod1(dat, 0, (max-1), (num+1), rng2))
        {
            yield return f;
        }
        yield break;
    } else {
        foreach(var f in frameSearcherMethod1(dat, min-1, (max-1), num+1, LCRNGUtil.lcrngNext(rng)))
        {
            yield return f;
        }
        yield break;
    }
}

}

}
