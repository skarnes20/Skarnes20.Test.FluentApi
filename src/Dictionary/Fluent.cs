namespace Skarnes20.Test.FluentApi.Dictionary;

public abstract class Fluent<T> where T : Fluent<T>
{
    public T Given => (T)this;

    public T That => (T)this;

    public T When => (T)this;

    public T Then => (T)this;
}