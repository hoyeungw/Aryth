using System;
using System.Collections.Generic;
using System.Reflection;
using Aryth.Expression;

//Const xem As XElement =
//<root>
//    <opr id="pwr" sign="^"    method="op_Exponent"    mode="bin" Associativity="right" Precedence="6"/>
//    <opr id="log" sign="l"    method="Log"            mode="bin" Associativity="right" Precedence="6"/>
//    <opr id="mtp" sign="*"    method="op_Multiply"    mode="bin" Associativity="left" Precedence="5"/>
//    <opr id="div" sign="/"    method="op_Division"    mode="bin" Associativity="left" Precedence="5"/>
//    <opr id="adt" sign="+"    method="op_Addition"    mode="bin" Associativity="left" Precedence="4"/>
//    <opr id="sbt" sign="-"    method="op_Subtraction" mode="bin" Associativity="left" Precedence="4"/>
//    <opr id="max" sign="max"  method="Max"            mode="bin" Associativity="left" Precedence="7"/>
//    <opr id="min" sign="min"  method="Min"            mode="bin" Associativity="left" Precedence="7"/>
//    <opr id="exp" sign="exp"  method="Pow"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="log" sign="log"  method="Log"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="sqr" sign="sqr"  method="Sqr"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="abs" sign="abs"  method="Abs"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="int" sign="int"  method="Int"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="sin" sign="sin"  method="Sin"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="cos" sign="cos"  method="Cos"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="tan" sign="tan"  method="Tan"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="cot" sign="cot"  method="Cot"            mode="una" Associativity="right" Precedence="8"/>
//    <opr id="asn" sign="asin" method="Asin"           mode="una" Associativity="right" Precedence="8"/>
//    <opr id="acs" sign="acos" method="Acos"           mode="una" Associativity="right" Precedence="8"/>
//    <opr id="atn" sign="atan" method="Atan"           mode="una" Associativity="right" Precedence="8"/>
//    <opr id="act" sign="acot" method="Acot"           mode="una" Associativity="right" Precedence="8"/>
//</root>

//假设:
//Algebra和OperatorType中的方法没有变更参数数量的重载.
//所有数字皆为Double.
//自定义数据类型(Customized Data Type(CDT))和数字同名的运算符优先级相同.
//运算方法存在于以下类型(Type): Double, CDT, Algebra(e.g. System.Math).
//凡是在上条中类型里出现的方法名称, 均视作运算符.
//凡是在operandLexicon里出现的名称, 均视作操作数.
//上两条冲突者, 视作运算符.
//所以所有操作数, 一律不得取名为Double, CDT, Algebra中的方法.
//且用户在写算式时, 假设悉数所有的运算符及对应的操作数类型.

//实现策略:
//中置表达式转为后制表达式, 再进行运算.
//所有运算符, 凡是出现在Double方法列表的, 先parse为Double运算符.
//待具体运算时, 再parse为自定义数据类型运算方法.
//

namespace Aryth.Archive {
  public static class ExpressionParser {
    public static Queue<IExpression> ExpressionToInfix(string humanExpression,
                                                       TryParser<string, dynamic> tryParseOperand = null,
                                                       TryParser<string, Type[], MethodInfo> tryParseOperator = null) {
      var queue = ExpressionQueue.Build(tryParseOperand, tryParseOperator);
      var phrase = "";
      foreach (var ch in humanExpression) {
        switch (ch) {
          case ' ':
            queue.ParseEnqueue(phrase);
            phrase = "";
            break;
          case '.':
            phrase += ch;
            break;
          case '+':
          case '-':
          case '*':
          case '/':
          case '^':
          case ',':
          case '(':
          case ')':
            queue.ParseEnqueue(phrase);
            phrase = "";
            queue.ParseEnqueue(ch.ToString());
            break;
          default:
            phrase += ch;
            break;
        }
      }
      queue.ParseEnqueue(phrase);
      return queue;
    }
  }
}