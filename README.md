# Very simple Expression Decomposer

```c#
_decompose = new ExpressionDecompose<DummyClass>();


Expression<Func<DummyClass, bool>> lambda = x => x.FirstName == "Random firstname" &&
                                                 x.LastName == "Random lastname" ||
                                                 x.LastName != "Testy" &&
                                                 x.Age > 10;
_decompose.Decompose(lambda);

```

Will result in:

- 'FirstName' Equal "Random firstname" (OrElse)
- 'LastName' Equal "Random lastname" (OrElse)
- 'LastName' NotEqual "Testy" (OrElse)
- 'Age' GreaterThan 10 (OrElse)
