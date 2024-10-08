//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;



namespace Game.Config.Tables
{ 
public partial class Tables
{
    public Examples.TbExampleBasic TbExampleBasic {get; }
    public Examples.TbExampleAdvance TbExampleAdvance {get; }
    public Examples.TbExampleSingleton TbExampleSingleton {get; }
    public TbAbilityConfig TbAbilityConfig {get; }
    public TbCharacterConfig TbCharacterConfig {get; }
    public TbCharacterAIConfig TbCharacterAIConfig {get; }
    public TbGlobalConfig TbGlobalConfig {get; }

    public Tables(System.Func<string, ByteBuf> loader)
    {
        var tables = new System.Collections.Generic.Dictionary<string, object>();
        TbExampleBasic = new Examples.TbExampleBasic(loader("examples_tbexamplebasic")); 
        tables.Add("Examples.TbExampleBasic", TbExampleBasic);
        TbExampleAdvance = new Examples.TbExampleAdvance(loader("examples_tbexampleadvance")); 
        tables.Add("Examples.TbExampleAdvance", TbExampleAdvance);
        TbExampleSingleton = new Examples.TbExampleSingleton(loader("examples_tbexamplesingleton")); 
        tables.Add("Examples.TbExampleSingleton", TbExampleSingleton);
        TbAbilityConfig = new TbAbilityConfig(loader("tbabilityconfig")); 
        tables.Add("TbAbilityConfig", TbAbilityConfig);
        TbCharacterConfig = new TbCharacterConfig(loader("tbcharacterconfig")); 
        tables.Add("TbCharacterConfig", TbCharacterConfig);
        TbCharacterAIConfig = new TbCharacterAIConfig(loader("tbcharacteraiconfig")); 
        tables.Add("TbCharacterAIConfig", TbCharacterAIConfig);
        TbGlobalConfig = new TbGlobalConfig(loader("tbglobalconfig")); 
        tables.Add("TbGlobalConfig", TbGlobalConfig);

        PostInit();
        TbExampleBasic.Resolve(tables); 
        TbExampleAdvance.Resolve(tables); 
        TbExampleSingleton.Resolve(tables); 
        TbAbilityConfig.Resolve(tables); 
        TbCharacterConfig.Resolve(tables); 
        TbCharacterAIConfig.Resolve(tables); 
        TbGlobalConfig.Resolve(tables); 
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        TbExampleBasic.TranslateText(translator); 
        TbExampleAdvance.TranslateText(translator); 
        TbExampleSingleton.TranslateText(translator); 
        TbAbilityConfig.TranslateText(translator); 
        TbCharacterConfig.TranslateText(translator); 
        TbCharacterAIConfig.TranslateText(translator); 
        TbGlobalConfig.TranslateText(translator); 
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}