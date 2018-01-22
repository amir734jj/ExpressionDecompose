Expression Decomposer

```      
_decompose = new ExpressionDecompose<DummyClass>();


Expression<Func<DummyClass, bool>> lambda = x => x.FirstName == "Random firstname" &&
                                                 x.LastName == "Random lastname" ||
                                                 x.LastName != "Testy"
                                                 && x.Age > 10;
_decompose.Decompose(lambda);

```
