using System;
using System.Collections.Generic;

class Program
{
	
	static void Main() 
	{
		
	}
}

class MyClass<T, TomaszKujawa> where T: class, IEnumerable<TomaszKujawa>, new() where TomaszKujawa: T
{
 T myValue;

 void MyMethod<T2>(T2 obj) where T2: class, IEnumerable<int>, new() {
 	// bla bla
 }
}