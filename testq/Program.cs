using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;


namespace testq
{
	class Program
	{   
		public class strok
		{
			public string Trio { get; set; }
			public int Con { get; set; }
			public strok() { }
		}
		
		
		static void trios(int a)
		{
			string[] s;
			if (a == 1)
			{
				 s = File.ReadAllLines("short.txt");
			}
			else if (a == 2)
			{
				 s = File.ReadAllLines("long.txt");
			}
			else
			{
				Console.WriteLine("Неверно выбран вариант.");
				return;
			}

			List<strok> str = new List<strok>();
			
			int max = 0;
			int maxdel = 0;
			int l = s.Length;
		
			Stopwatch sw = new Stopwatch();

			sw.Start();
			_ = Parallel.For(0, l, i =>
			{
				  int fl = 0;
				  char[] c = s[i].ToCharArray();
				  for (int j = 0; j < c.Length - 2; j++)
				  {
					  if ((c[j] != ' ') && Char.IsLetter(c[j]) && (c[j + 1] != ' ') && Char.IsLetter(c[j + 1]) && (c[j + 2] != ' ') && Char.IsLetter(c[j + 2]))
					  {
						string cc = String.Join(c[j + 1], c[j], c[j + 2]);
						fl = 0;
							for (int ii = 0; ii < str.Count; ++ii)
							{
								if (str[ii].Trio == cc)
								{
									str[ii].Con++;
									fl = 1;
									break;
								}
							}
						  if (fl == 0)
						  {
							strok data = new strok
							{
								Trio = cc,
								Con = 1
							};
							str.Add(data);
						  }
					  }
					  
				  }
			});

			
			for (int i = 0; i < 10; i++)
			{
				max = 0;
				foreach (strok b in str)
				{
					if (b.Con > max)
					{
						max = b.Con;
						maxdel = str.IndexOf(b);
					}
				}
				
				Console.Write("{0} {1}; ", str[maxdel].Con, str[maxdel].Trio);
				str.RemoveAt(maxdel);
			}

			sw.Stop();
			Console.WriteLine();
			Console.WriteLine("Затрачено на выполнение кода {0} мс для обработки {1} строк.", sw.ElapsedMilliseconds.ToString(), s.Length);
		}	
		
		static void Main(string[] args)
		{
			Console.WriteLine("Выберите документ для обработки: \n1. Короткий ( строк)\n2. Длинный ( строк)\n");
			int a = Convert.ToInt32(Console.ReadLine());
			Console.Clear();
			trios(a);
			Console.ReadKey();
		}
	}
}
