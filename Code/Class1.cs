namespace Code;


public interface Result<T> { }

public record Success<T>(T value) : Result<T> { }

public record Error<T>(string message) : Result<T> { }


public class Results
{

    public static Result<T1> map<T1, T2>(Result<T2> result, Func<T2, T1> f) => result switch
    {
        Success<T2> s => new Success<T1>(f(s.value)),
        Error<T2> e => new Error<T1>(e.message),
    };


    public static Result<T2> bind<T1, T2>(Result<T1> result, Func<T1, Result<T2>> f) => result switch
    {
        Success<T1> s => f(s.value),
        Error<T1> e => new Error<T2>(e.message),
    };
}
