using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
	// Autor: Ralf Westphal, Nasser und ?

	class Universum {
		public static IEnumerable<Zellkontext> Analysieren(bool[,] welt) {
			for(var y=0; y<welt.GetLength(0); y++)
				for(var x=0; x<welt.GetLength(1); x++) {
					var zelle = new Zelle{X=x, Y=y, Lebt=welt[y,x]};
					var nachbarn = Erkunden (zelle, welt);
					yield return Zählen (zelle, nachbarn);
				};
		}

		static IEnumerable<Zelle> Erkunden(Zelle zelle, bool[,] welt) {
			for (var dy = -1; dy < 2; dy++)
				for (var dx = -1; dx < 2; dx++) {
					var y = zelle.Y + dy;
					var x = zelle.X + dx;
					var istNachbar = y >= 0 && y < welt.GetLength (0) &&
					                 x >= 0 && x < welt.GetLength (1) &&
									 !(y == zelle.Y && x == zelle.X);
					if (istNachbar)
						yield return new Zelle{ 
							Y = y,
							X = x,
							Lebt = welt[y,x]
						};
				}
		}

		static Zellkontext Zählen(Zelle zelle, IEnumerable<Zelle> nachbarn) {
			return new Zellkontext{ 
				Zelle = zelle,
				AnzahlNachbarn = nachbarn.Count(z => z.Lebt)
			};
		}

		public static bool[,] Synthetisieren(IEnumerable<Zelle> zellen) {
			var breite = zellen.Max (z => z.X) + 1;
			var höhe = zellen.Max (z => z.Y) + 1;
			var welt = new bool[höhe,breite];

			foreach (var z in zellen)
				welt [z.Y, z.X] = z.Lebt;

			return welt;
		}
	}
}