namespace Aryth.Expression {
  public struct Expression : IExpression {
    public dynamic Value { get; set; }
    public Variety Variety { get; set; }
    public override string ToString() => Value.ToString();
  }
}