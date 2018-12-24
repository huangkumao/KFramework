using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringHelper
{
    //字符串拆分 eg: "1,2," ",,2" "1;;2"
    public static string[] ToStrs(this string pStr, char pSplit = ';', bool pIgnoreEmpty = false)
    {
        var _Strs = pStr.Split(pSplit);
        if (!pIgnoreEmpty)
            return _Strs;

        var _StrL = new List<string>(_Strs.Length);
        foreach (string _S in _Strs)
        {
            if(!string.IsNullOrEmpty(_S))
                _StrL.Add(_S);
        }
        return _StrL.ToArray();
    }

    //转换成int 无法转换或者空默认转成0
    public static int[] ToInts(this string pStr, char pSplit = ';', bool pIgnoreEmpty = false)
    {
        var _Strs = pStr.Split(pSplit);
        var _StrL = new List<int>(_Strs.Length);
        foreach (string _S in _Strs)
        {
            if (string.IsNullOrEmpty(_S))
            {
                if(pIgnoreEmpty)
                    continue;
                _StrL.Add(0);
            }
            else
            {
                int _T = 0;
                if (int.TryParse(_S, out _T))
                    _StrL.Add(_T);
                else
                {
                    if (pIgnoreEmpty)
                        continue;
                    _StrL.Add(0);
                }
            }
        }
        return _StrL.ToArray();
    }

    //转换成float 无法转换或者空默认转成0
    public static float[] ToFloats(this string pStr, char pSplit = ';', bool pIgnoreEmpty = false)
    {
        var _Strs = pStr.Split(pSplit);
        var _StrL = new List<float>(_Strs.Length);
        foreach (string _S in _Strs)
        {
            if (string.IsNullOrEmpty(_S))
            {
                if (pIgnoreEmpty)
                    continue;
                _StrL.Add(0f);
            }
            else
            {
                float _T = 0f;
                if (float.TryParse(_S, out _T))
                    _StrL.Add(_T);
                else
                {
                    if (pIgnoreEmpty)
                        continue;
                    _StrL.Add(0f);
                }
            }
        }
        return _StrL.ToArray();
    }

    //获取标记之前的内容(第一个Split)
    public static string Front(this string pStr, char pSplit = ';')
    {
        int _index = pStr.IndexOf(pSplit);
        return pStr.Substring(0, _index);
    }

    //获取标记之后的内容(最后一个Split)
    public static string Back(this string pStr, char pSplit = ';')
    {
        int _index = pStr.LastIndexOf(pSplit);
        return pStr.Substring(_index);
    }

    //标记中间的内容 第一个 和 最后一个
    public static string Mid(this string pStr, char pSplitF = ';', char pSplitB = ';')
    {
        int _indexF = pStr.IndexOf(pSplitF);
        int _indexB = pStr.LastIndexOf(pSplitB);
        return pStr.Substring(_indexF, _indexB - _indexF);
    }
}
