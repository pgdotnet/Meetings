using System;

class Program
{
	
	static void Main() 
	{
		Apple[] apples = new Apple[] { new Apple(), new Apple() };
		Fruit[] fruits = apples;
		fruits[1] = new Banana();
	}
}

class Fruit 
{
	public void Foo() 
	{
		Console.WriteLine("Foo in Fruit");
	}
}

class Apple : Fruit
{
	public void Boo() 
	{
		Console.WriteLine("Boo!");
	}
}

class Banana : Fruit 
{
	public void Zoo()
	{
		Console.WriteLine("HA HA HA");
	}
}