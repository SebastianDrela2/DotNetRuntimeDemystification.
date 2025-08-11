namespace RuntimeDemystification.SyntaxSugar;

internal class Logic : BaseLogic
{
    public override string Work() => $"I am {nameof(Logic)}";
}

internal class BaseLogic
{
    public virtual string Work() => $"I am {nameof(BaseLogic)}";
    public virtual string WorkReason() => "I do work";
}
