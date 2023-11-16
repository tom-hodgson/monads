using Code;

namespace Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(new Success<string>("hello"), new Success<string>("hello"));
    }


    [Fact]
    public void Test2()
    {
        var initial = new Success<string>("hello");
        var computed = Results.bind(initial, a => new Success<string>(a + " world"));
        Assert.Equal(computed, new Success<string>("hello world"));
    }

    [Fact]
    public void Test3()
    {
        var initial = new Error<string>("it broke");
        var computed = Results.bind(initial, a => new Success<string>(a + " world"));
        Assert.Equal(computed, new Error<string>("it broke"));
    }

    [Fact]
    public void Test4()
    {
        var initial = new Success<string>("hello");
        var computed = Results.bind(
            Results.bind(
                Results.bind(
                    Results.bind(
                        Results.bind(initial,
                        a => new Success<string>(a + "1")),
                        a => new Success<string>(a + "2")),
                        a => new Success<string>(a + "3")),
                        a => new Success<string>(a + "4")),
                        a => new Success<string>(a + "5"));

        Assert.Equal(computed, new Success<string>("hello12345"));

    }

    [Fact]
    public void Test5()
    {
        var throw_ = (string errorMessage) => new Error<string>(errorMessage);
        var initial = new Success<string>("hello");
        var computed = Results.bind(
            Results.bind(
                Results.bind(
                    Results.bind(
                        Results.bind(initial,
                        a => new Success<string>(a + "1")),
                        a => new Success<string>(a + "2")),
                        a => throw_("it broke")),
                        a => new Success<string>(a + "4")),
                        a => new Success<string>(a + "5"));


        // initial < -"hello"
        // a < -initial
        // let b = a + "1"


        // b < -a + "2"
        // c < -b + "3"
        // d < -throw_ "it broke"
        // e < -d + "4"
        // f < -e + "5"
        // return f


        // var a = a + 1;
        // var bind
        // throw



        Assert.Equal(computed, new Error<string>("it broke"));

    }
}