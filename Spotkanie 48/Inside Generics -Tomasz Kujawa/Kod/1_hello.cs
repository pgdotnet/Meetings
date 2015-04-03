using System;
using System.Collections.Generic;

class Program
{
	
	static void Main() 
	{
		/*var obj1 = new MyClass<int>();
		var obj2 = new MyClass<int[]>();
		var obj3 = new MyClass<string>();

		obj1.UpdateField(1);
		obj2.UpdateField(2);
		obj3.UpdateField(3);

		Console.WriteLine(obj1.GetStaticField());
		Console.WriteLine(obj2.GetStaticField());
		Console.WriteLine(obj3.GetStaticField());*/
	}
}

class MyClass<T>
{
	T value;
	int value2;

	void Mymethod<T>(T obj) {
obj.ToString();

	} 
	/*public static int StaticValue;

	 internal void UpdateField(int p)
     {
         StaticValue = p;
     }

     internal int GetStaticField()
     {
         return StaticValue;
     }*/
}