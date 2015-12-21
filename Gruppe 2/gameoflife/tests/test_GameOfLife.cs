using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace gameoflife
{
	[TestFixture]
	public class test_GameOfLife {
		[Test]
		public void Akzeptanztest() {
			var sut = new GameOfLife ();

			var welt = new bool[2, 3] {
				{ false, true, true },
				{ true, false, true } 
			};

			var result = sut.NextGeneration (welt);

			Assert.That (result, Is.EqualTo(new[,]{ 
				{false, true, true},
				{false, false, true}
			}));
		}
	}
}