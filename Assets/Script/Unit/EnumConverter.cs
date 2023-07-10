using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// 文字列をEnumに変換する静的クラス <br/>
/// JSONファイルからの生成を
/// </summary>
public static class EnumConverter
{
    private static Dictionary<string, UnitGroup> unitGroupTable = new ();
    private static Dictionary<string, UnionLevel> unionLevelTable = new ();

    static EnumConverter()
    {
        setTable<UnitGroup>(unitGroupTable);
        setTable<UnionLevel>(unionLevelTable);
    }

    private static void setTable<TEnum>(Dictionary<string, TEnum> table)
    where TEnum : Enum
    {
        foreach (TEnum item in Enum.GetValues(typeof(TEnum)))
        {
            table.Add(item.ToString(), item);
        }
    }
    
    private static TEnum convertEnum<TEnum>(string str, Dictionary<string, TEnum> table)
    where TEnum : Enum
    {
        Assert.IsNotNull(str, "null is invalid");
        Assert.IsTrue(table.ContainsKey(str), $"{str} is not exist in {typeof(TEnum)}");
        return table[str];
    }

    /// <summary>
    /// 文字列を <see cref="EnemyName"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="EnemyName"></typeparam>
    /// <returns>対応する <see cref="EnemyName"/></returns>
    

    /// <summary>
    /// 文字列を <see cref="UnitGroup"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="UnitName"></typeparam>
    /// <returns>対応する <see cref="UnitGroup"/></returns>
    public static UnitGroup ConvertUnitName(string str) => convertEnum<UnitGroup>(str, unitGroupTable);
    
    /// <summary>
    /// 文字列を <see cref="UnionLevel"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="UnitType"></typeparam>
    /// <returns>対応する <see cref="UnionLevel"/></returns>
    public static UnionLevel ConvertUnionLevel(string str) => convertEnum<UnionLevel>(str, unionLevelTable);
}