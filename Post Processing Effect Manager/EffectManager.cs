using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public static class EffectManager
{
    private static Volume _volume;
    public static Volume Volume { 
        get 
        { 
            return _volume; 
            } 
        set {
            _volume = value;
            comps = value.profile.components;
        }        
    }
    private static List<VolumeComponent> comps;
    public static List<VolumeComponent> Comps { get => comps; set => comps = value; }

    public static FilmGrain FilmGrain() => Comps.GetOfType<FilmGrain>();
    public static Bloom Bloom() => Comps.GetOfType<Bloom>();
    public static SplitToning SplitToning() => Comps.GetOfType<SplitToning>();
    public static ColorAdjustments ColorAdjustments() => Comps.GetOfType<ColorAdjustments>();
    public static ChromaticAberration ChromaticAberration() => Comps.GetOfType<ChromaticAberration>();

}
public static class EffectManagerExtensions 
{
    public static T GetOfType<T>(this List<VolumeComponent> comps) where T : VolumeComponent =>
        comps.Where(x => x is T).Select(x => (T)x).FirstOrDefault();
    public static void Apply<T>(this List<T> list,  Func<List<T>, List<VolumeComponent>> func) where T : VolumeComponent
    {
        EffectManager.Comps = func?.Invoke(list);
        EffectManager.Volume.profile.components = EffectManager.Comps;
    }
}
