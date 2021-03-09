namespace ExpressionTreesDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Diagnostics;

    public static class DynamicExtensions
    {
        public static ExposedObject Exposed(this object obj)
            => new ExposedObject(obj);
    }

    // Object constructor big scale
    public class New<T>
        where T : class
    {
        public static Func<T> Instance
            = Expression.Lambda<Func<T>>(Expression.New(typeof(T))).Compile();
    }

    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("-------------CONSTANT-------------");
            Expression<Func<int>> constantExpression = () => 42;
            ParseExpression(constantExpression, String.Empty);

            Console.WriteLine("-------------OPERATOR-------------");
            Expression<Func<int,int,int>> operatorExpression = (x , y) => x + y;
            ParseExpression(operatorExpression, String.Empty);

            Console.WriteLine("-------------PROPERTY-------------");
            Expression<Func<Cat, string>> propertyExpression = catN => catN.Name;
            ParseExpression(propertyExpression, String.Empty);

            Console.WriteLine("-------------NESTED PROPERTY-------------");
            Expression<Func<Cat, string>> propertyExpressionNested = catFN => catFN.Owner.FullName;
            ParseExpression(propertyExpressionNested, String.Empty);

            Console.WriteLine("-------------METHOD-------------");
            int someInt = 42;
            Expression<Func<Cat,string>> methodExpression = catM => catM.SayMew(someInt);
            ParseExpression(methodExpression, String.Empty);

            Console.WriteLine("-------------CONSTRUCTOR-------------");
            Expression<Func<string,string,Cat>> ctorExpression = (catName, ownerName) => new Cat("Pesho")
            {
                Owner = new Owner()
                {
                    FullName = ownerName
                }
            };
            ParseExpression(ctorExpression, String.Empty);

            Console.WriteLine("-------------INVOKE-------------");
            Expression<Func<Func<string>, string, Cat>> invokeExpression = (catName, ownerName) => new Cat(catName())
            {
                Owner = new Owner()
                {
                    FullName = ownerName
                }
            };
            ParseExpression(invokeExpression, String.Empty);

            Console.WriteLine("=====================================================================================");

            Console.WriteLine("---------------------COMPILE EXPRESSION-----------------------------");
            Expression<Func<Cat, string>> expr = catM => catM.SayMew(42);
            var func = expr.Compile();
            var result = func(new Cat());
            Console.WriteLine(result);

            Console.WriteLine("---------------------CREATE EXPRESSION CONST-----------------------------");
            // () => 42;
            // 42
            var constant = Expression.Constant(42);
            // () => 42;
            var lambda = Expression.Lambda<Func<int>>(constant);
            var func2 = lambda.Compile();
            Console.WriteLine(func2());

            Console.WriteLine("---------------------CREATE EXPRESSION CONSTRUCTOR-----------------------------");
            // () => new Cat()
            var newExpression = Expression.New(typeof(Cat));
            var lambda2 = Expression.Lambda<Func<Cat>>(newExpression);
            var catConstruction = lambda2.Compile();
            var cat2 = catConstruction();
            Console.WriteLine(cat2.SayMew(42));

            Console.WriteLine("---------------------CREATE EXPRESSION METHOD WITH CONST------------------------");
            // cat => cat.SayMew(42);
            var typeCat = typeof(Cat);
            var constant2 = Expression.Constant(42);
            //cat
            var parameter = Expression.Parameter(typeCat, "cat");
            // cat.SayMew(42)
            var methodInfo = typeCat.GetMethod(nameof(Cat.SayMew));
            var call = Expression.Call(parameter, methodInfo, constant);
            // cat => cat.SayMew(42);
            var lambda3 =  Expression.Lambda<Func<Cat, string>>(call, parameter);
            var func5 = lambda3.Compile();
            var result2 = func5(new Cat());
            Console.WriteLine(result2);

            Console.WriteLine("---------------------CREATE EXPRESSION METHOD WITH PARAMETER------------------------");
            // cat => cat.SayMew(42);
            var typeCat2 = typeof(Cat);
            //cat
            var parameterCat = Expression.Parameter(typeCat, "cat");
            var parameterNum = Expression.Parameter(typeof(int), "number");
            // cat.SayMew(42)
            var methodInfo2 = typeCat.GetMethod(nameof(Cat.SayMew));
            var call2 = Expression.Call(parameterCat, methodInfo2, parameterNum);
            // cat => cat.SayMew(42);
            var lambda4 = Expression.Lambda<Func<Cat, int, string>>(call2, parameterCat, parameterNum);
            var func6 = lambda4.Compile();
            var result3 = func6(new Cat(), 100);
            Console.WriteLine(result2);

            Console.WriteLine("---------------------OBJECT CONSTRUCTOR-----------------------------");
            var cat3 = New<Cat>.Instance();
            Console.WriteLine(cat3.SayMew(43));


            Console.WriteLine("---------------------ACTIVATOR VS EXPRESSION-----------------------------");
            var list = new List<Cat>();
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(Activator.CreateInstance<Cat>());
            }
            Console.WriteLine(sw.Elapsed);
            New<Cat>.Instance();
            sw = Stopwatch.StartNew();
            for (int i = 0; i < 1000; i++)
            {
                list.Add(New<Cat>.Instance());
            }
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine(list.Count);

            Console.WriteLine("---------------------PROPERTY HELPER-----------------------------");
            var obj = new {id = 5, query = "Test"};
            var dict = new Dictionary<string, object>();
            PropertyHelper.Get(obj).Select(pr => new
                {
                    Name = pr.Name, 
                    Value = pr.Getter(obj)
                })
                .ToList().ForEach(pr =>
                {
                    dict[pr.Name] = pr.Value;
                });
            foreach (var item in dict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}");
            }


            Console.WriteLine("---------------------DYNAMIC-----------------------------");
            var cat = new Cat("Some Cat");
            dynamic exposedCat = cat.Exposed();
            var value = exposedCat.SomeProperty;
            Console.WriteLine(value);

        }

        private static void ParseExpression(Expression expression, string level)
        {
            level += "-";

            if (expression.NodeType == ExpressionType.Lambda)
            {
                Console.WriteLine($"{level}Extracting lambda...");
                var lambdaExpression = (LambdaExpression)expression;

                Console.WriteLine($"{level}Extracting parameters...");

                foreach (var parameter in lambdaExpression.Parameters)
                {
                    ParseExpression(parameter, level);
                }

                var body = lambdaExpression.Body;

                Console.WriteLine($"{level}Extracting body...");
                ParseExpression(body, level);
            }
            else if (expression.NodeType == ExpressionType.Constant)
            {
                Console.WriteLine($"{level}Extracting constant...");
                var constantExpression = (ConstantExpression)expression;
                var value = constantExpression.Value;

                Console.WriteLine($"{level}Constant: {value}");
            }
            else if (expression.NodeType == ExpressionType.Convert)
            {
                Console.WriteLine($"{level}Converting...");
                var unaryExpression = (UnaryExpression)expression;
                ParseExpression(unaryExpression.Operand, level);
            }
            else if (expression.NodeType == ExpressionType.Call)
            {
                Console.WriteLine($"{level}Extracting method...");
                var methodExpression = (MethodCallExpression)expression;

                Console.WriteLine($"{level}Method Name: {methodExpression.Method.Name}");

                if (methodExpression.Object == null)
                {
                    Console.WriteLine($"{level}Method is static");
                }
                else
                {
                    ParseExpression(methodExpression.Object, level);
                }

                Console.WriteLine($"{level}Extracting method arguments...");
                foreach (var argument in methodExpression.Arguments)
                {
                    ParseExpression(argument, level);
                }
            }
            else if (expression.NodeType == ExpressionType.Parameter)
            {
                Console.WriteLine($"{level}Extracting parameter...");
                var parameterExpression = (ParameterExpression)expression;

                Console.WriteLine($"{level}Parameter: {parameterExpression.Name} - {parameterExpression.Type.Name}");
            }
            else if (expression.NodeType == ExpressionType.MemberAccess)
            {
                Console.WriteLine($"{level}Extracting member...");
                var memberExpression = (MemberExpression)expression;

                if (memberExpression.Member is PropertyInfo property)
                {
                    Console.WriteLine($"{level}Property: {property.Name} - {property.PropertyType.Name}");
                }

                if (memberExpression.Member is FieldInfo field)
                {
                    // instance of the closure class
                    var classInstance = (ConstantExpression)memberExpression.Expression;
                    var variableValue = field.GetValue(classInstance.Value); // GetValue(classInstance.Value);

                    Console.WriteLine($"{level}Variable: {variableValue}");
                }

                ParseExpression(memberExpression.Expression, level);
            }
            else if (expression.NodeType == ExpressionType.Add)
            {
                Console.WriteLine($"{level}Extracting binary operation...");
                var binaryExpression = (BinaryExpression)expression;

                ParseExpression(binaryExpression.Left, level);
                ParseExpression(binaryExpression.Right, level);
            }
            else if (expression.NodeType == ExpressionType.New)
            {
                Console.WriteLine($"{level}Extracting object creation...");
                var newExpression = (NewExpression)expression;

                Console.WriteLine($"{level}Constructor: {newExpression.Constructor.DeclaringType.Name}");
                Console.WriteLine($"{level}Extracting constructor arguments...");
                
                foreach (var argument in newExpression.Arguments)
                {
                    ParseExpression(argument, level);
                }
            }
            else if (expression.NodeType == ExpressionType.MemberInit)
            {
                Console.WriteLine($"{level}Extracting object creation with members...");

                var memberInitExpression = (MemberInitExpression)expression;

                ParseExpression(memberInitExpression.NewExpression, level);

                foreach (var memberBinding in memberInitExpression.Bindings)
                {
                    Console.WriteLine($"{level}Extracting member...");
                    Console.WriteLine($"{level}Member: {memberBinding.Member.Name}");

                    var memberAssignment = (MemberAssignment)memberBinding;

                    ParseExpression(memberAssignment.Expression, level);
                }
            }
            else if (expression.NodeType == ExpressionType.Invoke)
            {
                Console.WriteLine($"{level}Extracting Invoke with members...");

                var invocationExpression = (InvocationExpression)expression;

                ParseExpression(invocationExpression.Expression, level);

               //foreach (var memberBinding in invocationExpression.Bindings)
               //{
               //    Console.WriteLine($"{level}Extracting member...");
               //    Console.WriteLine($"{level}Member: {memberBinding.Member.Name}");
               //
               //    var memberAssignment = (MemberAssignment)memberBinding;
               //
               //    ParseExpression(memberAssignment.Expression, level);
               //}
            }
            else
            {
                // TODO: Variable
                // TODO: Extract not supported expression by compilation
            }
        }
    }
}
