
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;


namespace Examples
{
public partial class TbExampleBasic
{
    private readonly System.Collections.Generic.Dictionary<int, Examples.ExampleBasic> _dataMap;
    private readonly System.Collections.Generic.List<Examples.ExampleBasic> _dataList;
    
    public TbExampleBasic(ByteBuf _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, Examples.ExampleBasic>();
        _dataList = new System.Collections.Generic.List<Examples.ExampleBasic>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Examples.ExampleBasic _v;
            _v = Examples.ExampleBasic.DeserializeExampleBasic(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, Examples.ExampleBasic> DataMap => _dataMap;
    public System.Collections.Generic.List<Examples.ExampleBasic> DataList => _dataList;

    public Examples.ExampleBasic GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Examples.ExampleBasic Get(int key) => _dataMap[key];
    public Examples.ExampleBasic this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

