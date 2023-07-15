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

<<<<<<< HEAD:Assets/Script/Unit/EnumConverter.cs
    /// <summary>
    /// 文字列を <see cref="EnemyName"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="EnemyName"></typeparam>
    /// <returns>対応する <see cref="EnemyName"/></returns>
    

=======
>>>>>>> parent of 7f84b52 (gomi):Assets/Script/Utility/EnumConverter.cs
    /// <summary>
    /// 文字列を <see cref="EnemyName"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
<<<<<<< HEAD:Assets/Script/Unit/EnumConverter.cs
    /// <typeparam name="UnitGroup"></typeparam>
    /// <returns>対応する <see cref="UnitGroup"/></returns>
    public static UnitGroup ConvertUnitGroup(string str) => convertEnum<UnitGroup>(str, unitGroupTable);
    /// <typeparam name="UnitName"></typeparam>
    /// <returns>対応する <see cref="UnitGroup"/></returns>
    public static UnitGroup ConvertUnitName(string str) => convertEnum<UnitGroup>(str, unitGroupTable);
=======
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
>>>>>>> parent of 7f84b52 (gomi):Assets/Script/Utility/EnumConverter.cs
    
    /// <summary>
    /// 文字列を <see cref="UnitType"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
<<<<<<< HEAD:Assets/Script/Unit/EnumConverter.cs
    /// <typeparam name="UnionLevel"></typeparam>
    /// <returns>対応する <see cref="UnionLevel"/></returns>
    public static UnionLevel ConvertUnionLevel(string str) => convertEnum<UnionLevel>(str, unionLevelTable);

    /// <summary>
    /// 文字列を <see cref="StageID"/> に変換する
    /// </summary>
    /// <param name="str">文字列</param>
    /// <typeparam name="StageID"></typeparam>
    /// <returns>対応する <see cref="StageID"/></returns>
    public static StageID ConvertStageID(string str) => convertEnum<StageID>(str, stageIDTable);
    /// <typeparam name="UnitType"></typeparam>
    /// <returns>対応する <see cref="UnionLevel"/></returns>
    public static UnionLevel ConvertUnionLevel(string str) => convertEnum<UnionLevel>(str, unionLevelTable);
=======
    /// <typeparam name="UnitType"></typeparam>
    /// <returns>対応する <see cref="UnitType"/></returns>
    public static UnitType ConvertUnitType(string str) => convertEnum<UnitType>(str, unitTypeTable);
>>>>>>> parent of 7f84b52 (gomi):Assets/Script/Utility/EnumConverter.cs
}