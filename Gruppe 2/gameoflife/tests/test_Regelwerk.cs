using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
	// Autor: Martin Johns und ?

	[TestFixture]
	class test_Regelwerk {
		[Test]
		public void RegelNummer5()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, true, 1);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsFalse(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer32()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, true, 2);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsTrue(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer31()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, true, 3);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsTrue(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer4()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, true, 4);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsFalse(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer2ZuNiedrig()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, false, 1);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsFalse(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer2ZuNiedrig2()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, false, 2);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsFalse(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer1()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, false, 3);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsTrue(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void RegelNummer2Zuhoch()
		{
			List<Zellkontext> zellkontext = ErstelleZellkontextListeEinEintrag(0, 0, false, 4);

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.IsFalse(result.FirstOrDefault().Lebt);
		}

		[Test]
		public void MehrereEintraege0()
		{
			List<Zellkontext> zellkontext = new List<Zellkontext>();
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 4));
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 4));

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.AreEqual(0, result.Count(x => x.Lebt));
		}

		[Test]
		public void MehrereEintraege1()
		{
			List<Zellkontext> zellkontext = new List<Zellkontext>();
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 3));
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 4));

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.AreEqual(1, result.Count(x => x.Lebt));
		}

		[Test]
		public void MehrereEintraege2()
		{
			List<Zellkontext> zellkontext = new List<Zellkontext>();
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 3));
			zellkontext.Add(ErstelleZellkontext(0, 0, true, 4));

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.AreEqual(1, result.Count(x => x.Lebt));
		}

		[Test]
		public void MehrereEintraege3()
		{
			List<Zellkontext> zellkontext = new List<Zellkontext>();
			zellkontext.Add(ErstelleZellkontext(0, 0, false, 3));
			zellkontext.Add(ErstelleZellkontext(0, 0, true, 2));

			var result = Regelwerk.Zuechten(zellkontext);

			Assert.AreEqual(2, result.Count(x => x.Lebt));
		}

		private static List<Zellkontext> ErstelleZellkontextListeEinEintrag(int x, int y, bool lebt, int anzahlNachbarn)
		{
			return new List<Zellkontext>()
			{
				ErstelleZellkontext(x, y, lebt, anzahlNachbarn)
			};
		}

		private static Zellkontext ErstelleZellkontext(int x, int y, bool lebt, int anzahlNachbarn)
		{
			return new Zellkontext {Zelle = new Zelle {Lebt = lebt, X = x, Y = y}, AnzahlNachbarn = anzahlNachbarn};
		}
	}
}