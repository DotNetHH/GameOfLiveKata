using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace gameoflife
{
	// Autor: Ralf Westphal, Nasser und ?

	[TestFixture ()]
	public class test_Universum
	{
		[Test]
		public void Analysieren() {
			var welt = new bool[2, 3] {
				{ false, true, true },
				{ true, false, true } 
			};

			var result = Universum.Analysieren (welt);

			/*
			 * Der Equalidator ist eine kleine Bib, mit der man beliebige Objektbäume vergleichen kann,
			 * ohne dass darin ein Objekt ein spezielles Equal() implementieren müsste.
			 * Hier sehr bequem für den inhaltlichen Vergleich von Zelle-Objekten.
			 */
			equalidator.Equalidator.AreEqual (result, 
				new[]{
					new Zellkontext{Zelle = new Zelle{X=0,Y=0,Lebt=false}, AnzahlNachbarn=2},
					new Zellkontext{Zelle = new Zelle{X=1,Y=0,Lebt=true}, AnzahlNachbarn=3},
					new Zellkontext{Zelle = new Zelle{X=2,Y=0,Lebt=true}, AnzahlNachbarn=2},
					new Zellkontext{Zelle = new Zelle{X=0,Y=1,Lebt=true}, AnzahlNachbarn=1},
					new Zellkontext{Zelle = new Zelle{X=1,Y=1,Lebt=false}, AnzahlNachbarn=4},
					new Zellkontext{Zelle = new Zelle{X=2,Y=1,Lebt=true}, AnzahlNachbarn=2}
				},
				true
			);
		}


		/*
		 * Gerüsttest!
		 * Testet eine private Methode, sollte also gelöscht werden, nachdem die Funktionalität hergestellt ist.
		 * Er diente nur der gezielten Entwicklung von Universum.Erkunden(), um nicht immer durch die
		 * integrierende Methode Universum.Analysieren() gehen zu müssen.
		 * 
		 * Der Test bleibt ja im Repository, ist also nicht verloren.
		*/
		[Test]
		public void Erkunden() {
			var sut = typeof(Universum).GetMethod ("Erkunden", BindingFlags.Static | BindingFlags.NonPublic);

			var welt = new bool[2, 3] {
				{ false, true, true },
				{ true, false, true } 
			};


			var zelle = new Zelle{Y=0, X=0, Lebt=false};
			var result = (IEnumerable<Zelle>)sut.Invoke (null, new object[]{ zelle, welt });

			equalidator.Equalidator.AreEqual(result, new[]{
				new Zelle{Y=0, X=1, Lebt=true},
				new Zelle{Y=1, X=0, Lebt=true},
				new Zelle{Y=1, X=1, Lebt=false}
			}, true);


			zelle = new Zelle{Y=1, X=1, Lebt=false};
			result = (IEnumerable<Zelle>)sut.Invoke (null, new object[]{ zelle, welt });

			equalidator.Equalidator.AreEqual(result, new[]{
				new Zelle{Y=0, X=0, Lebt=false},
				new Zelle{Y=0, X=1, Lebt=true},
				new Zelle{Y=0, X=2, Lebt=true},
				new Zelle{Y=1, X=0, Lebt=true},
				new Zelle{Y=1, X=2, Lebt=true}
			}, true);
		}




		[Test ()]
		public void Synthetisieren ()
		{
			var zellen = new[]{ 
				new Zelle{X=0, Y=0, Lebt=false},
				new Zelle{X=1, Y=0, Lebt=true},
				new Zelle{X=2, Y=0, Lebt=false},
				new Zelle{X=0, Y=1, Lebt=true},
				new Zelle{X=1, Y=1, Lebt=false},
				new Zelle{X=2, Y=1, Lebt=true},			
			};

			var result = Universum.Synthetisieren (zellen);

			Assert.That (result, Is.EqualTo (
				new bool[2,3]{
					{false, true, false},
					{true, false, true} 
				}
			));
		}
	}


}