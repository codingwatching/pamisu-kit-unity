//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;


namespace Game.Config.Tables
{
public sealed partial class CharacterAIConfig :  Bright.Config.BeanBase 
{
    public CharacterAIConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadString();
        PostInit();
    }

    public static CharacterAIConfig DeserializeCharacterAIConfig(ByteBuf _buf)
    {
        return new CharacterAIConfig(_buf);
    }

    /// <summary>
    /// 这是Id
    /// </summary>
    public string Id { get; private set; }

    public const int __ID__ = -2021491821;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}