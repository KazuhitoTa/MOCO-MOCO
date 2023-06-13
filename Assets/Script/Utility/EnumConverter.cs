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
    private static Dictionary<string, EnemyName> enemyNameTable = new ();
    private static Dictionary<string, UnitName> unitNameTable = new ();
    private static Dictionary<string, UnitType> unitTypeTable = new ();

    static EnumConverter()
    {
        setTable<EnemyName>(enemyNameTable);
        setTable<UnitName>(unitNameTable);
        setTable<UnitType>(unitTypeTable);
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
    public static EnemyName ConvertEnemyName(string str) => convertEnum<EnemyName>(str, enemyNameTable);

    /// <summary>
    /// 文字列を <see cref="UnitName"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="UnitName"></typeparam>
    /// <returns>対応する <see cref="UnitName"/></returns>
    public static UnitName ConvertUnitName(string str) => convertEnum<UnitName>(str, unitNameTable);
    
    /// <summary>
    /// 文字列を <see cref="UnitType"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="UnitType"></typeparam>
    /// <returns>対応する <see cref="UnitType"/></returns>
    public static UnitType ConvertUnitType(string str) => convertEnum<UnitType>(str, unitTypeTable);
}