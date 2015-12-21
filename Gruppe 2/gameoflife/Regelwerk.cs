using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
	// Autor: Martin Johns und ?

	class Regelwerk
	{
		public static IEnumerable<Zelle> Zuechten(IEnumerable<Zellkontext> zellkontextListe)
		{
			foreach (var zellkontext in zellkontextListe)
			{
				yield return new Zelle	{
					X = zellkontext.Zelle.X,
					Y = zellkontext.Zelle.Y,
					Lebt = LebtZelle(zellkontext)
				};
			}
		}

		private static bool LebtZelle(Zellkontext zellkontext)
		{
			if (zellkontext.Zelle.Lebt)
				return zellkontext.AnzahlNachbarn == 2 || 
					   zellkontext.AnzahlNachbarn == 3;

			return zellkontext.AnzahlNachbarn == 3;
		}
	}	
}