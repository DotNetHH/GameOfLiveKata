using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
	public class GameOfLife {
		public bool[,] NextGeneration(bool[,] welt) {
			var zellkontexte = Universum.Analysieren (welt);
			var zellen = Regelwerk.Zuechten (zellkontexte);
			return Universum.Synthetisieren (zellen);
		}
	}
}