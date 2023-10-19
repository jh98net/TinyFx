using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace SqlSugarDemo.DAL
{
    public partial class Sdemo_userEO
    {
        #region tinyfx
        public static implicit operator Demo_userEO(Sdemo_userEO value)
        {
            return new Demo_userEO
            {
		        UserID = value.UserID,
		        ClassID = value.ClassID,
		        FBit = value.FBit,
		        FBit_Max = value.FBit_Max,
		        FTinyInt = value.FTinyInt,
		        FTinyInt_Unsigned = value.FTinyInt_Unsigned,
		        FBool = value.FBool,
		        F_Boolean = value.F_Boolean,
		        FBool_TinyInt = value.FBool_TinyInt,
		        FSmallInt = value.FSmallInt,
		        FSmallInt_Unsigned = value.FSmallInt_Unsigned,
		        FMediumInt = value.FMediumInt,
		        FMediumInt_Unsigned = value.FMediumInt_Unsigned,
		        FInt = value.FInt,
		        FInt_Unsigned = value.FInt_Unsigned,
		        F_Integer = value.F_Integer,
		        FBigInt = value.FBigInt,
		        FBigInt_Unsigned = value.FBigInt_Unsigned,
		        FFloat = value.FFloat,
		        FFloat_Max = value.FFloat_Max,
		        FDouble = value.FDouble,
		        FDouble_Max = value.FDouble_Max,
		        F_Real = value.F_Real,
		        F_Double_Precision = value.F_Double_Precision,
		        FYear = value.FYear,
		        FDate = value.FDate,
		        FTime = value.FTime,
		        FTimestamp = value.FTimestamp,
		        FDateTime = value.FDateTime,
		        FChar = value.FChar,
		        FVarChar = value.FVarChar,
		        FBinary = value.FBinary,
		        FVarBinary = value.FVarBinary,
		        FTinyText = value.FTinyText,
		        FText = value.FText,
		        FMediumText = value.FMediumText,
		        FLongText = value.FLongText,
		        FTinyBlob = value.FTinyBlob,
		        FBlob = value.FBlob,
		        FMediumBlob = value.FMediumBlob,
		        FLongBlob = value.FLongBlob,
		        FEnum = value.FEnum,
		        FDecimal = value.FDecimal,
		        FDecimal_Max = value.FDecimal_Max,
		        F_Numeric = value.F_Numeric,
		        F_Dec = value.F_Dec,
		        F_Fixed = value.F_Fixed,
            };
        }
        public static implicit operator Sdemo_userEO(Demo_userEO value)
        {
            return new Sdemo_userEO
            {
		        UserID = value.UserID,
		        ClassID = value.ClassID,
		        FBit = value.FBit,
		        FBit_Max = value.FBit_Max,
		        FTinyInt = value.FTinyInt,
		        FTinyInt_Unsigned = value.FTinyInt_Unsigned,
		        FBool = value.FBool,
		        F_Boolean = value.F_Boolean,
		        FBool_TinyInt = value.FBool_TinyInt,
		        FSmallInt = value.FSmallInt,
		        FSmallInt_Unsigned = value.FSmallInt_Unsigned,
		        FMediumInt = value.FMediumInt,
		        FMediumInt_Unsigned = value.FMediumInt_Unsigned,
		        FInt = value.FInt,
		        FInt_Unsigned = value.FInt_Unsigned,
		        F_Integer = value.F_Integer,
		        FBigInt = value.FBigInt,
		        FBigInt_Unsigned = value.FBigInt_Unsigned,
		        FFloat = value.FFloat,
		        FFloat_Max = value.FFloat_Max,
		        FDouble = value.FDouble,
		        FDouble_Max = value.FDouble_Max,
		        F_Real = value.F_Real,
		        F_Double_Precision = value.F_Double_Precision,
		        FYear = value.FYear,
		        FDate = value.FDate,
		        FTime = value.FTime,
		        FTimestamp = value.FTimestamp,
		        FDateTime = value.FDateTime,
		        FChar = value.FChar,
		        FVarChar = value.FVarChar,
		        FBinary = value.FBinary,
		        FVarBinary = value.FVarBinary,
		        FTinyText = value.FTinyText,
		        FText = value.FText,
		        FMediumText = value.FMediumText,
		        FLongText = value.FLongText,
		        FTinyBlob = value.FTinyBlob,
		        FBlob = value.FBlob,
		        FMediumBlob = value.FMediumBlob,
		        FLongBlob = value.FLongBlob,
		        FEnum = value.FEnum,
		        FSet = value.FSet,
		        FDecimal = value.FDecimal,
		        FDecimal_Max = value.FDecimal_Max,
		        F_Numeric = value.F_Numeric,
		        F_Dec = value.F_Dec,
		        F_Fixed = value.F_Fixed,
            };
        }
        #endregion
    }
}