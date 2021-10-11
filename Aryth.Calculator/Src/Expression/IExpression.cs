namespace Aryth.Expression {
  public interface IExpression {
    dynamic Value { get; set; }
    Variety Variety { get; set; }
  }
}