using System;

class Program
{
	static void Main() 
	{
		var b1 = new Box<int>();
		var b2 = new Box<string>();
		var b3 = new Box<AppDomain>();

		Console.ReadLine();

		// make sure values are not GC'ed
		GC.KeepAlive(b1);
		GC.KeepAlive(b2);
		GC.KeepAlive(b3);
	}
}

class Box<T>
{
	int x;
	T yGeneric;
	bool z;
}