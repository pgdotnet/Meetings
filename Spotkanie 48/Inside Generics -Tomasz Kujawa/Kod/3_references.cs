using System;



class Stack<T> 
{
	T[] objs;

	public void MyMethod<T2>(T2 o2) 
	{

	}
}

class Program
{
	
	static void Main() 
	{
	}
}







// public class DonaldDuck<A> {

// }

public class ATeam<T> {
 public class BigBuckBunny<T> {}
 public class CaptainAmerica<U,V> {
 	public class DonaldDuck<W> {}
 }
}
public class Xman {
	public class Yeti<T> {}
} 





// 		StrangeExample.MyMethod("test");
// 		StrangeExample.MyMethod<int>(34);
// class StrangeExample
// {
//     public static void MyMethod<T>(T myVal) { Console.WriteLine("generic!"); }
//     public static void MyMethod(int myVal) { Console.WriteLine("non-generic"); }
// }