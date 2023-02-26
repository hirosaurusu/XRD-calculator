using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRD_ver2
{
    /// <summary>
    /// 共通定義情報を取得する処理をまとめたstaticクラス
    /// </summary>
    /// <remarks>
    /// 値に応じて計算した結果を返してほしいといった処理をまとめると便利かも。
    /// 
    /// 
    /// </remarks>
    public static class ConstInfo
    {
        /// <summary>
        /// 結晶構造の日本語名を取得する。
        /// </summary>
        /// <param name="cryType">結晶構造の列挙</param>
        /// <returns>結晶構造の日本語名</returns>
        public static string GetCryTypeJpName(int cryType)
        {
            string cryName = "";

            switch (cryType)
            {
                case (int)CryType.CUBIC:
                    cryName = "立方晶";
                    break;
                case (int)CryType.HEXAGONAL:
                    cryName = "六方晶";
                    break;
                case (int)CryType.TETRAGONAL:
                    cryName = "正方晶系";
                    break;
                default:
                    //列挙で追加していても、日本語名称を用意していない場合
                    cryName = "不明";
                    break;
            }
            return cryName;
        }

        /// <summary>
        /// 結晶構造の列挙をもとに結晶構造名称を取得する。
        /// </summary>
        /// <param name="cryType">結晶構造の列挙</param>
        /// <returns>結晶構造の名称</returns>
        public static string GetCryName(int cryType)
        {
            string cryTypeName = "";

            switch (cryType)
            {
                case (int)CryType.CUBIC:
                    cryTypeName = "立方体";
                    break;
                case (int)CryType.HEXAGONAL:
                    break;
                case (int)CryType.TETRAGONAL:
                    break;
                default:
                    //列挙で追加していても名称を用意していない場合
                    cryTypeName = "不明";
                    break;
            }

            return cryTypeName;
        }
    }

    /// <summary>
    /// 結晶構造の列挙
    /// </summary>
    public enum CryType
    {
        /// <summary>
        /// Cubic
        /// </summary>
        CUBIC = 3,
        /// <summary>
        /// Hexagonal
        /// </summary>
        HEXAGONAL = 5,
        /// <summary>
        /// Tetragonal
        /// </summary>
        TETRAGONAL = 6,

    }

    /// <summary>
    /// メッセージの定数クラス
    /// </summary>
    /// <remarks>
    /// public const string ｛アッパースネークケース｝= "メッセージ内容";
    /// みたいな形で、メッセージの定義を増やしていけばいい。
    /// </remarks>
    public class ConstMessage
    {
        /// <summary>
        /// （エラー）数値を入力してください。
        /// </summary>
        public const string ERROR_INPUT_STRING_NOT_NUMERIC = "数値を入力してください。";
    }

}
